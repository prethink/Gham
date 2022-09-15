using Gham.Commands.Base;
using Gham.Helpers;
using Gham.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
            //InlineKeyboardMarkup inlineKeyboard = new(new[]
            //    {
            //        InlineKeyboardButton.WithSwitchInlineQuery("switch_inline_query"),
            //        InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("switch_inline_query_current_chat"),
            //    }
            //);

            //Message sentMessage = await _botClient.SendTextMessageAsync(
            //    chatId: update.GetChatId(),
            //    text: "A message with an inline keyboard markup",
            //    replyMarkup: inlineKeyboard,
            //    cancellationToken: cancellationToken);
            ////var menu = MenuGenerator.Inl();
            ////var option = new OptionMessage();
            ////option.MenuInlineKeyboardMarkup = menu;

            ////var messageInstance = new Common.Message(_botClient);
            ////await messageInstance.Send(update, "Выполнена команда главное меню", option);
        }
    }
}
