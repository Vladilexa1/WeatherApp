namespace WeatherApp.Infrastructure.OpenWeatherAPI
{
    public interface IBildUrl
    {
        string GetCurrentWeatherForName(string name);
        string GetForecastForCoordinates(decimal latitude, decimal longitude);
    }
}