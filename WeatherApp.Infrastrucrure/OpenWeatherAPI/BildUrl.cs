using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public static class BildUrl
    {
        private const string URL_OPENWEATHER = "https://api.openweathermap.org/data/2.5/weather?";
        private const string API_KEY = "7b420c1d27c4ae89ed5c275c79df2a00"; // TODO: спрятать апи кей
        private const string UNITS = "&units=metric";
        public static string GetCoordForName(string name) =>
            $"{URL_OPENWEATHER}q={name}appid={API_KEY}&{UNITS}";

    }
}
