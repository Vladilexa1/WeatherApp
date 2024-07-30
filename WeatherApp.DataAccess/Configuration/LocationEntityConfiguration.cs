using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherApp.DataAccess.Entitys;

namespace WeatherApp.DataAccess.Configuration
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<LocationEntity>
    {
        public void Configure(EntityTypeBuilder<LocationEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .HasMany(x => x.Users)
                .WithMany(x => x.Locations);
        }
    }
}