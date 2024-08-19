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
                        string deg,
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
        public string Deg { get; } = string.Empty;
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
                                        double deg,
                                        string dt_txt,
                                        string name,
                                        decimal latitude,
                                        decimal longitude,
                                        string country)
        {
            var dateTime = DateTime.Parse(dt_txt);

            var result = new Forecast(temp,feelsLike,weather,description,windSpeed, ConvertDegToСardinalDirections(deg),
                                 dateTime,name, latitude, longitude,country);
            return result;
        }
        private static string ConvertDegToСardinalDirections(double deg)
        {
            /*
             * 22.5 - 67.5 - north-east 
             * 67.5 - 112.5 - east 
             * 112.5 - 157.5 - south-east 
             * 157.5 - 202.5 - south 
             * 202.5 - 247.5 - south-west 
             * 247.5 - 292.5 - west 
             * 292.5 - 337.5 - north-west 
             * 337.5 - 22.5 - north
             */
            if (22.5 < deg && deg <= 67.5) return "north-east";
            if (67.5 < deg && deg <= 112.5) return "east";
            if (112.5 < deg && deg <= 157.5) return "south-east";
            if (157.5 < deg && deg <= 202.5) return "south";
            if (202.5 < deg && deg <= 247.5) return "south-west";
            if (247.5 < deg && deg <= 292.5) return "west";
            if (292.5 < deg && deg <= 337.5) return "north-west";
            return "north";
        }
    }
}
