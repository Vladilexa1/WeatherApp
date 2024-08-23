using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core;
using WeatherApp.DataAccess.Entitys;
using WeatherApp.DataAccess.Exceptions;

namespace WeatherApp.DataAccess.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly WeatherAppDbContext _dbContext;

        public LocationRepository(WeatherAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Location location, int userId)
        {
            var locationEntity = new LocationEntity
            {
                Id = location.Id,
                Name = location.Name,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                UserId = location.UserId
            };
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId) ?? throw new Exception();
            user.Locations.Add(locationEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Location> GetByIdLocations(int id)
        {
            var locationEntity = await _dbContext.Locations
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new LocationNotFaundException("Location not faund");

            var result = Location.Create(locationEntity.Name, locationEntity.UserId,
                                         locationEntity.Latitude, locationEntity.Longitude, locationEntity.Id);
            return result;
        }
        public async Task Delete(int idLocation, int userId)
        {
            var user = await _dbContext.Users.Include(l => l.Locations).FirstOrDefaultAsync(x => x.Id == userId);
            var location = user.Locations.FirstOrDefault(x => x.Id == idLocation) ?? throw new LocationNotFaundException("Location not faund");
            user.Locations.Remove(location);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Location>> GetByUserId(int userId)
        {
            var user = await _dbContext.Users.Include(l => l.Locations).FirstOrDefaultAsync(x => x.Id == userId) ?? throw new Exception();
            var locationsEntity = user.Locations;
            List<Location> locations = new();
            foreach (var item in locationsEntity)
            {
                locations.Add(Location.Create(item.Name, item.UserId, item.Latitude, item.Longitude, item.Id));
            }
            return locations;
        }
    }
}
