using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluetooth_Saat.Classes
{
    class tools
    {
        
        public static string connection_string  //tools calssı uygulama kodlanırken kullanılacak olan string ifadeleri tutacağımız calasstır
        {
            get { return @"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\berenn\BlueTooth_Saat V.2 eng\Bluetooth_Saat\Bluetooth_Saat\Bluetooth_Saat\saat_db.mdf;Integrated Security=True"; }

            //C:\Users\oracle\Desktop\TEZ\hafta4\Bluetooth_Saat\Bluetooth_Saat
            
        }
        public static string db_error
        {


            get { return "Database Connection Error!"; } //örneğin buarada veritabanı hatası için yazdığımız uyarı mesajı var
        
        }
       
        
    }
}
