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
using Gham.Commands.Common;

namespace Gham.Commands.Example
{
    public class Access
    {
        [MessageMenuHandler(false, Router.START)]
        public static async Task Start(ITelegramBotClient botClient, Update update)
        {
            await Common.Message.Send(botClient, update, "Выполнена команда Start");
        }

        public static async Task StartWithArguments(ITelegramBotClient botClient, Update update, string arg)
        {
            await Common.Message.Send(botClient, update, $"Выполнена команда старт с агрументом '{arg}'");
        }

        /// <summary>
        /// Не найдена команда
        /// </summary>
        public static async Task CommandMissing(ITelegramBotClient botClient, Update update, string command = "")
        {
            string result = !string.IsNullOrEmpty(command) ? command : update.Message.Text;
            await Common.Message.Send(botClient, update, $"В базе не найдена команда '{result}'");
        }

        /// <summary>
        /// Отображение сообщения что не хватает прав для того, чтобы использовать данный бот
        /// </summary>
        public static async Task PrivilagesMissing(ITelegramBotClient botClient, Update update)
        {
            string msg = $"У вас не достаточно прав на использование этой команды!";
            await Common.Message.Send(botClient, update, msg);
        }
    }
}
