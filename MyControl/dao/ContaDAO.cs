using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyControl.model;

namespace MyControl.dao
{
    class ContaDAO
    {
        public static DataTable getObrigatoriasPendente()
        {
            return SqlTool.Consultar("SELECT nome FROM CONTA left join Transacao_Temp on (Transacao_Temp.codconta = Conta.codconta) WHERE notificar = true and Transacao_Temp.codtran is null");
        }

        internal static Dictionary<string, object> getContasToFire()
        {
            Dictionary<string, object> lista = new Dictionary<string, object>();

            foreach (DataRow conta in SqlTool.Consultar("select nome, valor as saldo from saldo_contas_view").Rows)
            {
                Dictionary<string, object> node = new Dictionary<string, object>();
                Double saldo = Convert.ToDouble(conta["saldo"]);
                node["saldo inicial"] = saldo;
                node["saldo atual"] = saldo;                
                lista[conta["nome"].ToString()] = node;
            }

            return lista;

        }

        public static DataView getSaldoContas(bool somenteAtivos = false)
        {
            string q = "select S.nome as conta, S.valor as saldo, S.ativo " +
            "from saldo_contas_view_com_inativas S "+(somenteAtivos ? " where ativo = 'true' " : "") +" order by 1";

            return SqlTool.Consultar(q).DefaultView;
        }

        public static DataView getContasExceto(string nomeConta)
        {
            string q = "select nome from conta where ativo='true' and nome<>'"+nomeConta+"' order by nome";

            return SqlTool.Consultar(q).DefaultView;
        }

        internal static IEnumerable<string> getContasInativas()
        {
            List<string> lista = new List<string>();
            foreach(DataRow conta in SqlTool.Consultar("select nome from conta where ativo=false").Rows){
                lista.Add(conta["nome"].ToString());
            }
            return lista;
        }

        internal static void insertConta(Conta c)
        {
            string q = "insert into conta (nome, descricao, prioridade, notificar, ativo, adddatetime) values ('"+c.nome+"','"+c.descricao+"','"+c.prioridade+"', '"+c.notificar.ToString()+"', 'true', '"+DateTime.Now.ToString()+"')";
            SqlTool.Executar(q);
        }

        internal static Conta getConta(string nomeConta)
        {
            string q = "SELECT *, (select valor from saldo_contas_view where nome ='"+nomeConta+"' ) as saldo FROM conta WHERE nome = '" + nomeConta+"'";
            Conta c = new Conta();
            try
            {
                DataRow i = SqlTool.Consultar(q).Rows[0];
                c.cod = i["codconta"].ToString();
                c.nome = i["nome"].ToString();
                c.descricao = i["descricao"].ToString();
                c.prioridade = Convert.ToInt32(i["codconta"]);
                c.link = i["link"].ToString();
                c.ativo = Convert.ToBoolean(i["ativo"]);
                c.saldo = i["saldo"].ToString();
            }
            catch
            {
                c = null;
            }

            return c;
        }

        internal static void setStatus(string nomeConta, bool ativo)
        {
            string q = "UPDATE conta SET ativo = '" + ativo.ToString() + "' WHERE nome='"+nomeConta+"'";
            SqlTool.Executar(q);
        }
    }
}
