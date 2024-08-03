using Microsoft.EntityFrameworkCore;
using WeatherApp.DataAccess.Entitys;
using WeatherApp.DataAccess.Configuration;
namespace WeatherApp.DataAccess
{
    public class WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SessionEntityConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
