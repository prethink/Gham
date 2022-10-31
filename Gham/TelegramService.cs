using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace Gham
{
    public class TelegramService
    {
        private ITelegramBotClient _botClient;
        private Handler _handler;
        private CancellationTokenSource _cts;
        private ReceiverOptions _options;
        public delegate void ErrorEvent(Exception ex);
        public delegate void CommonEvent(string msg);
        public event ErrorEvent OnLogError;
        public event CommonEvent OnLogCommon;

        public static TelegramService Instance { get; private set; }

        public string Token { get; private set; }
        public bool IsWork { get; private set; }
        private TelegramService(string token)
        {
            Token = token;
        }

        public static TelegramService GetInstance(string token = "")
        {
            if (Instance == null)
            {
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception($"Пустой {nameof(token)} при создание объекта {typeof(TelegramService)}");
                }
                Instance = new TelegramService(token);
            }

            return Instance;
        }


        public async Task Start()
        {
            try
            {
                _botClient = new TelegramBotClient(Token);
                _handler = new Handler(_botClient);
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
            catch (Exception ex)
            {
                IsWork = false;
                TelegramService.GetInstance().InvokeErrorLog(ex);
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
                TelegramService.GetInstance().InvokeErrorLog(ex);
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

        public void InvokeCommonLog(string msg)
        {
            OnLogCommon?.Invoke(msg);
        }

        public void InvokeErrorLog(Exception ex)
        {
            OnLogError?.Invoke(ex);
        }


    }
}