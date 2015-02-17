using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.Pivot.Legacy;
using Telerik.WinControls;
using System.Threading;
using YahooWeatherForecast;

namespace Bluetooth_Saat
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        public RadForm1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public string en_yuksek_isi_1;
        public string en_yuksek_isi_2;
        public string en_yuksek_isi_3;
        public string en_dusuk_isi_1;
        public string en_dusuk_isi_2;
        public string en_dusuk_isi_3;
        public string en_yuksek_nem_1;
        public string en_yuksek_nem_2;
        public string en_yuksek_nem_3;
        public string en_dusuk_nem_1;
        public string en_dusuk_nem_2;
        public string en_dusuk_nem_3;
        public string gunes_dog;
        public string gunes_bat;
        public string gun_durum;
        classes.recieve_data data = new classes.recieve_data();
        Weather weather;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        Classes.degerler olcum_degerleri = new Classes.degerler();

        private void RadForm1_Load(object sender, EventArgs e)
        {

            // backgroundWorker1.RunWorkerAsync();
            radLabel6.Text = DateTime.Now.ToShortDateString();

           pictureBox1.Visible = false;
            
            try
            {
                port.Open();


                if (port.IsOpen)
                {
                    radLabel9.Text = "OK";
                    radLabel9.ForeColor = Color.Green;
                    //port.WriteLine("1");




                }
                else
                {
                    radLabel9.Text = "NOT OK";
                    radLabel9.ForeColor = Color.Red;
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("There is a connection error" + hata.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                radLabel9.Text = "NOT OK";
                radLabel9.ForeColor = Color.Red;
            }


            radLabel5.Visible = false;
            radLabel4.Visible = false;
            olcum_degerleri.get_alarm();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            radLabel5.Visible = true;
            radLabel4.Visible = true;
            radLabel5.Text = radTimePicker1.Value.Value.ToShortTimeString();
            olcum_degerleri.alarm_time = radTimePicker1.Value.Value.ToString(); //degerler classında oluşturduğumuz alarm time propertie değerine saat datasını gönderiyoruz

            // backgroundWorker1.RunWorkerAsync(); //timer her 1 saniyede veritabaınındaki saat değeri ile sistemin saatini karşılaştırarak alarmın ne zaman aktif olacağı konusunda yardımcı olur
            timer1.Start();
            olcum_degerleri.set_alarm_times = DateTime.Now.ToShortDateString() + " " + radTimePicker1.Value.Value.ToShortTimeString();
            ////degerler classında oluşturduğumuz set_alarm_time propertie değerine saat datasını gönderiyoruz
            olcum_degerleri.set_alarm(); //set_aram_times propertie değerini atatdıktan sonra set_alarm metodu ile saat bilgisini veritabanına gönderiyoruz
            port.Write(radLabel5.Text);

        }

        private void splitPanel2_Click(object sender, EventArgs e)
        {

        }
        private void showWeather(Weather weather)
        { 
            try
            {
                radLabel23.Text = weather.Condition.Temperature.ToString()+" °C" ;                

                radLabel25.Text = weather.Condition.Text; 
                        // weather.Condition.Code.ToString()  ---- mevcut hava durumunun türkçesini verir
                en_yuksek_isi_1 = weather.Forecast.Days[1].High.ToString();                  
                en_yuksek_isi_2 = weather.Forecast.Days[1].High.ToString();                 
                en_yuksek_isi_3 = weather.Forecast.Days[3].High.ToString();                  
                en_dusuk_isi_1 =  weather.Forecast.Days[1].Low.ToString();               
                en_dusuk_isi_2 =  weather.Forecast.Days[2].Low.ToString();
                en_dusuk_isi_3 =  weather.Forecast.Days[3].Low.ToString();                  
                en_yuksek_nem_1 = null;                   
                en_yuksek_nem_2 = null;                 
                en_yuksek_nem_3 = null;                  
                en_dusuk_nem_1 =  null;                 
                en_dusuk_nem_2 =  null;                  
                en_dusuk_nem_3 =  null;                 
                gunes_dog =   weather.Astronomy.Sunrise.ToShortTimeString();                    
                gunes_bat = weather.Astronomy.Sunset.ToShortTimeString();                      
            
                string mevcut_saat = DateTime.Now.ToShortTimeString();
                if (DateTime.Parse(mevcut_saat) > DateTime.Parse(gunes_dog) && DateTime.Parse(mevcut_saat) < DateTime.Parse(gunes_bat))
                {
                    gun_durum = "GUNDUZ";
                }
                else
                {
                    gun_durum="GECE";
                }



                if (weather.Condition.Code.ToString().Contains("SAĞANAK") || weather.Condition.Code.ToString().Contains("HAFİF YAĞMURLU"))
                {
                    if (gun_durum == "GUNDUZ")
                    {
                        pictureBox1.ImageLocation = @"img\yagmurlu.png";
                    }
                    else
                    {
                        pictureBox1.ImageLocation = @"img\gece_yagmurlu.png";
                    }

                }
                else if (weather.Condition.Code.ToString().Contains("KARLA"))
                {

                    pictureBox1.ImageLocation = @"img\karka_karisik.png";
                }
                else if (weather.Condition.Code.ToString().Contains("KAR"))
                {
                    if (gun_durum == "GUNDUZ")
                    {
                        pictureBox1.ImageLocation = @"img\kar.png";
                    }
                    else
                    {
                        pictureBox1.ImageLocation = @"img\gece_karli.png";
                    }

                }
                else if ((weather.Condition.Code.ToString()).Contains("ÇOK BULUTLU"))
                {
                    if (gun_durum == "GUNDUZ")
                    {
                        pictureBox1.ImageLocation = @"img\bulutlu.png";
                    }
                    else
                    {
                        pictureBox1.ImageLocation = @"img\gece_bulutlu.png";
                    }

                }
                else if (weather.Condition.Code.ToString().Contains("AZ BULUTLU"))
                {
                    if (gun_durum == "GUNDUZ")
                    {
                        pictureBox1.ImageLocation = @"img\parcali_bulutlu.png";
                    }
                    else
                    {
                        pictureBox1.ImageLocation = @"img\gece_bulutlu.png";
                    }

                }
                else if ((weather.Condition.Code.ToString().Contains("Bulutlu")))
                {
                    if (gun_durum == "GUNDUZ")
                    {
                        pictureBox1.ImageLocation = @"img\parcali_bulutlu.png";
                    }
                    else
                    {
                        pictureBox1.ImageLocation = @"img\gece_bulutlu.png";
                    }
                }
                else if (weather.Condition.Code.ToString().Contains("Güneşli"))
                {
                    
                    if (gun_durum == "GUNDUZ")
                    {
                        pictureBox1.ImageLocation = @"img\gunesli.png";
                    }
                    else
                    {
                        pictureBox1.ImageLocation = @"img\ggunesli.png";
                    }
                }
                else if (weather.Condition.Text=="Mist")
                {
                    pictureBox1.ImageLocation = @"img\22.png";
                    
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("There is a web service error, please try again later" + hata.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            weather.Refresh();
}


        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
           
        
            try
            {
                pictureBox1.Visible = true;
                
                string[] city = radDropDownList1.SelectedItem.ToString().Split('-');
                string city_code = city[1].ToString();
              
                
                weather = new Weather(city_code, Weather.TemperatureUnits.Celcius);
                showWeather(weather);




            }
            catch (Exception m)
            {
               
            }
        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

      

        private void radLabel5_Click(object sender, EventArgs e)
        {

        }

        private void radLabel9_Click(object sender, EventArgs e)
        {

        }

        private void radLabel22_Click(object sender, EventArgs e)
        {

        }

        private void radLabel25_Click(object sender, EventArgs e)
        {

        }

        private void radPanel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
           
                string zaman = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                {
                    if (zaman == olcum_degerleri.set_alarm_times)
                    {
                        // System.Diagnostics.Process.Start(@"C:\Users\oracle\Desktop\kop.mp3");
                        port.Write("1");
                        // MessageBox.Show("Test");


                    }
                    else
                    {
                        port.Write("0");
                    }
                
            }
        }
          
           
           



        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            olcum_degerleri.en_yuksek_isi(); //veritabanına kaydedilen en yüksek ısı değerini degerler classı aracılığı ile çekeriz
            olcum_degerleri.en_dusuk_isi();//veritabanına kaydedilen en düşük ısı değerini degerler classı aracılığı ile çekeriz
            olcum_degerleri.en_yuksek_nem();//veritabanına kaydedilen en yüksek nem değerini degerler classı aracılığı ile çekeriz
            olcum_degerleri.en_dusuk_nem();//veritabanına kaydedilen en düşük nem değerini degerler classı aracılığı ile çekeriz


            radLabel15.Text = olcum_degerleri.yuksek_isi.ToString() + "°C";//veritabanından gelen bilgileri arayüzde gösteririz
            radLabel17.Text = olcum_degerleri.dusuk_isi.ToString() + "°C";
            radLabel19.Text = "% " + olcum_degerleri.dusuk_nem.ToString();
            radLabel20.Text = "% " + olcum_degerleri.yuksek_nem.ToString();
            data.hop();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // timer1.Start();

        }

        private void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (port.IsOpen)
                {
                    if (port.ReadLine().Contains("-99.50\r"))
                    {
                        radLabel10.Text = "----";
                        radLabel12.Text = "----";
                    }
                    else
                    {
                        string[] gelen_tum_veri = port.ReadLine().Split('*');
                        string [] gelen_veri = gelen_tum_veri[0].Split('-');
                        radLabel10.Text = gelen_veri[0].ToString(); ;// +" °C"; //radLabel23.Text = port.ReadLine() + " °C";
                        radLabel12.Text = gelen_veri[1].ToString();
                        radLabel2.Text = gelen_tum_veri[1].ToString();




                        data.gelen_isi = radLabel10.Text;
                        data.gelen_nem = radLabel12.Text;
                        data.isi_yaz();
                    }
                }
                else
                {

                    MessageBox.Show("There is a connection error" + "HATA", "HATAA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    radLabel9.Text = "NOT OK";
                    radLabel9.ForeColor = Color.Red;
                }
            }
            catch (Exception m  )
            {


                MessageBox.Show("There is a connection error" , "HATA:"+m.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
           
           
        }

        private void radButton2_Click(object sender, EventArgs e)
        {

            timer1.Stop();
            port.Write("0");


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

           // desktop alert üzerine gelen veriler yazılır

            radDesktopAlert1.ContentText += weather.Forecast.Days[1].Date.ToShortDateString()+" : "  + en_yuksek_isi_1 + " °C" + "     %" + en_yuksek_nem_1 + "    EN DÜŞÜK : " + en_dusuk_isi_1 + " °C" + "     %" + en_dusuk_nem_1 + "\n" + "\n";
            radDesktopAlert1.ContentText += weather.Forecast.Days[2].Date.ToShortDateString()+" : "  + en_yuksek_isi_2 + " °C" + "     %" + en_yuksek_nem_2 + "    EN DÜŞÜK : " + en_dusuk_isi_2 + " °C" + "     %" + en_dusuk_nem_2 +"\n" + "\n";
            radDesktopAlert1.ContentText += weather.Forecast.Days[3].Date.ToShortDateString()+" : "  + en_yuksek_isi_3 + " °C" + "     %" + en_yuksek_nem_3 + "    EN DÜŞÜK : " + en_dusuk_isi_3 + " °C" + "     %" + en_dusuk_nem_3;

            radDesktopAlert1.PlaySound = true;
            radDesktopAlert1.Show();
            
        }

        private void radDesktopAlert1_Closed(object sender, Telerik.WinControls.UI.RadPopupClosedEventArgs args)
        {
            radDesktopAlert1.ContentText = null;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            radDesktopAlert2.ContentText = null;
            radDesktopAlert2.Show();

            radDesktopAlert2.ContentImage = pictureBox1.Image;
            //radDesktopAlert2.ContentText.PadLeft(600);
            radDesktopAlert2.ContentText += "SUN RISE TIME      :" + weather.Astronomy.Sunrise+"\n"+"\n";
            radDesktopAlert2.ContentText += "SUN SET TIME       :" + weather.Astronomy.Sunset + " \n"+"\n";
            radDesktopAlert2.ContentText += "HUMIDITY           :" + " % "+weather.Atmosphere.Humidity +"\n"+"\n";
            radDesktopAlert2.ContentText +=" ATMOSPHERE PRESSURE:"+ weather.Atmosphere.Pressure + " "+weather.Units.Pressure+ "\n" + "\n";
            radDesktopAlert2.ContentText += "ATMOSPHERE RISING  :"+weather.Atmosphere.Rising + "\n" + "\n";
            radDesktopAlert2.ContentText += "CURRENT WEATHER (TURKISH)   :"+weather.Condition.Code + "\n" + "\n";
            radDesktopAlert2.ContentText += "CURENT DATE        :" +weather.Condition.Date + "\n" + "\n";
            radDesktopAlert2.ContentText += "CURRENT TEMPERATURE   :"+weather.Condition.Temperature +" "+weather.Units.Temperature+"\n" + "\n";
            radDesktopAlert2.ContentText += "CUURENT CONDITION  :"+weather.Condition.Text + "\n" + "\n";
            //radDesktopAlert2.ContentText += weather.Forecast.Days[1].Text + "\n" + "\n";
            radDesktopAlert2.ContentText += "LATTIUDE           :"+weather.Geography.Lattitude + "\n" + "\n";
            radDesktopAlert2.ContentText += "LONGITUDE          :"+weather.Geography.Longitude + "\n" + "\n";
            radDesktopAlert2.ContentText += "COUNTRY            :"+weather.Location.Country + "\n" + "\n";
            radDesktopAlert2.ContentText += "WIND CHILL         :"+weather.Wind.Chill.ToString()+" "+weather.Units.Temperature + "\n" + "\n";
            radDesktopAlert2.ContentText += "WIND DIRECTION     :"+weather.Wind.Direction + "\n" + "\n";
            radDesktopAlert2.ContentText += "WIND SPEED         :"+weather.Wind.Speed +" "+ weather.Units.Speed+"\n" + "\n";
            
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
           
            try
            {
                port.Open();


                if (port.IsOpen)
                {
                    radLabel9.Text = "OK";
                    radLabel9.ForeColor = Color.Green;
                    //port.WriteLine("1");




                }
                else
                {
                    radLabel9.Text = "NOT OK";
                    radLabel9.ForeColor = Color.Red;
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("There is a connection error" + hata.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }


            radLabel5.Visible = false;
            radLabel4.Visible = false;
            olcum_degerleri.get_alarm();
        }

        private void port_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            radLabel9.Text = "NOT OK";
            radLabel9.ForeColor = Color.Red;
            MessageBox.Show("There is a connection error - try to reconnect", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            port.Close();
        }

      
    }
}
