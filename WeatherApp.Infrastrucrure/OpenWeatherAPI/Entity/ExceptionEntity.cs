using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.OpenWeatherAPI.Entity
{
    public class ExceptionEntity
    {
        public string cod { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;

    }
}
