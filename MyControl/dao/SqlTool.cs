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
            DataSet ds = null;
            try
            {
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(SqlQuery, con);
                ds = new DataSet();
                adapter.Fill(ds);
            }
            finally
            {
                con.Close();
            }
            
            return ds.Tables[0];
        }

        public static int Executar(String SqlQuery)
        {
            int r = 0;
            NpgsqlConnection con = ConnectionFactory.GetConnection();
            con.Open();            
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(SqlQuery, con);
                r = cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }            
            return r;
        }
    }
}
