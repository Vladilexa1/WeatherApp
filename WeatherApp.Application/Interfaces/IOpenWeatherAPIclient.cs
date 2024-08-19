using WeatherApp.Core;

namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public interface IOpenWeatherAPIclient
    {
        Task<List<Forecast>> GetForecastForCoordinates(decimal latitude, decimal longitude);
        Task<WeatherApp.Core.Weather> GetWeatherForName(string name);
    }
}