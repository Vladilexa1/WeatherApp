using WeatherApp.Core;

namespace WeatherApp.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}