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
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Gham.Commands.Common
{
    public class Message 
    {
        public static async Task Send(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, string msg, OptionMessage option = null)
        {
            await Send(botClient, update.GetChatId(), msg, option);
        }

        public static async Task Send(ITelegramBotClient botClient, long chatId, string msg, OptionMessage option = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(msg))
                {
                    return;
                }

                if (option == null)
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: msg,
                            parseMode: ParseMode.Html);
                }
                else
                {
                    if (option.ClearMenu)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: new ReplyKeyboardRemove());
                    }
                    else if (option.MenuReplyKeyboardMarkup != null)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                        text: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuReplyKeyboardMarkup);
                    }
                    else if (option.MenuInlineKeyboardMarkup != null)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuInlineKeyboardMarkup);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        public static async Task SendPhoto(ITelegramBotClient botClient, long chatId, string msg, string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                await Send(botClient, chatId, msg);
                return;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var photo = await botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: new InputOnlineFile(fileStream),
                            caption: msg,
                            parseMode: ParseMode.Html
                            );
            }
        }
        public static async Task Edit(ITelegramBotClient botClient, Update update, string msg, OptionMessage option = null)
        {
     
                long chatId = update.GetChatId();
                int messageId = update.Message.MessageId;



            var sentMessage = await botClient.EditMessageReplyMarkupAsync(
                    chatId: chatId,
                    messageId: messageId
                );
        }
    }
}
