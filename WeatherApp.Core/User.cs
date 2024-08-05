using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WeatherApp.Core
{
    public class User
    {
        private const string PATTERN_EMAIL = ".+\\@.+\\..+";
        private User(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }
        public int Id { get; }
        public string Login { get; } = string.Empty;
        public string PasswordHash { get; } = string.Empty;

        public static User Create(string login, string passwordHash)
        {
            Regex regex = new(PATTERN_EMAIL);
           
            if (regex.IsMatch(login))
            {
                var user = new User(login, passwordHash);
                return user;
            }
            else
            {
                throw new ValidationException("Enter valid login");
            }
        }
    }
}
