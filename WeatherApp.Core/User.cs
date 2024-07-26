namespace WeatherApp.Core
{
    public class User
    {
        public User(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
        public int Id { get; }
        public string Login { get; } = string.Empty;
        public string Password { get; } = string.Empty;
    }
}
