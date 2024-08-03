namespace WeatherApp.Infrastructure
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        public bool Verify(string password, string passwordHash);
    }
}