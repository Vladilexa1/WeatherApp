namespace WeatherApp.DataAccess.Entitys
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set;   } = string.Empty;
        public List<LocationEntity> Locations { get; set; } = new List<LocationEntity>();
    }
}