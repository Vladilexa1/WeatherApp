using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.Application
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherAPIclient _openWeatherAPIclient;
        private const int HOUR_CHECK_WEATHER = 12;
        public WeatherService(IOpenWeatherAPIclient openWeatherAPIclient)
        {
            _openWeatherAPIclient = openWeatherAPIclient;
        }
        public async Task<List<Forecast>> GetForecast(decimal latitude, decimal longitude)
        {
            var forecast = await _openWeatherAPIclient.GetForecastForCoordinates(latitude, longitude);
            List<Forecast> forecastList = new()
            {
                forecast[0]
            };
            int j = 1;
            for (int i = 1; i < forecast.Count; i++)
            {
                if (forecast[i].DateTime.Day == DateTime.Now.AddDays(j).Day)
                {
                    if (forecast[i].DateTime.Hour == HOUR_CHECK_WEATHER)
                    {
                        forecastList.Add(forecast[i]);
                        j++;
                    }
                }
            }
            return forecastList;
        }
        public async Task<Weather> GetWeatherForName(string city)
        {
            return await _openWeatherAPIclient.GetWeatherForName(city);
        }
    }
}
