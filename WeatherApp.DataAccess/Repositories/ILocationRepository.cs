using WeatherApp.Core;

namespace WeatherApp.DataAccess.Repositories
{
    public interface ILocationRepository
    {
        Task Add(Location location);
        Task<Location> GetById(int id);
    }
}