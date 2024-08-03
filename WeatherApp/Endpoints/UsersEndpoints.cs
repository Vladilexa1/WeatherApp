using Microsoft.AspNetCore.Mvc;
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
        private static async Task<IResult> Register(string email, string password, IUserService userServices)
        {
            await userServices.Register(email, password);
            return Results.Ok();
        }
        private static async Task<IResult> Login(string email, string password, IUserService userServices)
        {
            var token = userServices.Login(email, password);

            return Results.Ok(token);
        }
    }
}
