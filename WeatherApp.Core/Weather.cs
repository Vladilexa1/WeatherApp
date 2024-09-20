using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Weather
    {
        private Weather(decimal latitude, decimal longitude, 
            string currentWeather, string description, decimal temp, 
            decimal feelsLike, decimal pressure, decimal windSpeed, 
            string deg, string country, string sityName, string icon)
        {
            Latitude = latitude;
            Longitude = longitude;
            CurrentWeather = currentWeather;
            Description = description;
            Temp = temp;
            FeelsLike = feelsLike;
            Pressure = pressure;
            WindSpeed = windSpeed;
            Deg = deg;
            Country = country;
            SityName = sityName;
            Icon = icon;
        }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public string CurrentWeather { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Temp { get; }
        public decimal FeelsLike { get; }
        public decimal Pressure { get; }
        public decimal WindSpeed { get; }
        public string Deg { get; } = string.Empty;
        public string Country { get; } = string.Empty;
        public string SityName { get; } = string.Empty;
        public string Icon { get; } = string.Empty;

        public static Weather Create(decimal latitude, decimal longitude,
            string currentWeather, string description, decimal temp,
            decimal feelsLike, decimal pressure, decimal windSpeed,
            double deg, string country, string sityName, string icon)
        {
            var result = new Weather(latitude, longitude, currentWeather, 
                description, temp, feelsLike, pressure, windSpeed, ConvertDegToСardinalDirections(deg), country, sityName, icon);
            return result;
        }
        private static string ConvertDegToСardinalDirections(double deg) // DDRY
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
