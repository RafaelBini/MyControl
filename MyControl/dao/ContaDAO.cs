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
    }
}
