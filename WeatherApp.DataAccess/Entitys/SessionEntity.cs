namespace WeatherApp.DataAccess.Entitys
{
    public class SessionEntity
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow;
        public UserEntity User { get; set; } = new UserEntity();
    }
}