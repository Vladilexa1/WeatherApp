using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Contracts;
using WeatherApp.Application;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : Controller
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRequest userRequest, IUserService userServices)
        {
            await userServices.Register(userRequest.login, userRequest.password);
            return Ok();
        }        
        [HttpPost("login")]
        public async Task<ActionResult> Login(
            UserRequest userRequest,
            IUserService userServices
            )
        {
            var token = await userServices.Login(userRequest.login, userRequest.password);
            
            HttpContext.Response.Cookies.Append("test-cooky", token); // перенести имя в константу

            return Ok();
        }
        [Authorize]
        [HttpPost("addLocation")]
        public async Task<ActionResult> AddLokation(LocationContract locationContract, IUserService userServices)
        {
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int.TryParse(userIdString, out int userId);

            var location = Location.Create(locationContract.city, userId, locationContract.Latitue, locationContract.Longitude);
            await userServices.AddLocation(location);
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteLocation(int id, IUserService userServices)
        {
            //TODO
            return Ok();
        }
    }
}
