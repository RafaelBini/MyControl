using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControl.dao
{
    class TransacaoDAO
    {
        public static void Transferir(string contaDoadora, string contaRecebedora, string valor)
        {
            DateTime agora = DateTime.Now;

            string q1 = "insert into transacao (descricao,valor,codconta,datapgto,tipo,adddatetime,metodoentrada,conta) " +
                        "values ('Transferência entre contas MyControl (cod "+ agora.ToString("yyyyMMddHHmmss")+")', '-"+ valor.Replace(",",".") +"', (select codconta from conta where nome='"+contaDoadora+"'), '"+DateTime.Today.ToString()+ "', 'T', '" + agora.ToString() + "', 'C# MyC', '" + contaDoadora + "'); ";

            string q2 = "insert into transacao (descricao,valor,codconta,datapgto,tipo,adddatetime,metodoentrada,conta) " +
                        "values ('Transferência entre contas MyControl (cod " + agora.ToString("yyyyMMddHHmmss") + ")', '" + valor.Replace(",", ".") + "', (select codconta from conta where nome='" + contaRecebedora + "'), '" + DateTime.Today.ToString() + "', 'T', '" + agora.ToString() + "', 'C# MyC', '" + contaRecebedora + "'); ";

            SqlTool.Executar(q1 + q2);
        }
    }
}
