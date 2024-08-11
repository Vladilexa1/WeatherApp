namespace WeatherApp.Infrastructure.OpenWeatherAPI.Entity
{
    public class WeatherForecast
    {
        public Temp main { get; set; }
        public Weather[] weather { get; set; }
        public Wind wind { get; set; }
        public string dt_txt { get; set; }
    }
}