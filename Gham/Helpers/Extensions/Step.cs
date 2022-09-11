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
        public delegate Task Command(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        static Dictionary<long, Command> _step = new();

        public static void RegisterNextStep(this ITelegramBotClient bot, long userId, Command command)
        {
            ClearStepUser(bot, userId);
            _step.Add(userId, command);
        }

        public static KeyValuePair<long, Command> GetStepOrNull(this ITelegramBotClient bot, long userId)
        {
            return _step.FirstOrDefault(x => x.Key == userId);
        }

        public static void ClearStepUser(this ITelegramBotClient bot, long userId)
        {
            if (HasStep(bot, userId))
            {
                _step.Remove(userId);
            }

        }

        public static bool HasStep(this ITelegramBotClient bot, long userId)
        {
            return _step.ContainsKey(userId);
        }
    }
}
