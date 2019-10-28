using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControl.dao
{
    class EstatisticaDAO
    {
        public static Double getMesesSobrevivencia()
        {
            return Convert.ToDouble(SqlTool.Consultar("select meses from sobrevivencia_view").Rows[0][0].ToString());
        }
    }
}
