using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.TelegramBot.Builders
{
    public static class ResponseBuilder
    {
        private static HttpClient httpClient = new HttpClient();
        public async static Task<string> GetWeather(string city)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7217/Weather/SearchLocation?city={city}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }
}
