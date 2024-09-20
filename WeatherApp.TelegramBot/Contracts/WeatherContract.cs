namespace WeatherApp.TelegramBot.Contracts
{
    record class WeatherContract
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string currentWeather { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal temp { get; set; }
        public decimal feelsLike { get; set; }
        public decimal pressure { get; set; }
        public decimal windSpeed { get; set; }
        public string deg { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string sityName { get; set; } = string.Empty;
        public string icon { get; set; } = string.Empty;
    }
}