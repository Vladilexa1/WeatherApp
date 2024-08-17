
using WeatherApp.Core;

namespace WeatherApp.Application
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(string email, string password);
        Task AddLocation(Location location, int userId);
        Task DeleteLocation(int idLocation, int idUser);
    }
}