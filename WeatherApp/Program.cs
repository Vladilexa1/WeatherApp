using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WeatherApp.API.Endpoints;
using WeatherApp.API.Extensions;
using WeatherApp.Application;
using WeatherApp.DataAccess;
using WeatherApp.DataAccess.Repositories;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.JWT;
using WeatherApp.Infrastructure.OpenWeatherAPI;

namespace WeatherApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var configuration = builder.Configuration;
            
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<WeatherAppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(WeatherAppDbContext)));
            });


            builder.Services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            builder.Services.Configure<BuildUrlOptions>(configuration.GetSection("BuildUrlOptions"));
            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJWTProvider, JWTProvider>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBildUrl, BildUrl>();
            builder.Services.AddScoped<IOpenWeatherAPIclient, OpenWeatherAPIclient>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            
            ApiExtensions.AddApiAuthentication(builder, configuration.GetSection("JWTOptions"));
            
            var app = builder.Build();

            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            app.UseHttpsRedirection();
            app.UseCors();
            app.Run();
        }
    }
}
