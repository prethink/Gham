using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace Gham.Helpers.Extensions
{
    public static class Step
    {
        public delegate Task Command(ITelegramBotClient botClient, Update update);
        static Dictionary<long, Command> _step = new();

        public static void RegisterNextStep(this Update update, Command command)
        {
            long userId = update.GetChatId();
            ClearStepUser(update);
            _step.Add(userId, command);
        }

        public static KeyValuePair<long, Command> GetStepOrNull(this Update update)
        {
            long userId = update.GetChatId();
            return _step.FirstOrDefault(x => x.Key == userId);
        }

        public static void ClearStepUser(this Update update)
        {
            long userId = update.GetChatId();
            if (HasStep(update))
            {
                _step.Remove(userId);
            }

        }

        public static bool HasStep(this Update update)
        {
            long userId = update.GetChatId();
            return _step.ContainsKey(userId);
        }
    }
}
