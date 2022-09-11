using Gham.Commands.Base;
using Gham.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gham.Commands.Inline
{
    public class Menu : CommandBase
    {
        public Menu(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task MenuCallBack(Update update)
        {
            var menu = MenuGenerator.GenerateInlineWithCallBack();
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = menu;

            var messageInstance = new Common.Message(_botClient);
            await messageInstance.Send(update, "Выполнена команда главное меню", option);
        }
    }
}
