using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Location
    {
        private Location(int id, string name, int userId, decimal latitude, decimal lonngitude)
        {
            Id = id;
            Name = name;
            UserId = userId;
            Latitude = latitude;
            Longitude = lonngitude;
        }
        public int Id { get; } = 0;
        public string Name { get; } = string.Empty;
        public int UserId { get; } = 0;
        public decimal Latitude { get; }
        public decimal Longitude { get; }

        public static Location Create(int id, string name, int userId, decimal latitude, decimal lonngitude)// TODO: validation
        {
            Location location = new Location(id, name, userId, latitude, lonngitude);
            return location;
        }

    }
}
