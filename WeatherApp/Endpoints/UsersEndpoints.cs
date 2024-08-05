using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Contracts;
using WeatherApp.Application;
using WeatherApp.DataAccess.Repositories;

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
        private static async Task<IResult> Register(UserRequest userRequest, IUserService userServices)
        {
            await userServices.Register(userRequest.login, userRequest.password);
            return Results.Ok();
        }
        private static async Task<IResult> Login(
            string email, string password, 
            IUserService userServices, 
            HttpContext httpContext)
        {
            var token = await userServices.Login(email, password);

            httpContext.Response.Cookies.Append("test-cooky", token);// перенести имя в константу

            return Results.Ok(token);
        }
    }
}
