namespace WeatherApp.Infrastructure.JWT
{
    public class JWTOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpieresHours { get; set; }
    }
}