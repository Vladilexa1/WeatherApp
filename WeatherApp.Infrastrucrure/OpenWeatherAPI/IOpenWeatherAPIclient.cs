using WeatherApp.Core;

namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public interface IOpenWeatherAPIclient
    {
        Task<Forecast> GetForecastForName(string name);
        Task<CurrentWeather> GetWeatherForName(string name);
    }
}