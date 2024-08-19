using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherApp.Application;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI;
using WeatherApp.Infrastructure.OpenWeatherAPI.Exceptions;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("Weather")]
    public class WeatherController : Controller
    {
        [Authorize]
        [HttpGet("SearchLocation")]
        public async Task<ActionResult> SearchForName(string city, IWeatherService weatherService)
        {
            Weather currentWeather;
            try
            {
                 currentWeather = await weatherService.GetWeatherForName(city);
            }
            catch (WrongCoordinatesException)
            {
                return StatusCode(400, "Wrong Coordinates");
            }
            catch (CityNotFaundException)
            {
                return StatusCode(404, "City not faund");
            }
            catch (ServiceNotAvailableException)
            {
                return StatusCode(503, "Service Unavailable");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(currentWeather);
        }
        [Authorize]
        [HttpGet("Forecast")]
        public async Task<ActionResult> GetForecast(decimal latitude, decimal longitude, IWeatherService weatherService)
        {
            //if (Authorize == false)
            //{
            //    Redirect(Login());
            //}
            List<Forecast> response;
            try
            {
                response = await weatherService.GetForecast(latitude, longitude);
            }
            catch (WrongCoordinatesException)
            {
                return StatusCode(400, "Wrong Coordinates");
            }
            catch (CityNotFaundException)
            {
                return StatusCode(404, "City not faund");
            }
            catch (ServiceNotAvailableException)
            {
                return StatusCode(503, "Service Unavailable");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
            return Ok(response);
        }
    }
}
