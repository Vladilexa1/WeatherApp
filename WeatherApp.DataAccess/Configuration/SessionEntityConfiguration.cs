using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherApp.DataAccess.Entitys;

namespace WeatherApp.DataAccess.Configuration
{
    public class SessionEntityConfiguration : IEntityTypeConfiguration<SessionEntity>
    {
        public void Configure(EntityTypeBuilder<SessionEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Session)
                .HasForeignKey<SessionEntity>(s => s.UserId);
        }
    }
}