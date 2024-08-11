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
    public class OpenWeatherAPIclient
    {
        private const string URL_OPENWEATHER = "https://api.openweathermap.org/data/2.5/weather?";
        private const string API_KEY = "7b420c1d27c4ae89ed5c275c79df2a00";

        // https: //api.openweathermap.org/data/2.5/weather?q={sity}appid=7b420c1d27c4ae89ed5c275c79df2a00

        //lat=88.5&lon=88.5&appid=7b420c1d27c4ae89ed5c275c79df2a00
        private static HttpClient sharedClient = new();

        public async Task<CurrentWeather> GetWeatherForName(string name) // TODO: проверить на налл, ...
        {
            var response = await sharedClient.GetAsync(BildUrl.GetCoordForName(name));

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var weather = JsonSerializer.Deserialize<WeatherEntity>(jsonResponse) ?? throw new Exception("Problem bro");
            var result = CurrentWeather.Create
                (weather.coord.lat, weather.coord.lat, weather.weather[0].main, weather.weather[0].description,
                weather.main.temp, weather.main.feelLike, weather.main.pressure, weather.wind.speed, weather.wind.deg,
                weather.country.country, weather.name
                );
            return result;
        }
        public async Task<List<CurrentWeather>> GetForecastForName(string name)
        {
            
            return null;
        }




    }
}
