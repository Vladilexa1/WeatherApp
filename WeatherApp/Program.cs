using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WeatherApp.API.Endpoints;
using WeatherApp.API.Extensions;
using WeatherApp.Application;
using WeatherApp.DataAccess;
using WeatherApp.DataAccess.Repositories;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.JWT;
using WeatherApp.Infrastructure.OpenWeatherAPI;
using WeatherApp.TelegramBot;
namespace WeatherApp
{
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var configuration = builder.Configuration;
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WeatherAPI",
                    Description = "Клиент с регистрацией для просмотра прогноза погоды в добавленных городах",
                    Contact = new OpenApiContact
                    {
                        Name = "Vladyslav",
                        Email = "vlad.slip4enko2012@gmail.com",
                        Url = new Uri("https://github.com/Vladilexa1")
                    }
                });
                var basePath = AppContext.BaseDirectory;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(basePath, xmlFilename);
                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<WeatherAppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(WeatherAppDbContext)));
            });

            builder.Services.ConfigureApplicationCookie(options => // Перенаправление не авторизованых пользователей
            {
                options.LoginPath = "/login";
            });
            builder.Services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            builder.Services.Configure<BuildUrlOptions>(configuration.GetSection("BuildUrlOptions"));
            builder.Services.AddHostedService<TelegrammBotBackgroundService>(); // tetegram bot
            builder.Services.Configure<TelegramOptions>(builder.Configuration.GetSection(TelegramOptions.Telegram));
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
