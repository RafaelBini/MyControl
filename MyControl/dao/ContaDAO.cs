using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal static IEnumerable<string> getContasInativas()
        {
            List<string> lista = new List<string>();
            foreach(DataRow conta in SqlTool.Consultar("select nome from conta where ativo=false").Rows){
                lista.Add(conta["nome"].ToString());
            }
            return lista;
        }
    }
}
