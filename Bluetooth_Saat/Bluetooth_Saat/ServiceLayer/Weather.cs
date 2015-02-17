using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace YahooWeatherForecast
{
   
    public class Weather
    {
      

          public string city_id { get; set; }
        public string LocationId = "" ;
        
        private string baseUrl = "http://weather.yahooapis.com/forecastrss?&";

        private bool isMetric = false;
       
        public bool IsMetric
        {
            get { return isMetric; }
        }

       
        public enum TemperatureUnits
        {
            Celcius,
            Fahrenheit,
            Null
        }
       
        public enum DistanceUnits
        {
            Miles,
            Kilometeres,
            Null
        }
       
        public enum PressureUnits
        {
            PoundsPerSquareInch,
            Millibars,
            Null
        }
       
        public enum SpeedUnits
        {
            MilesPerHour,
            KilometersPerHour,
            Null
        }
        
       
        public enum BarometricPressure
        {
            Steady=0,Rising=1,Falling=2
        }
       
        public enum ConditionCodes
        {
             Kasýrga= 0,
            TropikKasýrga=1,
            Kasýrga1=2,
            SeverThunderstorms=3,
            Thunderstorms=4,
            KarlaKarýþýkYaðmur=5,
            KarlaKarýþýkYaðmur1 = 6,
            KarlaKarýþýkYaðmur2 = 7,
            Çið=8,
            HafifYaðmur=9,
            HafifYaðmur1=10,
            Yaðmur=11,
            Yaðmur1=12,
            HafifKarlý=13,
            HafifKarlý1=14,
            RüzgarlýKar=15,
            Karlý=16,
            Dolu=17,
            KarlaKarýþýkYaðmur3=18,
            Toz=19,
            Sisli=20,
            Puslu=21,
            Sisli1=22,
            Blustery=23,
            Rüzgarlý=24,
            Soðuk=25,
            Bulutlu=26,
            Bulutlugece=27,
            BulutluGündüz=28,
            ParçalýBulutluGece=29,
            ParçalýBulutluGündüz=30,
            Açýkgece=31,
            Güneþli=32,
            FairNight=33,
            FairDay=34,
            MixedRainAndHail=35,
            Sýcak=36,
            IsolatedThunderstorms=37,
            ScatteredThunderstorms1=38,
            ScatteredThunderstorms2=39,
            ScatteredShowers=40,
            HeavySnow1=41,
            ScatteredSnowShowers=42,
            Tipi=43,
            ParçalýBulutlu=44,
            Tipi1=45,
            SnowShowers=46,
            IsolatedThundershowers=47,
            nodata=3200
        }
    
        

      
        public Location Location = new Location("", "", "");
        
        public Units Units = new Units(TemperatureUnits.Null, DistanceUnits.Null, PressureUnits.Null, SpeedUnits.Null);
      
        public Wind Wind = new Wind(-1,-1,-1);
      
        public Atmosphere Atmosphere = new Atmosphere(-1, -1, -1, 0);
    
        public Astronomy Astronomy = new Astronomy(new DateTime(), new DateTime());
        
        public Geography Geography = new Geography(-1, -1);
     
        public Condition Condition = new Condition("", (ConditionCodes)3200, -1, new DateTime());

        public Forecast Forecast = new Forecast();


      
        public Weather(string location, TemperatureUnits units)
        {
            LocationId = location;
            Units.Temperature = units;
            Refresh();
        }
        
       
        public void Refresh()
        {
          
            bool isInGeoLattitude = false;

          
            bool isInGeoLongitude = false;

        
            DateTime date;
            ConditionCodes code;
            int low, high, rising, conditionCode;
            string text, temp;
            

         
            string forecastUrl;

           
            if (Units.Temperature == TemperatureUnits.Celcius)
            {
                forecastUrl = baseUrl + "p=" + LocationId + "&u=c";
                isMetric = true;
            }
            else
            {
                forecastUrl = baseUrl + "p=" + LocationId + "&u=f";
                isMetric = false;
            }

        
            Forecast.Days.Clear();

        
              
                XmlTextReader reader = new XmlTextReader(forecastUrl);
        
                while (reader.Read())
                {
                
                    switch (reader.NodeType)
                    {
                       
                        case XmlNodeType.Element:
                            
                            if (reader.Name.ToLower() == "yweather:location")
                            {
                                Location.City = reader.GetAttribute("city");
                                Location.Region = reader.GetAttribute("region");
                                Location.Country = reader.GetAttribute("country");
                            }
                            if (reader.Name.ToLower() == "yweather:units")
                            {
                              
                                temp = reader.GetAttribute("temperature").ToLower();
                              
                                if (temp == "c")
                                {
                                    Units.Temperature = TemperatureUnits.Celcius;
                                }
                                else
                                {
                                    Units.Temperature = TemperatureUnits.Fahrenheit;
                                }

                                temp = reader.GetAttribute("distance");
                                if (temp == "km")
                                {
                                    Units.Distance = DistanceUnits.Kilometeres;
                                }
                                else
                                {
                                    Units.Distance = DistanceUnits.Miles;
                                }

                                temp = reader.GetAttribute("pressure");
                                if (temp == "mb")
                                {
                                    Units.Pressure = PressureUnits.Millibars;
                                }
                                else
                                {
                                    Units.Pressure = PressureUnits.PoundsPerSquareInch;
                                }

                                temp = reader.GetAttribute("speed");
                                if (temp == "kph")
                                {
                                    Units.Speed = SpeedUnits.KilometersPerHour;
                                }
                                else
                                {
                                    Units.Speed = SpeedUnits.MilesPerHour;
                                }
                            }
                            if (reader.Name.ToLower() == "yweather:wind")
                            {
                                Wind.Chill = Convert.ToInt32(reader.GetAttribute("chill"));
                                Wind.Direction = Convert.ToInt32(reader.GetAttribute("direction"));
                               
                            }
                            if (reader.Name.ToLower() == "yweather:atmosphere")
                            {
                                Atmosphere.Humidity = Convert.ToInt32(reader.GetAttribute("humidity"));
                               
                                Atmosphere.Pressure = Convert.ToDouble(reader.GetAttribute("pressure"));
                                rising = Convert.ToInt32(reader.GetAttribute("rising"));
                                Atmosphere.Rising = (BarometricPressure)rising;
                            }
                            if (reader.Name.ToLower() == "yweather:astronomy")
                            {
                                Astronomy.Sunrise = Convert.ToDateTime(reader.GetAttribute("sunrise"));
                                Astronomy.Sunset = Convert.ToDateTime(reader.GetAttribute("sunset"));
                            }
                            if (reader.Name.ToLower() == "yweather:condition")
                            {
                                Condition.Text = reader.GetAttribute("text");
                                conditionCode = Convert.ToInt32(reader.GetAttribute("code"));
                               Condition.Code = (ConditionCodes)conditionCode;
                                Condition.Temperature = Convert.ToInt32(reader.GetAttribute("temp"));
                                Condition.Date = ParseDateTime(reader.GetAttribute("date"));
                            }
                            if (reader.Name.ToLower() == "yweather:forecast")
                            {
                                date = Convert.ToDateTime(reader.GetAttribute("date"));
                                low = Convert.ToInt32(reader.GetAttribute("low"));
                                high = Convert.ToInt32(reader.GetAttribute("high"));
                                text = reader.GetAttribute("text");
                                code = (ConditionCodes)Convert.ToInt32(reader.GetAttribute("code"));
                                Forecast.Days.Add(new ForecastDay(date, low, high, text, code));
                            }
                          
                            if (reader.Name.ToLower() == "geo:long")
                            {
                                isInGeoLongitude = true;
                            }
                           
                            if (reader.Name.ToLower() == "geo:lat")
                            {
                                isInGeoLattitude = true;
                            }
                            break;
                       
                        case XmlNodeType.Text:
                            
                            if (isInGeoLattitude)
                            {
                              
                                Geography.Lattitude = Convert.ToDecimal(reader.Value);
                            }
                            
                            if (isInGeoLongitude)
                            {
                            
                                Geography.Longitude = Convert.ToDecimal(reader.Value);
                            }
                            break;
                      
                        case XmlNodeType.EndElement:
                           
                            if (reader.Name.ToLower() == "geo:long")
                            {
                                isInGeoLongitude = false;
                            }
                            
                            if (reader.Name.ToLower() == "geo:lat")
                            {
                                isInGeoLattitude = false;
                            }
                            break;
                    }
                }
          
        }

       
        private DateTime ParseDateTime(string s)
        {
           
            int month,day,year,hour,minute;

          
            string[] arr = s.Split(' ');
      
            string[] arrTime = arr[4].Split(':');

          
            DateTime dt;


            day = Convert.ToInt32(arr[1]);
            year = Convert.ToInt32(arr[3]);
            hour = Convert.ToInt32(arrTime[0]);
            minute = Convert.ToInt32(arrTime[1]);

         
            if (arr[5].ToLower() == "pm")
            {
                hour = hour + 12;  
            }

        
            switch (arr[2])
            {
                case "Jan":
                    month = 1;
                    break;
                case "Feb":
                    month = 2;
                    break;
                case "Mar":
                    month = 3;
                    break;
                case "Apr":
                    month = 4;
                    break;
                case "May":
                    month = 5;
                    break;
                case "Jun":
                    month = 6;
                    break;
                case "Jul":
                    month = 7;
                    break;
                case "Aug":
                    month = 8;
                    break;
                case "Sep":
                    month = 9;
                    break;
                case "Oct":
                    month = 10;
                    break;
                case "Nov":
                    month = 11;
                    break;
                case "Dec":
                    month = 12;
                    break;
                default:
                    month = 0;
                    break;
            }

         
            dt = new DateTime(year, month, day, hour, minute, 0);

            return dt;
        }
    }

  
    public class Location
    {
     
        public string City = "";
     
        public string Region = "";
      
        public string Country = "";

      
        public Location(string city, string region, string country)
        {
            City = city;
            Region = region;
            Country = country;
        }
    }

   
    public class Units
    {
    
        public Weather.TemperatureUnits Temperature = Weather.TemperatureUnits.Null;
      
        public Weather.DistanceUnits Distance = Weather.DistanceUnits.Null;
      
        public Weather.PressureUnits Pressure = Weather.PressureUnits.Null;
       
        public Weather.SpeedUnits Speed = Weather.SpeedUnits.Null;

       
        public Units(Weather.TemperatureUnits temperature, Weather.DistanceUnits distance, Weather.PressureUnits pressure, Weather.SpeedUnits speed)
        {
            Temperature = temperature;
            Distance = distance;
            Pressure = pressure;
            Speed = speed;
        }

       
        public string GetTemperatureUnitAbbreviation()
        {
            switch (Temperature)
            {
                case Weather.TemperatureUnits.Celcius:
                    return "C";
                case Weather.TemperatureUnits.Fahrenheit:
                    return "F";
            }
            return null;
        }
      
        public string GetDistanceUnitAbbreviation()
        {
            switch (Distance)
            {
                case Weather.DistanceUnits.Kilometeres:
                    return "KM";
                case Weather.DistanceUnits.Miles:
                    return "MI";
            }
            return null;
        }
     
        public string GetPressureUnitAbbreviation()
        {
            switch (Pressure)
            {
                case Weather.PressureUnits.Millibars:
                    return "MB";
                case Weather.PressureUnits.PoundsPerSquareInch:
                    return "PSI";
            }
            return null;
        }
       
        public string GetSpeedUnitAbbreviation()
        {
            switch (Speed)
            {
                case Weather.SpeedUnits.KilometersPerHour:
                    return "KMH";
                case Weather.SpeedUnits.MilesPerHour:
                    return "MPH";
            }
            return null;
        }
    }

   
    public class Wind
    {
      
        public int Chill = -1;
        
        public int Direction = -1;
       
        public int Speed = -1;

       
        public Wind(int chill, int direction, int speed)
        {
            Chill = chill;
            Direction = direction;
            Speed = speed;
        }
    }

    public class Atmosphere
    {
       
        public int Humidity = -1;
        
        private decimal dVisibility = -1;
        public decimal Visibility
        {
            get
            {
                return dVisibility;
            }
            set
            {
                dVisibility = (decimal)value/(decimal)100;
            }
        }
       
        public double Pressure = -1;
        
        public Weather.BarometricPressure Rising = 0;

        public Atmosphere(int humidity, int visibility, double pressure, Weather.BarometricPressure rising)
        {
            Humidity = humidity;
            Visibility = (decimal)visibility/(decimal)100;
            Pressure = pressure;
            Rising = rising;
        }
    }

   
    public class Astronomy
    {
        
        public DateTime Sunrise = new DateTime();
       
        public DateTime Sunset = new DateTime();

        
        public Astronomy(DateTime sunrise, DateTime sunset)
        {
            Sunrise = sunrise;
            Sunset = sunset;
        }
    }

   
    public class Geography
    {
     
        public decimal Lattitude = -1;
       
        public decimal Longitude = -1;

     
        public Geography(decimal lattitude, decimal longitude)
        {
            Lattitude = lattitude;
            Longitude = longitude;
        }
    }

   
    public class Condition
    {
       
        public string Text = "";
       
        public Weather.ConditionCodes Code = (Weather.ConditionCodes)3200;
        
        public int Temperature = -1;
       
        public DateTime Date = new DateTime();

       
        public Condition(string text, Weather.ConditionCodes code, int temperature, DateTime date)
        {
            Text = text;
            Code = code;
            Temperature = temperature;
            Date = date;
        }
    }

   
    public class ForecastDay
    {
       
        public DateTime Date = new DateTime();
       
        public int Low = -1;
       
        public int High = -1;
       
        public string Text = "";
        
        public Weather.ConditionCodes Code = (Weather.ConditionCodes)3200;

        
        public ForecastDay(DateTime date, int low, int high, string text, Weather.ConditionCodes code)
        {
            Date = date;
            Low = low;
            High = high;
            Text = text;
            Code = code;
        }
    }

    
    public class ForecastDayCollection : CollectionBase
    {
        public virtual void Add(ForecastDay newForecastDay)
        {
            this.List.Add(newForecastDay);
        }
        public virtual ForecastDay this[int Index]
        {
            get
            {
                return (ForecastDay)this.List[Index];
            }
        }
    }

   
    public class Forecast
    {
        
        public ForecastDayCollection Days = new ForecastDayCollection();
       
        public Forecast()
        {

        }
    }
}
