using System.Drawing;

namespace WeatherApp.Infrastructure.OpenWeatherAPI.Entity
{
    public class City
    {
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
    }
}