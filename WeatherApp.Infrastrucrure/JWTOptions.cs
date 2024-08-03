namespace WeatherApp.Infrastructure
{
    public class JWTOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpieresHours { get; set; }
    }
}