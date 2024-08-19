using WeatherApp.Core;

namespace WeatherApp.DataAccess.Repositories
{
    public interface ILocationRepository
    {
        Task Add(Location location, int userId);
        Task<Location> GetByIdLocations(int id);
        Task Delete(int idLocation, int idUser);
        Task<List<Location>> GetByUserId(int userId);
    }
}