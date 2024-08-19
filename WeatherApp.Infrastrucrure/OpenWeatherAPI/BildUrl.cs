using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public class BildUrl : IBildUrl
    {
        private const string URL_OPENWEATHER = "https://api.openweathermap.org/data/2.5/";
        private const string CURRENT = "weather";
        private const string FORECAST = "forecast";
        private readonly string API_KEY;
        private const string UNITS = "metric";
        private BuildUrlOptions _options;
        public BildUrl(IOptions<BuildUrlOptions> options)
        {
            _options = options.Value;
            API_KEY = _options.Token;
        }
        public string GetCurrentWeatherForName(string name) =>
            $"{URL_OPENWEATHER}{CURRENT}?q={name}&units={UNITS}&appid={API_KEY}";
        public string GetForecastForCoordinates(decimal latitude, decimal longitude) =>
           $"{URL_OPENWEATHER}{FORECAST}?lat={latitude}&lon={longitude}&appid={API_KEY}&units={UNITS}";
    }
}
