using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class CurrentWeather
    {
        private CurrentWeather(decimal latitude, decimal longitude, 
            string currentWeather, string description, decimal temp, 
            decimal feelsLike, decimal pressure, decimal windSpeed, 
            int deg, string country, string sityName)
        {
            Latitude = latitude;
            Longitude = longitude;
            CurrentWeath = currentWeather;
            Description = description;
            Temp = temp;
            FeelsLike = feelsLike;
            Pressure = pressure;
            WindSpeed = windSpeed;
            Deg = deg;
            Country = country;
            SityName = sityName;
        }

        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public string CurrentWeath { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Temp { get; }
        public decimal FeelsLike { get; }
        public decimal Pressure { get; }
        public decimal WindSpeed { get; }
        public int Deg { get; }
        public string Country { get; } = string.Empty;
        public string SityName { get; } = string.Empty;

        public static CurrentWeather Create(decimal latitude, decimal longitude,
            string currentWeather, string description, decimal temp,
            decimal feelsLike, decimal pressure, decimal windSpeed,
            int deg, string country, string sityName)
        {
            var result = new CurrentWeather(latitude, longitude, currentWeather, 
                description, temp, feelsLike, pressure, windSpeed, deg, country, sityName);
            return result;
        }
    }
    
}
