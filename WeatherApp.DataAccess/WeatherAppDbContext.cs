using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Numerics;

namespace WeatherApp.DataAccess
{
    public class WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }

        
    }
}
