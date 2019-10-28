using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControl.dao
{
    class ConnectionFactory
    {

        public static string server = "127.0.0.1";
        public static string port = "5432";
        public static string user = "postgres";
        public static string password = "senha123";
        public static string database = "MyControl";

        public static NpgsqlConnection GetConnection()
        {
            string con_string = "Server="+ server + ";Port="+ port + ";User Id="+ user + ";Password="+password+";Database="+database+";";
            return new NpgsqlConnection(con_string);
        }

    }
}
