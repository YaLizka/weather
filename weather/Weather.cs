using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HowIsTheWeather
{
    class WeatherAPI
    {
        public Weather GetWeather(string city)
        {
            try
            {
                WebRequest r = WebRequest.Create($"https://query.yahooapis.com/v1/public/yql?format=json&q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{city}\") and u=\'c\'");
                WebResponse w = r.GetResponse();
                string a = string.Empty;
                using (StreamReader c = new StreamReader(w.GetResponseStream()))
                    a = c.ReadToEnd();
                return JsonConvert.DeserializeObject<Weather>(a);
            }
            catch
            {
                MessageBox.Show("Невозможно загрузить погоду");
                return null;
            }
        }
    }
    class Weather
    {
        public Query Query { get; set; }
    }
    class Query
    {
        public int Count { get; set; }
        public Results Results { get; set; }
    }
    class Results
    {
        public Channel Channel { get; set; }
    }
    class Channel
    {
        public Units Units { get; set; }
        public Location Location { get; set; }
        public Wind Wind { get; set; }
        public Item Item { get; set; }
        public Atmosphere Atmosphere { get; set; }
    }
    class Atmosphere
    {
        public string Humidity { get; set; }
        public string Pressure { get; set; }
    }
    class Units
    {
        public string Distance { get; set; }
        public string Pressure { get; set; }
        public string Speed { get; set; }
        public string Temperature { get; set; }
    }
    class Location
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
    }
    class Wind
    {
        public float Speed { get; set; }
    }
    class Item
    {
        [JsonProperty(PropertyName = "condition")]
        public Condition Condition { get; set; }
        public List<Condition> Forecast { get; set; }
    }
    class Condition
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
        public int Temp { get; set; }
        public string Text { get; set; }
    }
}
