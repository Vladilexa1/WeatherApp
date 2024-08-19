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
    public class OpenWeatherAPIclient : IOpenWeatherAPIclient // cods error
    {
        private static HttpClient sharedClient = new();
        private readonly IBildUrl BildUrl;
        public OpenWeatherAPIclient(IBildUrl bildUrl)
        {
            BildUrl = bildUrl;
        }
        public async Task<Core.Weather> GetWeatherForName(string name) // TODO: проверить на налл, ...
        {
            var jsonResponse = await GetJsonResponse(BildUrl.GetCurrentWeatherForName(name));

            var weather = JsonSerializer.Deserialize<WeatherEntity>(jsonResponse) ?? throw new Exception("Problem deserialize");
            var result = Core.Weather.Create
                (weather.coord.lat, weather.coord.lon, weather.weather[0].main, weather.weather[0].description,
                weather.main.temp, weather.main.feels_like, weather.main.pressure, weather.wind.speed, weather.wind.deg,
                weather.sys.country, weather.name
                );
            return result;
        }
        public async Task<List<Forecast>> GetForecastForCoordinates(decimal latitude, decimal longitude)
        {
            var jsonResponse = await GetJsonResponse(BildUrl.GetForecastForCoordinates(latitude, longitude));

            var forecast = JsonSerializer.Deserialize<ForecastEntity>(jsonResponse) ?? throw new Exception("Problem deserialize");
            List<Forecast> result = new();
            foreach (var item in forecast.list)
            {
                result.Add(Forecast.Create(item.main.temp, item.main.feels_like, item.weather[0].main, item.weather[0].description, item.wind.speed, item.wind.deg, item.dt_txt,
                    forecast.city.name, forecast.city.coord.lat, forecast.city.coord.lon, forecast.city.country));
            }

            return result;
        }
        private async Task<string> GetJsonResponse(string URL)
        {
            var response = sharedClient.GetAsync(URL).Result;
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }
}

