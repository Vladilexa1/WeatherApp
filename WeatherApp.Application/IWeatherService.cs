using WeatherApp.Core;

namespace WeatherApp.Application
{
    public interface IWeatherService
    {
        Task<List<Forecast>> GetForecast(decimal latitude, decimal longitude);
        Task<Weather> GetWeatherForName(string city);
    }
}