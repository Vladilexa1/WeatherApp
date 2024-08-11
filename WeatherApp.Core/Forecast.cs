using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Forecast
    {
        private Forecast(decimal temp,
                        decimal feelsLike,
                        string weather,
                        string description,
                        decimal windSpeed,
                        int deg,
                        DateTime dateTime,
                        string name,
                        decimal latitude,
                        decimal longitude,
                        string country)
        {
            Temp = temp;
            FeelsLike = feelsLike;
            Weather = weather;
            Description = description;
            WindSpeed = windSpeed;
            Deg = deg;
            DateTime = dateTime;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Country = country;
        }

        public decimal Temp { get; }
        public decimal FeelsLike { get; }
        public string Weather { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal WindSpeed { get; }
        public int Deg { get; }
        public DateTime DateTime { get; }
        public string Name { get; set; } = string.Empty;
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public string Country { get; set; } = string.Empty;
        public static Forecast Create(decimal temp,
                                        decimal feelsLike,
                                        string weather,
                                        string description,
                                        decimal windSpeed,
                                        int deg,
                                        string dt_txt,
                                        string name,
                                        decimal latitude,
                                        decimal longitude,
                                        string country)
        {
            var dateTime = DateTime.Parse(dt_txt);

            var result = new Forecast(temp,feelsLike,weather,description,windSpeed, deg,
                                 dateTime,name, latitude, longitude,country);
            return result;
        }
    }
}
