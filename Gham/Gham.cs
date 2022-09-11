using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace Gham
{
    public class Gham
    {
        private ITelegramBotClient _botClient;
        private Handler _handler;
        private CancellationTokenSource _cts;
        private ReceiverOptions _options;
        public string Token { get; private set; }
        public bool IsWork { get; private set; }   
        public Gham(string token)
        {
            Token = token;
        }

        public async Task Start()
        {
            try
            {
                _botClient = new TelegramBotClient(Token);
                _handler = new Handler();
                _cts = new CancellationTokenSource();
                _options = new ReceiverOptions { AllowedUpdates = { } };

                await ClearUpdates();

                _botClient.StartReceiving(
                            _handler.HandleUpdateAsync,
                            _handler.HandleErrorAsync,
                            _options,
                            cancellationToken: _cts.Token);

                IsWork = true;
            }
            catch(Exception ex)
            {
                IsWork = false;
                //TODO Logging exception
            }
        }

        public async Task Stop()
        {
            try
            {
                _cts.Cancel();

                await Task.Delay(3000);
                IsWork = false;
            }
            catch (Exception ex)
            {
                //TODO Logging exception
            }
        }

        public async Task ChangeToken(string token)
        {
            await Stop();
            Token = token; 
            await Start(); 
        }

        /// <summary>
        /// Очистка очереди команд перед запуском
        /// </summary>
        public async Task ClearUpdates()
        {
            var update = await _botClient.GetUpdatesAsync();
            foreach (var item in update)
            {
                var offset = item.Id + 1;
                await _botClient.GetUpdatesAsync(offset);
            }
        }


    }
}