namespace WeatherApp.Core
{
    public class Location
    {
        private Location(string name, int userId, decimal latitude, decimal lonngitude, int id)
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

        public static Location Create(string name, int userId, decimal latitude, decimal lonngitude, int id = 0)// TODO: validation
        {
            Location location = new Location(name, userId, latitude, lonngitude, id);
            return location;
        }

    }
}
