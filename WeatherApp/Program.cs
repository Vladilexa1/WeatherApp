using Microsoft.EntityFrameworkCore;
using WeatherApp.DataAccess;

namespace WeatherApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            var configuration = builder.Configuration;
            builder.Services.AddDbContext<WeatherAppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(WeatherAppDbContext)));
            });
            
            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
