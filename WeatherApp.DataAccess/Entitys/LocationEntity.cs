using System.Net.Http.Headers;

namespace WeatherApp.DataAccess.Entitys
{
    public class LocationEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}