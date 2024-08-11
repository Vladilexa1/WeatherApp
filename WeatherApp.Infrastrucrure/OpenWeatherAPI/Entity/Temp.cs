using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.OpenWeatherAPI.Entity
{
    public record Temp(decimal temp, decimal feelLike, decimal pressure)
    {
    }
}
