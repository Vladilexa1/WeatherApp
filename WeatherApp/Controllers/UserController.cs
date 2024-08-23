using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Contracts;
using WeatherApp.API.Extensions;
using WeatherApp.Application;
using WeatherApp.Core;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("/")]
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    public class UserController : Controller
    {
        public const string AUTH_COOKY_NAME = ApiExtensions.AUTH_COOKY_NAME;
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="userRequest"></param>
        /// <param name="userServices"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterContract userRequest, IUserService userServices)
        {
            if (userRequest.repeatPassword == userRequest.password)
            {
                await userServices.Register(userRequest.login, userRequest.password);
                return Ok();
            }
            else
            {
                return StatusCode(404);
            }
        }        
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginContract userResponse, IUserService userServices)
        {
            var token = await userServices.Login(userResponse.login, userResponse.password);
            
            HttpContext.Response.Cookies.Append(AUTH_COOKY_NAME, token);

            return Ok();
        }
        [Authorize]
        [HttpPost("logout")]
        public ActionResult Logout()
        {
            var cookie = HttpContext.Response.Cookies;
            cookie.Delete(AUTH_COOKY_NAME);
            return Ok();
        }
        [Authorize]
        [HttpPost("AddLocation")]
        public async Task<ActionResult> AddLokation(LocationContract locationContract, IUserService userServices)
        {
            var userId = GetIdUser();

            var location = Location.Create(locationContract.city, userId, locationContract.Latitue, locationContract.Longitude);
            await userServices.AddLocation(location, userId);
            return StatusCode(201);
        }
        [Authorize]
        [HttpGet("Location")]
        public async Task<ActionResult> GetAllLocation(IUserService userServices)
        {
            var userId = GetIdUser();
            var response = await userServices.GetLocation(userId);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("DeleteLocation")]
        public async Task<ActionResult> DeleteLocation(int idLocation, IUserService userServices)
        {
            var userId = GetIdUser();
            await userServices.DeleteLocation(idLocation, userId);
            return Ok();
        }
        private int GetIdUser()
        {
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int.TryParse(userIdString, out int userId);
            return userId;
        }
    }
}
