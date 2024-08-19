using WeatherApp.Core;

namespace WeatherApp.DataAccess.Repositories
{
    public interface ILocationRepository
    {
        Task Add(Location location, int userId);
        Task<Location> GetById(int id);
        Task Delete(int idLocation, int idUser);
    }
}