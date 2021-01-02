using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public static void saveBackup()
        {
            string backupFolderName = Directory.GetCurrentDirectory() + "\\db_backups";
            Directory.CreateDirectory(backupFolderName);
            string backupFileName = "PostgresV96_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".dump";
            string comando = "set PGPASSWORD=" + password + "\n" + "pg_dump -f \"" + backupFolderName + "\\" + backupFileName + "\" -h "+server+ " -U " + user + " " + database + "";
            StreamWriter sw = new StreamWriter("save-backup.bat");
            sw.Write(comando);
            sw.Close();

            // MessageBox.Show(comando);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "save-backup.bat";
            process.StartInfo = startInfo;
            process.Start();
        }

    }
}
