using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WeatherApp.TelegramBot.Services
{
    public class ImageSenderService
    {
        private const string PATH_TO_IMG = "../WeatherApp.TelegramBot/img/";
        private async Task<Stream> GetImageWeather(string icon)
        {
            // this codes taken from https://openweathermap.org/weather-conditions#Weather-Condition-Codes-2 
            switch (icon)
            {
                case var s when s == "01d" ||  s == "01n": //  clear sky
                    return System.IO.File.OpenRead(PATH_TO_IMG + "sun.png");

                case var s when s == "02d" || s =="02n" : //  few clouds
                    return System.IO.File.OpenRead(PATH_TO_IMG + "few_clouds.png");

                case var s when s == "03d" || s == "03n": //  scattered clouds
                    return System.IO.File.OpenRead(PATH_TO_IMG + "scattered_clouds.png");

                case var s when s == "04d" || s == "04n": //  broken cloud
                    return System.IO.File.OpenRead(PATH_TO_IMG + "scattered_clouds.png");

                case var s when s == "09d" || s == "09n"://  shower rain 
                    return System.IO.File.OpenRead(PATH_TO_IMG + "rain.png");

                case var s when s == "10d" || s == "10n": //  rain
                    return System.IO.File.OpenRead(PATH_TO_IMG + "rain.png");

                case var s when s == "11d" || s == "11n": // 	thunderstorm
                    return System.IO.File.OpenRead(PATH_TO_IMG + "thunderstorm.png");

                case var s when s == "13d" || s == "13n": //  snow
                    return System.IO.File.OpenRead(PATH_TO_IMG + "snow.png");

                case var s when s == "50d" || s == "50n": //  mist
                    return System.IO.File.OpenRead(PATH_TO_IMG + "mist.png");

                default:
                    await Console.Out.WriteLineAsync($"Default switch, check ImageSenderService(code icon = \"{icon}\")");
                    return null;
            }
        }
        public async Task SendStikerMessage(Chat chat, ITelegramBotClient client, string icon)
        {
            var image = GetImageWeather(icon).Result;
            if (image == null) return;
            await client.SendStickerAsync(chat, InputFile.FromStream(image));
        }
    }
}
