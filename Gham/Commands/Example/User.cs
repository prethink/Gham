using Gham.Attributes;
using Gham.Commands.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gham.Commands.Example
{
    public class User
    {
        /// <summary>
        /// Команда для получения идентификатора пользователя/группы
        /// </summary>
        [MessageMenuHandler(false, Router.MY_ID)]
        public static async Task GetMyId(ITelegramBotClient botClient, Update update)
        {
            await Common.Message.Send(botClient, update, $"Id - {update.Message.Chat.Id}");
        }

        /// <summary>
        /// Команда для получения идентификатора пользователя/группы
        /// </summary>
        [MessageMenuHandler(false, Router.ECHO)]
        public static async Task Echo(ITelegramBotClient botClient, Update update)
        {
            await Common.Message.Send(botClient, update, $"Вы написали {update.Message.Text}");
        }

    }
}
