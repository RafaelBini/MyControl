using MyControl.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Google.Cloud.Firestore;
using System.Data.SqlClient;

namespace MyControl.dao
{
    class RotinaDAO
    {

        public static DataTable getRotinasView()
        {
            return SqlTool.Consultar("select mes, entrou, saiu, saldo from rotinas_view");
        }


        public static DataView getBancosImportados()
        {
            return SqlTool.Consultar("select (select (CASE WHEN bco is null THEN 'NÃO' ELSE 'SIM' END) from transacao_temp where bco = bco_id limit 1) as importado, bco_id, saldo " +
                "from saldo_bco_preview_view " +
                "order by saldo desc").DefaultView;
        }

        internal static IEnumerable getSaldoBancos()
        {
            return SqlTool.Consultar("select bco_id, saldo " +
                                    "from saldo_bco_preview_view " +
                                    "order by saldo desc").DefaultView;
        }

        public static string getCreditosTotal(string nomeConta=null)
        {
            string q = "";

            if (nomeConta != null)
                q = "select saldo from saldos_temp_grupo where grupo = (select grupo from conta where nome = '"+ nomeConta + "')";
            else
                q = "select sum(saldo) from saldos_temp_grupo";

            return SqlTool.Consultar(q).Rows[0][0].ToString();
        }

        public static string getCreditosSaldos()
        {
            string q = "select grupo, saldo from saldos_temp_grupo";
            string result = "";
            foreach(DataRow linha in SqlTool.Consultar(q).Rows)
            {
                result += linha["grupo"].ToString() + ": " + linha["saldo"].ToString() + "\n";
            }

            return result;
        }

        public static DataTable getCreditosDistribuir()
        {
            string q = "select nome, V.valor " +
            "from conta C " +
            /*"inner join transacao T " +
            "on C.codconta = T.codconta " +
            "inner join (select codconta, sum(valor) as anterior from transacao where to_char(datapgto, 'yyyy/MM') = '2019/07' and tipo = 'D' group by codconta) as A " +
            "on A.codconta = T.codconta " +
            "inner join(select codconta, avg(valor) as media_debitos from(select codconta, to_char(datapgto, 'yyyy/MM'), sum(valor) as valor from transacao where tipo = 'D' group by codconta, to_char(datapgto, 'yyyy/MM')) as soma_mes_conta group by codconta) as MD " +
            "on MD.codconta = T.codconta " +*/
            "left join(select codconta, sum(valor) as valor from transacao_temp where tipo = 'C' group by codconta) as V " +
            "on V.codconta = C.codconta " +
            "where C.ativo = 'true' " +
            "group by nome, V.valor, C.prioridade " +
            "order by C.prioridade desc";

            return SqlTool.Consultar(q);
        }

        internal static void deleteTransacaoTemp()
        {
            string q = "DELETE FROM transacao_temp ";
            SqlTool.Executar(q);
        }

        internal static void insertTransacoesReais()
        {
            string q = "INSERT INTO transacao " +
                        "SELECT * FROM transacao_temp " +
                        "WHERE codconta IS NOT NULL; ";
            SqlTool.Executar(q);

        }

        internal static void insertCreditosReais()
        {
            string q = "INSERT INTO credito (descricao,valor,datarec,distribuido,bco) " +
                        "SELECT descricaobco2,valor,datapgto,'true',bco FROM transacao_temp " +
                        "WHERE tipo='C' AND codconta IS NULL";
            SqlTool.Executar(q);
        }

        public static string getSaldoConta(string conta)
        {
            try
            {
                string q = "select sum(valor) from transacao_preview where codconta = (select codconta from conta where nome='"+conta+"')";
                return SqlTool.Consultar(q).Rows[0][0].ToString() == "" ? "0.0" : SqlTool.Consultar(q).Rows[0][0].ToString();
            }
            catch
            {
                return "0.00";
            }
        }

        internal static object getMediaCreditosConta(string conta)
        {
            try
            {
                return SqlTool.Consultar("select round(avg(valor),2) as media_creditos from(select codconta, to_char(datapgto, 'yyyy/MM'), sum(valor) as valor from transacao where tipo = 'C' group by codconta, to_char(datapgto, 'yyyy/MM')) as soma_mes_conta where codconta = (select codconta from conta where nome='" + conta + "') group by codconta").Rows[0][0].ToString();
            }
            catch
            {
                return "0.00";
            }
        }

        internal static void insertCredito(string conta, string valor)
        {
            string q = "insert into transacao_temp (descricao, codconta, valor, datapgto, tipo, adddatetime, metodoentrada, conta) VALUES " +
            "('Distribuição MyControl', (select codconta from conta where nome ='" + conta + "'), '" + valor.Replace(",", ".") + "', '" + getAnoMes() + "/01" + "', 'C', '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', 'Distrib MyC', '" + conta + "');";
            SqlTool.Executar(q);
        }

        internal static void updateTransacaoFire(DocumentSnapshot document)
        {
            Dictionary<string, object> d = document.ToDictionary();

            DateTime addTime = Convert.ToDateTime(d["addTime"].ToString().Replace("Timestamp: ", "").Replace("T"," ").Replace("Z", " "));

            string q = "update transacao_temp " +
                "set descricao = '" + d["descricao"].ToString().Replace("'", "") + "', codconta=(select codconta from conta where nome='"+d["conta"].ToString()+ "'), conta='" + d["conta"].ToString() + "' " +
                "where valor='-"+d["valor"].ToString().Replace(",",".") +"' and datapgto between '"+ addTime.AddDays(-5).ToString() + "' and '" + addTime.AddDays(3).ToString() + "' ";
            SqlTool.Executar(q);
        }

        internal static void importarCSV(string fileName, Banco bancoSelecionado, string anoMes)
        {
            // Recebe os cabeçalhos
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("iso-8859-1"));
            string[] headers = sr.ReadLine().Split(';');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }

            // Valida se existem os tres cabeçalhos
            if (!dt.Columns.Contains("DESCRICAO"))
            {
                throw new Exception("Deve haver uma coluna com o nome DESCRICAO");
            }
            else if (!dt.Columns.Contains("VALOR"))
            {
                throw new Exception("Deve haver uma coluna com o nome VALOR");
            }
            else if (!dt.Columns.Contains("DATA"))
            {
                throw new Exception("Deve haver uma coluna com o nome DATA");
            }

            // Recebe as linhas
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                // Valida se a linha está vazia
                if (line.Replace(";", "").Trim() == "")
                    continue;

                // Recebe os dados da linha
                string[] rows = line.Split(';');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }

            // Cria a string do comando SQL
            string transacao_temp_insert = "INSERT INTO transacao_temp (datapgto,valor,tipo,descricaobco2,dctobco,bco,adddatetime,metodoentrada) VALUES ";
            foreach (DataRow linha in dt.Rows)
            {
                // Se é fora do mes, pula
                if (Convert.ToDateTime(linha["DATA"]).ToString("yyyy/MM") != anoMes)
                    continue;

                // Incrementa a string
                transacao_temp_insert +=
                    "('" + linha["DATA"].ToString().Trim() + "', " +
                    "'" + linha["VALOR"].ToString().Replace(",", ".").Trim() + "', " +
                    "'" + (Convert.ToDouble(linha["VALOR"].ToString()) > 0 ? "C" : "D") + "', " +
                    "'" + linha["DESCRICAO"].ToString() + "', " +
                    "''," +
                    "'" + bancoSelecionado.Id + "'," +
                    "'" + DateTime.Now.ToString() + "'," +
                    "'C# CSV'),";
            }

            // Finaliza string
            transacao_temp_insert = transacao_temp_insert.Substring(0, transacao_temp_insert.Length - 1);

            // Executa o insert
            SqlTool.Executar(transacao_temp_insert);

        }

        internal static void deleteCredito(string conta, string valor)
        {
            string q = "delete from transacao_temp where tipo = 'C' and codconta = (select codconta from conta where nome = '"+ conta + "')";
            SqlTool.Executar(q);
        }

        public static bool isContaDeBoa(string conta)
        {
            try
            {
                // Recebe a media de debitos
                double media_deb = Convert.ToDouble(getMediaGastosConta(conta));

                // Recebe o saldo
                double saldo = Convert.ToDouble(getSaldoConta(conta));
                                
                return (saldo >= Math.Abs(media_deb));
            }
            catch
            {
                return false;
            }
        }

        public static string getMediaGastosConta(string conta)
        {
            try
            {
                string q = "select round(avg(valor),2) as media_debitos from(select codconta, to_char(datapgto, 'yyyy/MM'), sum(valor) as valor from transacao where tipo = 'D' group by codconta, to_char(datapgto, 'yyyy/MM')) as soma_mes_conta where codconta = (select codconta from conta where nome='" + conta + "') group by codconta";
                return Math.Abs(Convert.ToDouble(SqlTool.Consultar(q).Rows[0][0])).ToString() == "" ? "0.0" : Math.Abs(Convert.ToDouble(SqlTool.Consultar(q).Rows[0][0])).ToString();
            }
            catch
            {
                return "0.00";
            }
        }

        public static DataView getSaldoDebitoContas()
        {
            string q = "select S.nome as conta, S.valor as saldo, D.debitos " +
            "from saldo_contas_view S left " +
            "join total_debito_contas_view D on D.nome = S.nome " +
            "where (D.mespgto = '"+ getAnoMes() + "'  or D.mespgto is null) " +
            "order by 1";

            return SqlTool.Consultar(q).DefaultView;
        }

        public static DataView getSaldoContas()
        {
            string q = "select S.nome as conta, S.valor as saldo " +
            "from saldo_contas_preview_view S order by 1";

            return SqlTool.Consultar(q).DefaultView;
        }

        internal static bool debitosCanGo()
        {
            return SqlTool.Consultar("select count(*) from transacao_temp where (descricao is null or descricao = '' or codconta is null or conta is null or conta = '') and tipo='D'").Rows[0][0].ToString() == "0";
        }

        public static void dividirTransacao(string valor, int transacaoSelecionada)
        {

            String insert = "insert into transacao_temp (descricaobco1, descricaobco2, valor, datapgto, dctobco, tipo, adddatetime, metodoentrada, bco, codtran_mae) " +
                    "select descricaobco1, descricaobco2, '-"+valor.ToString().Replace(",",".")+"', datapgto, dctobco, tipo, adddatetime, metodoentrada, bco, '" + transacaoSelecionada + "' " +
                    "from transacao_temp "+
                    "where codtran = '"+transacaoSelecionada+"'; ";
            String update = "update transacao_temp set valor = valor + " + valor.ToString().Replace(",", ".") + " where codtran = '" + transacaoSelecionada + "'";

            SqlTool.Executar(insert + update);
        }

        public static DataView getDebitosTemp()
        {
            string q = "select B.nome as conta, A.descricao, A.valor, A.descricaobco2, A.descricaobco1, to_char(A.datapgto,'dd/MM/yy') as data, A.bco as banco, A.codtran, A.codtran_mae, banco.grupo from transacao_temp A left join conta B on (A.codconta = B.codconta) left join banco on (banco.bco_id = A.bco) where to_char(datapgto,'yyyy/MM') = '" + getAnoMes() + "' and tipo='D' order by datapgto desc, bco, descricaobco2, valor";

            return SqlTool.Consultar(q).DefaultView;
        }

        public static int getParte()
        {
            return Convert.ToInt32(SqlTool.Consultar("SELECT parte FROM rotina").Rows[0][0].ToString());
        }

        public static void setParte(int parte)
        {
            SqlTool.Executar("UPDATE rotina SET parte=" + parte);
        }

        internal static void setAnoMes()
        {
            string ano_mes = SqlTool.Consultar("select min(mes) from rotinas_view where saiu is null").Rows[0][0].ToString();
            SqlTool.Executar("UPDATE rotina SET ano_mes='" + ano_mes + "'");
        }

        internal static string getAnoMes()
        {
            return SqlTool.Consultar("select ano_mes from rotina").Rows[0][0].ToString();
        }

        public static void updateDescricaoTransTemp(string nova_d, int t)
        {
            SqlTool.Executar("UPDATE transacao_temp SET descricao='"+ nova_d + "' WHERE codtran="+t);
        }

        public static bool foiImportado(Banco bco)
        {
            return SqlTool.Consultar("SELECT COUNT(bco) FROM transacao_temp WHERE bco = '" + bco.Id + "'").Rows[0][0].ToString() != "0";
        }

        public static void deleteExtrato(Banco bco)
        {
            SqlTool.Executar("DELETE FROM transacao_temp WHERE bco = '"+bco.Id+"'");
        }

        public static void importarOfx(string arquivo_ofx, Banco banco, string ano_mes)
        {
            // Cria lista de transações
            List<Transacao> transacoes = new List<Transacao>();

            // Cria um metodo para leitura das tags
            String TagValue(String tagLine, String tagName)
            {
                return tagLine.Replace("<" + tagName + ">", "").Replace("</" + tagName + ">", "").TrimStart().TrimEnd();
            }

            // Abre o arquivo
            StreamReader sr = new StreamReader(arquivo_ofx);

            // Le o arquivo e extrai informações para a lista
            while (!sr.EndOfStream)
            {
                // Recebe uma nova linha
                String linha = sr.ReadLine();

                // Faz verificação de banco
                if (linha.Contains("<BANKID>"))
                {
                    int bco_id2 = Convert.ToInt32(TagValue(linha, "BANKID"));
                    if (bco_id2 != banco.Id2)
                    {
                        throw new Exception("Arquivo não correspondente ao banco selecionado!");
                    }

                }
                

                // Procura por uma transacao
                    if (!linha.Contains("<STMTTRN>"))
                    continue;

                else
                {
                    // Cria uma nova transacao
                    Transacao transacao = new Transacao();

                    // Grava o banco
                    transacao.bco = banco.Id;

                    // Faz leitura da linha
                    while (true)
                    {
                        // Le a proxima linha
                        linha = sr.ReadLine();

                        // Se chegar ao fim da tag transacao ou encontrar todos os valores buscados ou chegar ao fim do arquivo, break
                        if (linha.Contains("</STMTTRN>") || transacao.hasAllValues() || sr.EndOfStream)
                            break;

                        // Verifica se é um atributo e registra no lugar certo
                        if (linha.Contains("TRNTYPE"))
                        {
                            string t = TagValue(linha, "TRNTYPE");
                            t = t.Contains("0") || t.Contains("C") ? "C" : "D";
                            transacao.tipo = t;
                        }
                        else if (linha.Contains("DTPOSTED"))
                        {
                            string dtS = TagValue(linha, "DTPOSTED");
                            dtS = dtS.Length > 8 ? dtS.Substring(0, 8) : dtS;
                            transacao.datapgto = DateTime.ParseExact(dtS, "yyyyMMdd", null);
                        }
                        else if (linha.Contains("TRNAMT"))
                            transacao.valor = Convert.ToDouble(TagValue(linha, "TRNAMT"), new CultureInfo("en-US"));
                        else if (linha.Contains("CHECKNUM") || linha.Contains("CHKNUM"))
                            transacao.dctobco = TagValue(TagValue(linha, "CHECKNUM"), "CHKNUM");
                        else if (linha.Contains("MEMO"))
                            transacao.descricaobco2 = TagValue(linha, "MEMO");
                    }

                    // Add na lista
                    transacoes.Add(transacao);
                }
            }

            // Transforma a lista captada em uma string de insert
            string transacao_temp_insert = "INSERT INTO transacao_temp (datapgto,valor,tipo,descricaobco2,dctobco,bco,adddatetime,metodoentrada) VALUES ";
            int mes_alvo = Convert.ToInt32(ano_mes.Substring(5, 2));
            int ano_alvo = Convert.ToInt32(ano_mes.Substring(0, 4));
            foreach (Transacao transacao in transacoes)
            {
                // Se não é do mês alvo, pula
                if (transacao.datapgto.Month != mes_alvo || transacao.datapgto.Year != ano_alvo)
                    continue;

                // Incrementa a string
                transacao_temp_insert += 
                    "('" + transacao.datapgto.ToString("dd/MM/yyyy") + "', " +
                    "'" + transacao.valor.ToString().Replace(".","").Replace(",",".") + "', " +
                    "'" + transacao.tipo + "', " +
                    "'" + transacao.descricaobco2 + "', " +
                    "'" + transacao.dctobco + "'," +
                    "'" + transacao.bco + "'," +
                    "'"+DateTime.Now.ToString()+"'," +
                    "'C# OFX'),";                
            }

            // Finaaliza string
            transacao_temp_insert = transacao_temp_insert.Substring(0, transacao_temp_insert.Length - 1);

            // Executa o insert
            SqlTool.Executar(transacao_temp_insert);
        }

        #region advinhações

        // Função para transformar frase em buscável sql
        private static string condits(string campo, string theString)
        {
            // Valida a string
            if (theString == null || theString.Trim() == "" || theString.Length <= 1)
                return theString;

            string c = "( ";
            foreach (string palavra in theString.Split(' '))
            {
                if (palavra.Trim() != "" && palavra.Trim().Length > 1)
                    c += " UPPER(" + campo + ") like UPPER('%" + palavra + "%') or ";
            }
            return c = c.Substring(0, c.Length - 3) + " )";
        }

        // Função para retirar palavras genéricas de uma string
        private static string limpar(string theString)
        {
            // Valida a string
            if (theString == null || theString.Trim() == "" || theString.Length <= 1)
                return theString;

            string[] genericas = new string[] { "COMPRA", "CART", "ELO", "CP", "MAESTRO", "DEBITO", "PAGTO", "COBRANCA" };
            theString = theString.ToUpper();
            foreach (string g in genericas)
            {
                theString = theString.Replace(g, "");
            }
            return theString;
        }


        public static string adivinharConta(string descricao, double valor)
        {
            // Declarações
            string[] contas = new string[3];
            int[] pontos = new int[3];

            // Define as variaveis de margem de valor
            double delta = valor * 5 * -1;
            string valorMax = (valor + delta).ToString().Replace(",", ".");
            string valorMin = (valor - delta).ToString().Replace(",", ".");

            // Retira palavras genéricas
            descricao = limpar(descricao);

            // Recebe as adivinhações peso 3
            string q = "SELECT conta, count(*) from transacao WHERE tipo='D' and (valor between " + valorMin + " and " + valorMax + ") and (" + condits("descricao", descricao) + ") GROUP BY conta ORDER BY 2 DESC";
            DataTable dt = SqlTool.Consultar(q);
            if (dt.Rows.Count > 0)
            {
                contas[0] = dt.Rows[0][0].ToString();
                pontos[0] = Convert.ToInt32(dt.Rows[0][1]) * 3;
            }

            // Recebe as adivinhações peso 2
            q = "SELECT conta, count(*) from transacao WHERE tipo='D' and (valor between " + valorMin + " and " + valorMax + ") and (" + condits("descricaobco2", descricao) + ") GROUP BY conta ORDER BY 2 DESC";
            dt = SqlTool.Consultar(q);
            if (dt.Rows.Count > 0)
            {
                contas[1] = dt.Rows[0][0].ToString();
                pontos[1] = Convert.ToInt32(dt.Rows[0][1]) * 2;
            }

            // Recebe as adivinhações peso 1
            q = "SELECT conta, count(*) from transacao WHERE tipo='D' and (valor between " + valorMin + " and " + valorMax + ") and (" + condits("descricaobco1", descricao) + ") GROUP BY conta ORDER BY 2 DESC";
            dt = SqlTool.Consultar(q);
            if (dt.Rows.Count > 0)
            {
                contas[2] = dt.Rows[0][0].ToString();
                pontos[2] = Convert.ToInt32(dt.Rows[0][1]) * 1;
            }

            // Verifica qual descricao é maior
            int maxValue = pontos.Max();
            int maxIndex = pontos.ToList().IndexOf(maxValue);

            return contas[maxIndex];

        }

        public static string adivinharDescricao(string descricao, double valor)
        {

            // Função para retirar sufixo inadequado e adicionar sufixo adequado
            string tratarSufixo(string theString)
            {
                // Valida a string
                if (theString == null || theString.Trim() == "" || theString.Length <= 1)
                    return theString;

                string[] chaves = new string[] { "DIZIMO", "TELEFONE", "NET", "CLARO", "PREST HAB", "LUZ", "ENERGIA" };
                bool precisaTratar = false;
                foreach (string c in chaves)
                {
                    // Verifica se realmente precisa tratar (verificando pelas chaves)
                    if (theString.ToUpper().Contains(c))
                    {
                        precisaTratar = true;
                        break;
                    }
                }
                if (!precisaTratar)
                   return theString;

                // Retira badSufixos
                string[] meses = new string[] { "JAN", "FEV", "MAR", "ABR", "MAI", "JUN", "JUL", "AGO", "SET", "OUT", "NOV", "DEZ" };
                foreach(string p in theString.Split(' '))
                {
                    foreach(string m in meses)
                    {
                        if (p.Contains(m))
                            theString = theString.Replace(p, "");
                    }
                }

                // Insere right sufixo
                string mesAnoRotina = RotinaDAO.getAnoMes();
                theString += " " + Convert.ToDateTime(mesAnoRotina + "/01").AddMonths(-1).ToString("MMMyy");

                // Retorna
                return theString;

            }

            // Declarações
            string[] descricoes = new string[3];
            int[] pontos = new int[3];

            // Define as variaveis de margem de valor
            double delta = valor * 5 * -1;
            string valorMax = (valor + delta).ToString().Replace(",", ".");
            string valorMin = (valor - delta).ToString().Replace(",", ".");

            // Retira palavras genéricas
            descricao = limpar(descricao);

            // Valida a descricao
            if (descricao.Trim() == "" || descricao.Length <= 1 || descricao == null)            
                return "";
            

            // Recebe as adivinhações peso 3
            string q = "SELECT descricao, count(*) from transacao WHERE tipo='D' and (valor between "+ valorMin + " and "+ valorMax + ") and (" + condits("descricao", descricao) + ") GROUP BY descricao ORDER BY 2 DESC";
            DataTable dt = SqlTool.Consultar(q);
            if (dt.Rows.Count > 0)
            {
                descricoes[0] = dt.Rows[0][0].ToString();
                pontos[0] = Convert.ToInt32(dt.Rows[0][1]) * 3;
            }

            // Recebe as adivinhações peso 2
            q = "SELECT descricao, count(*) from transacao WHERE tipo='D' and (valor between " + valorMin + " and " + valorMax + ") and (" + condits("descricaobco2", descricao) + ") GROUP BY descricao ORDER BY 2 DESC";
            dt = SqlTool.Consultar(q);
            if (dt.Rows.Count > 0)
            {
                descricoes[1] = dt.Rows[0][0].ToString();
                pontos[1] = Convert.ToInt32(dt.Rows[0][1]) * 2;
            }

            // Recebe as adivinhações peso 1
            q = "SELECT descricao, count(*) from transacao WHERE tipo='D' and (valor between " + valorMin + " and " + valorMax + ") and (" + condits("descricaobco1", descricao) + ") GROUP BY descricao ORDER BY 2 DESC";
            dt = SqlTool.Consultar(q);
            if (dt.Rows.Count > 0)
            {
                descricoes[2] = dt.Rows[0][0].ToString();
                pontos[2] = Convert.ToInt32(dt.Rows[0][1]) * 1;
            }

            // Verifica qual descricao é maior
            int maxValue = pontos.Max();
            int maxIndex = pontos.ToList().IndexOf(maxValue);

            // Adiciona sufixo
            descricoes[maxIndex] = tratarSufixo(descricoes[maxIndex]);

            // Verifica se já não existe uma descricao com este nome nas transacoes temp e Retorna a descriçao vencedora
            string repetidos = SqlTool.Consultar("SELECT COUNT(*) FROM transacao_temp where descricao = '"+ descricoes[maxIndex] + "'").Rows[0][0].ToString();
            if (repetidos =="0" || descricoes[maxIndex] == "" || descricoes[maxIndex] == null)
                return descricoes[maxIndex];
            else
                return descricoes[maxIndex] + " " + repetidos;

        }

        #endregion

        public static void updateContaTransTemp(string nova_c, int t)
        {
            SqlTool.Executar("UPDATE transacao_temp SET conta='" + nova_c + "', codconta=(select codconta from conta where nome = '" + nova_c + "') WHERE codtran='" + t + "' ");
        }
    }
}
