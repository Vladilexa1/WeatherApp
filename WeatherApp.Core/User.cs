namespace WeatherApp.Core
{
    public class User
    {
        private User(int id, string login, string passwordHash)
        {
            Id = id;
            Login = login;
            PasswordHash = passwordHash;
        }
        public int Id { get; }
        public string Login { get; } = string.Empty;
        public string PasswordHash { get; } = string.Empty;

        public static User Create(int id, string login, string passwordHash) // TODO: Валидация??
        {
            var user = new User(id, login, passwordHash);
            return user;
        }
    }
}
