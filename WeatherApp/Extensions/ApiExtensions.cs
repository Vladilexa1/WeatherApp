using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherApp.Infrastructure.JWT;

namespace WeatherApp.API.Extensions
{
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    public static class ApiExtensions
    {
        public const string AUTH_COOKY_NAME = "AuthCooky";
        public static void AddApiAuthentication(WebApplicationBuilder builder, IConfigurationSection configurationSection)
        {
            JWTOptions jwtOptions = configurationSection.Get<JWTOptions>()?? throw new Exception("Problem configuration");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[AUTH_COOKY_NAME];
                            return Task.CompletedTask;
                        }
                    };
                });
            builder.Services.AddAuthorization(options =>
            {

            });
        }
    }
}
