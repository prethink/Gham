using Gham.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gham.Commands.Common
{
    public class User : CommandBase
    {
        public User(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        /// <summary>
        /// Команда для получения идентификатора пользователя/группы
        /// </summary>
        public async Task GetMyId(Update update)
        {
            var messageInstance = new Message(_botClient);
            await messageInstance.Send(update, $"Id - {update.Message.Chat.Id}");
        }

        /// <summary>
        /// Команда для получения идентификатора пользователя/группы
        /// </summary>
        public async Task Echo(Update update)
        {
            var messageInstance = new Message(_botClient);
            await messageInstance.Send(update, $"Вы написали {update.Message.Text}");
        }

    }
}
