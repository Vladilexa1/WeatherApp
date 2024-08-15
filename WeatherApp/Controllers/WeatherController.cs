using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("Weather")]
    public class WeatherController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> WeatherForName(string city, IOpenWeatherAPIclient openWeatherAPIclient)
        {
            CurrentWeather currentWeather;
            try
            {
                 currentWeather = await openWeatherAPIclient.GetWeatherForName(city);
            }
            catch (JsonException)
            {
                return StatusCode(404);
            }
            
            return Ok(currentWeather);
        }
    }
}
