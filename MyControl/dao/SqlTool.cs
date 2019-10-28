using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControl.dao
{
    class SqlTool
    {
        public static DataTable Consultar(String SqlQuery)
        {
            NpgsqlConnection con = ConnectionFactory.GetConnection();
            con.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(SqlQuery, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            return ds.Tables[0];
        }

        public static int Executar(String SqlQuery)
        {
            NpgsqlConnection con = ConnectionFactory.GetConnection();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(SqlQuery,con);
            int r = cmd.ExecuteNonQuery();
            con.Close();
            return r;
        }
    }
}
