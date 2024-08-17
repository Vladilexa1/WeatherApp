using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI.Entity;

namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public interface IOpenWeatherAPIclient
    {
        Task<ForecastEntity> GetForecastForName(string name);
        Task<WeatherApp.Core.Weather> GetWeatherForName(string name);
    }
}