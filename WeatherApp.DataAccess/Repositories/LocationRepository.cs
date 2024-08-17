using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core;
using WeatherApp.DataAccess.Entitys;

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
            //await _dbContext.Locations.AddAsync(locationEntity);
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId) ?? throw new Exception();
            user.Locations.Add(locationEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Location> GetById(int id)
        {
            var locationEntity = await _dbContext.Locations
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();

            var result = Location.Create(locationEntity.Name, locationEntity.UserId,
                                         locationEntity.Latitude, locationEntity.Longitude);
            return result;
        }
        public async Task Delete(int idLocation, int idUser)
        {
            var user = await _dbContext.Users.Include(l => l.Locations).FirstOrDefaultAsync(x => x.Id == idUser) ?? throw new Exception();
            var location = user.Locations.FirstOrDefault(x => x.Id == idLocation) ?? throw new Exception("Location not faund");
            user.Locations.Remove(location);
            await _dbContext.SaveChangesAsync();
        }
    }
}
