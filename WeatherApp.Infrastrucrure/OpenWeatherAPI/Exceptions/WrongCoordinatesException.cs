﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.OpenWeatherAPI.Exceptions
{
    public class WrongCoordinatesException(string message) : Exception(message)
    {
    }
}
