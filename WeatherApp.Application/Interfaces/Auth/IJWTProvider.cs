using WeatherApp.Core;

namespace WeatherApp.Infrastructure
{
    public interface IJWTProvider
    {
        string GenerateToken(User user);
    }
}