namespace WeatherApp.Core
{
    public class User
    {
        private User(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }
        public int Id { get; }
        public string Login { get; } = string.Empty;
        public string PasswordHash { get; } = string.Empty;

        public static User Create(string login, string passwordHash) // TODO: Валидация?? // Id????
        {
            var user = new User(login, passwordHash);
            return user;
        }
    }
}
