namespace WeatherApp.DataAccess
{
    public class UserEntity
    {
        public int Id { get; }
        public string Login { get; } = string.Empty;
        public string PasswordHash { get; } = string.Empty;
    }
}