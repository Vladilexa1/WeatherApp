using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.OpenWeatherAPI.Entity
{
    public class WeatherEntity
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public Temp main { get; set; }
        public Wind wind { get; set; }
        public Sys country { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}
