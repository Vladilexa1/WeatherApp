namespace WeatherApp.Core
{
    public class Location
    {
        private Location(string name, int userId, decimal latitude, decimal lonngitude)
        {
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

        public static Location Create(string name, int userId, decimal latitude, decimal lonngitude)// TODO: validation
        {
            Location location = new Location(name, userId, latitude, lonngitude);
            return location;
        }

    }
}
