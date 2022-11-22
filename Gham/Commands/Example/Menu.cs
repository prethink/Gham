using Gham.Attributes;
using Gham.Commands.Common;
using Gham.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gham.Commands.Example
{
    public class Menu
    {
        [MessageMenuHandler(true, Router.MENU, Router.MAIN_MENU)]
        public static async Task MainMenu(ITelegramBotClient botClient, Update update)
        {
            int columnCount = 1;
            var menuList = new List<string>();

            menuList.Add(Router.ECHO);
            menuList.Add(Router.INLINE_MENU);

            var menu = MenuGenerator.ReplyKeyboard(columnCount, menuList, true, Router.MAIN_MENU);
            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;

            await Common.Message.Send(botClient, update, "Выполнена команда главное меню", option);
        }
    }
}
