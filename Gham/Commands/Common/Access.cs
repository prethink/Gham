using Gham.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using static Gham.Helpers.Extensions.Step;
using Gham.Helpers.Extensions;

namespace Gham.Commands.Common
{
    public class Access : CommandBase
    {
        public Access(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Start(Update update)
        {
                var messageInstance = new Message(_botClient);
                await messageInstance.Send(update, "Выполнена команда Start");
        }

        public async Task StartWithArguments(Update update, string arg)
        {
            var messageInstance = new Message(_botClient);
            await messageInstance.Send(update, $"Выполнена команда старт с агрументом '{arg}'");
        }

        /// <summary>
        /// Не найдена команда
        /// </summary>
        public async Task CommandMissing(Update update)
        {
            var messageInstance = new Message(_botClient);
            await messageInstance.Send(update, $"В базе не найдена команда '{update.Message.Text}'");
        }

        /// <summary>
        /// Отображение сообщения что не хватает прав для того, чтобы использовать данный бот
        /// </summary>
        public async Task PrivilagesMissing(Update update)
        {
            var messageInstance = new Message(_botClient);
            string msg = $"У вас не достаточно прав на использование этой команды!";
            await messageInstance.Send(update, msg);
        }
    }
}
