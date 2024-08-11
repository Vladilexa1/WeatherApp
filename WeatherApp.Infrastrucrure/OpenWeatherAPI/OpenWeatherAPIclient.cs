using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI.Entity;

namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public class OpenWeatherAPIclient : IOpenWeatherAPIclient
    {
        private static HttpClient sharedClient = new();
        private readonly IBildUrl BildUrl;
        public OpenWeatherAPIclient(IBildUrl bildUrl)
        {
            BildUrl = bildUrl;
        }
        public async Task<CurrentWeather> GetWeatherForName(string name) // TODO: проверить на налл, ...
        {
            var jsonResponse = await GetJsonResponse(BildUrl.GetCurrentWeatherForName(name));

            var weather = JsonSerializer.Deserialize<WeatherEntity>(jsonResponse) ?? throw new Exception("Problem bro");
            var result = CurrentWeather.Create
                (weather.coord.lat, weather.coord.lat, weather.weather[0].main, weather.weather[0].description,
                weather.main.temp, weather.main.feels_like, weather.main.pressure, weather.wind.speed, weather.wind.deg,
                weather.sys.country, weather.name
                );
            return result;
        }
        public async Task<Forecast> GetForecastForName(string name)
        {
            var jsonResponse = await GetJsonResponse(BildUrl.GetForecastForName(name));

            var forecast = JsonSerializer.Deserialize<Forecast>(jsonResponse) ?? throw new Exception("Problem bro");

            return forecast;
        }
        private async Task<string> GetJsonResponse(string URL)
        {
            var response = await sharedClient.GetAsync(URL);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }
}

