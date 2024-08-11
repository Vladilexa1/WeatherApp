using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.OpenWeatherAPI.Entity
{
    public class Forecast
    {
        public string cod { get; set; }
        public List<WeatherForecast> list { get; set; }
        public City city { get; set; }
    }
}
