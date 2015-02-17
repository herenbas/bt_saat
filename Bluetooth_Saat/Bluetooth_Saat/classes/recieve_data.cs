using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bluetooth_Saat.classes
{
    class recieve_data
    {
        public string gelen_isi { get; set; }
        public string gelen_nem { get; set; }
        public string gelen_saat { get; set; }


        public string isi_yaz()

        {
            SqlConnection con = (Classes.Connection_Class.GetConnection());
            SqlCommand com = new SqlCommand("insert into deger (sicaklik,nem) values (@sicaklik,@nem)", con);

            com.Parameters.AddWithValue("@sicaklik", gelen_isi);
            com.Parameters.AddWithValue("@nem", gelen_nem);

            int etki = com.ExecuteNonQuery();
            if (etki>0)
            {
               //başarılı kayıt
            }
            else
            {
                //başarısız kayıt
            }

            return gelen_isi;
        
        
        
        }
        public string nem_yaz()
        {


            SqlConnection con = (Classes.Connection_Class.GetConnection());
            SqlCommand com = new SqlCommand("insert into deger (nem) values (@nem)", con);

            com.Parameters.AddWithValue("@nem", gelen_nem);

            int etki = com.ExecuteNonQuery();
            if (etki > 0)
            {
                //başarılı kayıt
            }
            else
            {
                //başarısız kayıt
            }

            return gelen_nem;        
        
        }
        public void hop()
        {
            
          

        
        
        }

    }
}
