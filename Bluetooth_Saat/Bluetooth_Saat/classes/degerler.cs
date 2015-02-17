using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bluetooth_Saat.Classes //degerler classı veritabanına yazacağımız değerlerin metodlarını yazdığımız classtır
{
    class degerler
    {

        public string yuksek_isi { get; set; }
        public string yuksek_nem { get; set; }
        public string dusuk_isi { get; set; }
        public string dusuk_nem { get; set; }
        public string alarm_time { get; set; }
        public int Alarm_stat { get; set; }
        public string set_alarm_times { get; set; }
        public string isi_tahmin_1 { get; set; }
        public string isi_tahmin_2 { get; set; }
        public string isi_tahmin_3 { get; set; }
        public string nem_tahmin_1 { get; set; }
        public string nem_tahmin_2 { get; set; }
        public string nem_tahmin_3 { get; set; }

        public string en_yuksek_isi() // ısı sensöründen aldığımız değerleri veritabanına yazdıktan sonra değerler arasında en yüksek ısı değerini çektiğimiz metod
        {
            SqlConnection con = Classes.Connection_Class.GetConnection();
            SqlCommand com = new SqlCommand("select sicaklik from deger order by sicaklik asc ", con);
            
            SqlDataReader rdr = com.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    yuksek_isi = rdr["sicaklik"].ToString(); ;
                }
            }
            rdr.Close();
            return yuksek_isi;


        }
        public string en_dusuk_isi() //ısı sensöründen aldığımız değerleri veritabanına yazdıktan snra değerler arasından en düşük ısı değerini çektiğimiz metod
        {
            SqlConnection con = Classes.Connection_Class.GetConnection();
            SqlCommand com = new SqlCommand("select sicaklik from deger order by sicaklik desc ", con);

            SqlDataReader rdr = com.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    dusuk_isi = rdr["sicaklik"].ToString(); ;
                }
            }
            rdr.Close();
            return dusuk_isi;


        }

        public string en_yuksek_nem() //en yüksek nem değerini çektiğimiz metod
        {
            SqlConnection con = Classes.Connection_Class.GetConnection();
            SqlCommand com = new SqlCommand("select nem from deger order by nem asc ", con);

            SqlDataReader rdr = com.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                yuksek_nem = rdr["nem"].ToString(); ;

            }
            rdr.Close();
            return yuksek_nem;


        }

        public string en_dusuk_nem() //en düşük nem miktarını çektiğimiz metod
        {
            SqlConnection con = Classes.Connection_Class.GetConnection();
            SqlCommand com = new SqlCommand("select nem from deger order by nem desc ", con);

            SqlDataReader rdr = com.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                dusuk_nem = (rdr["nem"].ToString());

            }
            rdr.Close();
            return dusuk_nem;


        }

        public string get_alarm() //alarm kurulduktan sonra veritabanına yazılan alarma ait gün ve saat bilgisini çektiğimiz metod
        {
            SqlConnection con = Classes.Connection_Class.GetConnection();
            SqlCommand com = new SqlCommand("select * from Alarms where id=1", con);

            SqlDataReader rdr = com.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                alarm_time = rdr["alarm"].ToString();
                Alarm_stat =Convert.ToInt32( rdr["alarm_status"]);

            }

            rdr.Close();
            return alarm_time;


        }

        public string set_alarm() //alarm kurulumu yapılırken kullanıcnın seçtiği gin ve saat bilgisini veritabanına yazan metod
        {
            SqlConnection con = Classes.Connection_Class.GetConnection();

            SqlCommand com = new SqlCommand("update Alarms set alarm=@alarm,alarm_status=alarm_status where id=1", con);

            com.Parameters.AddWithValue("@alarm", set_alarm_times);
            com.Parameters.AddWithValue("@alarm_status", Alarm_stat);
            com.ExecuteNonQuery();
            return alarm_time;
        }




    }
}
