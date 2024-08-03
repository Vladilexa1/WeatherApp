
namespace WeatherApp.Application
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(string email, string password);
    }
}