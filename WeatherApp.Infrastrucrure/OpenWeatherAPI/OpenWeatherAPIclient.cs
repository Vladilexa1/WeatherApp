using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI.Entity;
using WeatherApp.Infrastructure.OpenWeatherAPI.Exceptions;

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
        public async Task<Core.Weather> GetWeatherForName(string name)
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
            var forecast = JsonSerializer.Deserialize<ForecastEntity>(jsonResponse) ?? throw new Exception("Problem deserialize"); ;
            List<Forecast> result = new();
            foreach (var item in forecast.list)
            {
                result.Add(Forecast.Create(item.main.temp, item.main.feels_like, item.weather[0].main, item.weather[0].description, item.wind.speed, item.wind.deg, item.dt_txt,
                    forecast.city.name, forecast.city.coord.lat, forecast.city.coord.lon, forecast.city.country));
            }

            return result;
        }
        private void CheckException(HttpStatusCode statusCode)
        {
            if (((int)statusCode) == 404)
            {
                throw new CityNotFaundException("City not faund");
            }
            if (((int)statusCode) == 400)
            {
                throw new WrongCoordinatesException("Wrong coordinates");
            }
            if (((int)statusCode) != 200)
            {
                throw new ServiceNotAvailableException("Service not available, try later");
            }
        }
        private async Task<string> GetJsonResponse(string URL)
        {
            var response = sharedClient.GetAsync(URL).Result;
            CheckException(response.StatusCode);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }
}

