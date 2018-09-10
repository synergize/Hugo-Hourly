using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace historicalweather
{
    class Program
    {
        //Unique API Key provided from OpenWeatherMap

        static void Main(string[] args)
        {
            string zipInput = "29579";
            //int timeInput = Convert.ToInt32(Console.ReadLine());
            GetData(zipInput);

        }

         static void GetData(string zip)
        {
            string apiKey = System.Environment.GetEnvironmentVariable("WEATHER_KEY");
            string darkAPI = System.Environment.GetEnvironmentVariable("DARK_KEY");
            string url = "";

            using (WebClient web = new WebClient())
            {
                
                //Combines the zip code entered, the API key and a template for looking up via Zip code on the API. 
                url = string.Format($"http://api.openweathermap.org/data/2.5/weather?zip={zip},us&appid={apiKey}");

                var openWeather = web.DownloadString(url);
                    //Acquires the data from the URL above and stores it into json variable. 

                    var result = JsonConvert.DeserializeObject<GetWeather.RootObject>(openWeather);
                    GetWeather.RootObject openOutput = result;
                    url = string.Format($"https://api.darksky.net/forecast/{darkAPI}/{openOutput.Coord.Lat},{openOutput.Coord.Lon},622440000");
                    var darkWeather = web.DownloadString(url);
                    var darkResult = JsonConvert.DeserializeObject<GetDarkSky.RootObject>(darkWeather);
                    Console.WriteLine($"Hurrican Hugo Hourly Data Start Time: {DateTime("622440000")}");
                   Console.WriteLine("\n");
                    for (int i = 0; i < darkResult.hourly.data.Count; i++)
                {
                    Console.WriteLine($"Date and Timestamp Hourly: {DateTime(darkResult.hourly.data[i].time.ToString())}");
                    Console.WriteLine($"Weather Summary: {darkResult.hourly.data[i].summary}");
                    Console.WriteLine($"Temperature: {darkResult.hourly.data[i].temperature.ToString()}°F");
                    Console.WriteLine($"Humidity: {darkResult.hourly.data[i].humidity * 100}%");
                    Console.WriteLine($"Pressure: {darkResult.hourly.data[i].pressure} hPa");
                    Console.WriteLine($"Wind Speed: {darkResult.hourly.data[i].windSpeed} MPH");
                    Console.WriteLine($"Wind Gust: {darkResult.hourly.data[i].windGust} MPH");
                    Console.WriteLine($"Cloud Coverage: {darkResult.hourly.data[i].cloudCover * 100}%");
                    Console.WriteLine("\n");

                }
                Console.WriteLine("Powered by Dark Sky, https://darksky.net/poweredby/ and OpenWeatherMap, https://openweathermap.org/");
                Console.ReadLine();







            }
        }
        static string Validation(string json)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    //openUrl = string.Format($"http://api.openweathermap.org/data/2.5/weather?zip={zipInput},us&appid={apiKey}");
                   // validate = true;

                }
                return json;
            }
            catch (WebException wex)
            {
                if (wex.Source != null)
                {
                    Console.WriteLine("Please enter a valid Zip Code");

                }
                //validate = false;
            }
            return null;
        } //Input validation to ensure a zip code is added.

        static string DateTime(string input)
        {
            DateTime Time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return Time.AddSeconds(Convert.ToDouble(input)).ToString();
            //return Time.AddSeconds(Convert.ToDouble(input)).ToLocalTime().ToString("yyyyMMddTHH:mm:ssZ");
        }//Converted time input and outputs to readable format.
    }
}
