using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bluetooth_Saat.Classes
{
    class Connection_Class
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(tools.connection_string); //connection classı lokal veritabanı ile bağlantıının açık kapanmasını ve bağlantı durumunu kontrol eder
            try
            {

                
                if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                {
                    con.Open();
                }
                else
                {
                    con.Close();
                }
                
            }
                
            catch (Exception m)
            {

                System.Windows.Forms.MessageBox.Show(tools.db_error+" "+m.Message);

            }
            return con;
        }
    }
}
