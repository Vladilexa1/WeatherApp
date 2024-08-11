using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherApp.API.Endpoints;
using WeatherApp.Application;
using WeatherApp.DataAccess;
using WeatherApp.DataAccess.Repositories;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.JWT;

namespace WeatherApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var configuration = builder.Configuration;
            
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<WeatherAppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(WeatherAppDbContext)));
            });

            builder.Services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJWTProvider, JWTProvider>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IUserService, UserService>();
            
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
            app.MapUsersEndpoints();
            app.MapControllers();
            app.UseHttpsRedirection();
            app.UseCors();
            app.Run();
        }
    }
}
