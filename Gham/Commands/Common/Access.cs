using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using static Gham.Helpers.Extensions.Step;
using Gham.Helpers.Extensions;
using Gham.Attributes;

namespace Gham.Commands.Common
{
    public class Access
    {
        [MessageMenuHandler(false,Router.START)]
        public static async Task Start(ITelegramBotClient botClient, Update update)
        {
            await Message.Send(botClient, update, "Выполнена команда Start");
        }

        public static async Task StartWithArguments(ITelegramBotClient botClient, Update update, string arg)
        {
            await Message.Send(botClient, update, $"Выполнена команда старт с агрументом '{arg}'");
        }

        /// <summary>
        /// Не найдена команда
        /// </summary>
        public static async Task CommandMissing(ITelegramBotClient botClient, Update update, string command = "")
        {
            string result = !string.IsNullOrEmpty(command) ? command : update.Message.Text;
            await Message.Send(botClient, update, $"В базе не найдена команда '{result}'");
        }

        /// <summary>
        /// Отображение сообщения что не хватает прав для того, чтобы использовать данный бот
        /// </summary>
        public static async Task PrivilagesMissing(ITelegramBotClient botClient, Update update)
        {
            string msg = $"У вас не достаточно прав на использование этой команды!";
            await Message.Send(botClient, update, msg);
        }
    }
}
