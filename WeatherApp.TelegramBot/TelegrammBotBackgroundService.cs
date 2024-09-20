using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static System.Net.WebRequestMethods;
using WeatherApp.Core;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.OpenWeatherAPI.Entity;
using WeatherApp.TelegramBot.Contracts;
using WeatherApp.TelegramBot.Builders;
using System.IO;
using WeatherApp.TelegramBot.Services;

namespace WeatherApp.TelegramBot
{
    public class TelegrammBotBackgroundService : BackgroundService
    {
        private readonly ILogger<TelegrammBotBackgroundService> _logger;
        private readonly TelegramOptions telegramOptions;
        private static HttpClient httpClient = new();
        public TelegrammBotBackgroundService(ILogger<TelegrammBotBackgroundService> logger, IOptions<TelegramOptions> options)
        {
            _logger = logger;
            telegramOptions = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var botClient = new TelegramBotClient(telegramOptions.Token);

            ReceiverOptions options = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            while (!stoppingToken.IsCancellationRequested)
            {

                await botClient.ReceiveAsync(HandleMessageAsync, Error);
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
        async Task HandleMessageAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            if (update.Message.Text == null)
            {
                await client.SendTextMessageAsync(update.Message.Chat.Id, "Не корректное сообщение");
                return;
            }
            var result = ResponseBuilder.GetWeather(update.Message?.Text);
            WeatherContract weatherContract = new();
            try
            {
                weatherContract = JsonSerializer.Deserialize<WeatherContract>(result.Result);
            }
            catch (Exception)
            {
                await Console.Out.WriteLineAsync("Error serizlize");
            }
            ImageSenderService imageSenderService = new();
            var latitude = double.Parse(weatherContract.latitude.ToString());
            var longitude = double.Parse(weatherContract.longitude.ToString());
            await imageSenderService.SendStikerMessage(update.Message.Chat, client, weatherContract.icon);
            await client.SendLocationAsync(update.Message.Chat.Id, latitude, longitude);
            await client.SendTextMessageAsync(update.Message.Chat.Id, result.Result);
        }
        async Task Error(ITelegramBotClient client, Exception ex, CancellationToken cancellationToken)
        {

        }
    }
}
