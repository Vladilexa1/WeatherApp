using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("Weather")]
    public class WeatherController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> WeatherForName(string name, IOpenWeatherAPIclient openWeatherAPIclient)
        {
            var result = await openWeatherAPIclient.GetWeatherForName(name);
            return Ok(result);
        }
    }
}
