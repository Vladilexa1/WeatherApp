using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherApp.Application;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("Weather")]
    public class WeatherController : Controller
    {
        [Authorize]
        [HttpGet("SearchLocation")]
        public async Task<ActionResult> SearchForName(string city, IOpenWeatherAPIclient openWeatherAPIclient)
        {
            Weather currentWeather;
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
        [HttpGet("Forecast")]
        public async Task<ActionResult> GetForecast(decimal latitude, decimal longitude, IWeatherService weatherService)
        {
            var forecast = weatherService.GetForecast(latitude, longitude);
            return Ok(forecast);
        }
    }
}
