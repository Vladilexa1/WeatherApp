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
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    public class WeatherController : Controller
    {
        /// <summary>
        /// Search current weather in city by its name
        /// </summary>
        /// <param name="city"></param>
        /// <param name="weatherService"></param>
        /// <response code="400">If write wrong coordinates</response>
        /// <response code="401">If user unauthorized</response>
        /// <response code="404">If city is not faund</response>
        /// <response code="503">If service is not available</response>
        /// <response code="500">If have problem in server</response>
        /// <returns></returns>
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
        /// <summary>
        /// Add location to user
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="weatherService"></param>
        /// <response code="400">If write wrong coordinates</response>
        /// <response code="401">If user unauthorized</response>
        /// <response code="404">If city is not faund</response>
        /// <response code="503">If service is not available</response>
        /// <response code="500">If have problem in server</response>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Forecast")]
        public async Task<ActionResult> GetForecast(decimal latitude, decimal longitude, IWeatherService weatherService)
        {
            List<Forecast> response;
            try
            {
                response = await weatherService.GetForecast(latitude, longitude);
            }
            catch (WrongCoordinatesException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (CityNotFaundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (ServiceNotAvailableException ex)
            {
                return StatusCode(503, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(response);
        }
    }
}
