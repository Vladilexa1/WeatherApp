using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherApp.DataAccess.Entitys;

namespace WeatherApp.DataAccess.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.
                HasKey(x => x.Id);
            builder
                .HasIndex(x => x.Login)
                .IsUnique();
            builder
                .HasMany(x => x.Locations)
                .WithMany(x => x.Users);
        }
    }
}