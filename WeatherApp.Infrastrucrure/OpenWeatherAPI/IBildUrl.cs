namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public interface IBildUrl
    {
        string GetCurrentWeatherForName(string name);
        string GetForecastForName(string name);
    }
}