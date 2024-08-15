using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Contracts;
using WeatherApp.Application;
using WeatherApp.DataAccess.Repositories;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp.API.Endpoints
{
    public static class UsersEndpoints 
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            return app;
        }
        private static async Task<IResult> Register(UserRequest userRequest, IUserService userServices, IBildUrl a)
        {
            await userServices.Register(userRequest.login, userRequest.password);
            a.GetCurrentWeatherForName("asd");
            return Results.Ok();
        }
        private static async Task<IResult> Login(
            UserRequest userRequest, 
            IUserService userServices, 
            HttpContext httpContext
            )
        {
            var token = await userServices.Login(userRequest.login, userRequest.password);

            httpContext.Response.Cookies.Append("test-cooky", token); // перенести имя в константу

            return Results.Ok();
        }
    }
}
