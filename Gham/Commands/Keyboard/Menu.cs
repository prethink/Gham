using Gham.Commands.Base;
using Gham.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gham.Commands.Keyboard
{
    public class Menu : CommandBase
    {
        public Menu(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task MainMenu(Update update)
        {

            int columnCount = 1;
            var menuList = new List<string>();

            menuList.Add(Router.ECHO);
            menuList.Add(Router.INLINE_MENU);

            var menu = MenuGenerator.GenerateReplyMenu(columnCount, menuList, Router.MAIN_MENU);
            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;

            var messageInstance = new Common.Message(_botClient);
            await messageInstance.Send(update, "Выполнена команда главное меню", option);
        }
    }
}
