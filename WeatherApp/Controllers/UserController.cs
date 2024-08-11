using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Contracts;
using WeatherApp.Application;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : Controller
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRequest userRequest, IUserService userServices, IBildUrl a)
        {
            await userServices.Register(userRequest.login, userRequest.password);
            a.GetCurrentWeatherForName("asd");
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
    }
}
