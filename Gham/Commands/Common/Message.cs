using Gham.Commands.Base;
using Gham.Helpers;
using Gham.Helpers.Extensions;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Gham.Commands.Common
{
    public class Message : CommandBase
    {
        public Message(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Send(Update update, string msg, OptionMessage option = null)
        {

                long chatId = update.GetChatId();

                if (string.IsNullOrWhiteSpace(msg))
                {
                    return;
                }

                if (option == null)
                {
                    var sentMessage = await _botClient.SendTextMessageAsync(
                         chatId: chatId,
                         text: msg,
                         parseMode: ParseMode.Html,
                         cancellationToken: cancellationToken);
                }
                else
                {
                    if (option.ClearMenu)
                    {
                        var sentMessage = await _botClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: msg,
                             parseMode: ParseMode.Html,
                             replyMarkup: new ReplyKeyboardRemove(),
                             cancellationToken: cancellationToken);
                    }
                    else if (option.MenuReplyKeyboardMarkup != null)
                    {
                        var sentMessage = await _botClient.SendTextMessageAsync(
                             chatId: chatId,
                        text: msg,
                             parseMode: ParseMode.Html,
                             replyMarkup: option.MenuReplyKeyboardMarkup,
                             cancellationToken: cancellationToken);
                    }
                    else if (option.MenuInlineKeyboardMarkup != null)
                    {
                        var sentMessage = await _botClient.SendTextMessageAsync(
                             chatId: chatId,
                        text: msg,
                             parseMode: ParseMode.Html,
                             replyMarkup: option.MenuInlineKeyboardMarkup,
                             cancellationToken: cancellationToken);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }

        }

        public async Task Edit(Update update, string msg, OptionMessage option = null)
        {
     
                long chatId = update.GetChatId();
                long messageId = update.Message.MessageId;



                //var sentMessage = await _botClient.EditMessageReplyMarkupAsync(
                //        chatId: chatId,
                //        messageId: messageId,
                //        replyMarkup: inlineMenu,
                //        cancellationToken: cancellationToken);
 
        }

    }
}
