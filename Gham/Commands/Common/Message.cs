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
using static Gham.TelegramService;

namespace Gham.Commands.Common
{
    public class Message
    {
        public static async Task<MessageId> CopyMessage(ITelegramBotClient botClient, Telegram.Bot.Types.Message message, long chatId)
        {
            try
            {
                ChatId toMsg = new ChatId(chatId);
                ChatId fromMsg = new ChatId(message.Chat.Id);
                var rMessage = await botClient.CopyMessageAsync(toMsg, fromMsg, message.MessageId);
                return rMessage;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
        }
        public static async Task CopyMessage(ITelegramBotClient botClient, List<Telegram.Bot.Types.Message> messages, long chatId)
        {
            try
            {
                ChatId toMsg = new ChatId(chatId);
                foreach (var message in messages)
                {

                    try
                    {
                        ChatId fromMsg = new ChatId(message.Chat.Id);
                        await botClient.CopyMessageAsync(toMsg, fromMsg, message.MessageId);
                    }
                    catch (Exception ex)
                    {
                        GetInstance().InvokeErrorLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
            }
        }

        public static async Task<Telegram.Bot.Types.Message> Send(ITelegramBotClient botClient, Update update, string msg, OptionMessage option = null)
        {
            var message = await Send(botClient, update.GetChatId(), msg, option);
            return message;
        }

        public static async Task<Telegram.Bot.Types.Message> Send(ITelegramBotClient botClient, long chatId, string msg, OptionMessage option = null)
        {
            try
            {
                Telegram.Bot.Types.Message message;
                if (string.IsNullOrWhiteSpace(msg))
                {
                    return null;
                }

                if (option == null)
                {
                    message = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: msg,
                            parseMode: ParseMode.Html);
                }
                else
                {
                    if (option.ClearMenu)
                    {
                        message = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: new ReplyKeyboardRemove());
                    }
                    else if (option.MenuReplyKeyboardMarkup != null)
                    {
                        message = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: msg,
                                parseMode: ParseMode.Html,
                                replyMarkup: option.MenuReplyKeyboardMarkup);
                    }
                    else if (option.MenuInlineKeyboardMarkup != null)
                    {
                        message = await botClient.SendTextMessageAsync(
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


                GetInstance().InvokeCommonLog($"Бот отправил ответ пользователю с id {chatId}\n{msg}", TelegramEvents.Server, ConsoleColor.White);
                return message;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("bot was blocked by the user"))
                {
                    GetInstance().InvokeErrorLog(ex);
                }
                else
                {
                    GetInstance().InvokeErrorLog(ex);
                }



                return null;
            }
        }

        public static async Task SendPhoto(ITelegramBotClient botClient, long chatId, string msg, string filePath, OptionMessage option = null)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    await Send(botClient, chatId, msg);
                    return;
                }

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    if (option == null)
                    {
                        var photo = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: new InputOnlineFile(fileStream),
                                    caption: msg,
                                    parseMode: ParseMode.Html
                                    );
                    }
                    else
                    {
                        if (option.MenuReplyKeyboardMarkup != null)
                        {
                            var photo = await botClient.SendPhotoAsync(
                                        chatId: chatId,
                                        photo: new InputOnlineFile(fileStream),
                                        caption: msg,
                                        parseMode: ParseMode.Html,
                                        replyMarkup: option.MenuReplyKeyboardMarkup
                                        );
                        }
                        else if (option.MenuInlineKeyboardMarkup != null)
                        {
                            var photo = await botClient.SendPhotoAsync(
                                        chatId: chatId,
                                        photo: new InputOnlineFile(fileStream),
                                        caption: msg,
                                        parseMode: ParseMode.Html,
                                        replyMarkup: option.MenuInlineKeyboardMarkup
                                        );
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
            }

        }
        public static async Task<Telegram.Bot.Types.Message> SendPhotoWithUrl(ITelegramBotClient botClient, long chatId, string msg, string url, OptionMessage option = null)
        {
            try
            {
                Telegram.Bot.Types.Message message = null;
                if (option == null)
                {
                    message = await botClient.SendPhotoAsync(
                                chatId: chatId,
                                photo: new InputOnlineFile(url),
                                caption: msg,
                                parseMode: ParseMode.Html
                                );
                }
                else
                {
                    if (option.MenuReplyKeyboardMarkup != null)
                    {
                        message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: new InputOnlineFile(url),
                                    caption: msg,
                                    parseMode: ParseMode.Html,
                                    replyMarkup: option.MenuReplyKeyboardMarkup
                                    );
                    }
                    else if (option.MenuInlineKeyboardMarkup != null)
                    {
                        message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: new InputOnlineFile(url),
                                    caption: msg,
                                    parseMode: ParseMode.Html,
                                    replyMarkup: option.MenuInlineKeyboardMarkup
                                    );
                    }
                }
                return message;

            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }

        }

        public static async Task SendFile(ITelegramBotClient botClient, long chatId, string msg, string filePath)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    await Send(botClient, chatId, msg);
                    return;
                }

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var file = await botClient.SendAudioAsync(chatId: chatId,
                                                              audio: new InputOnlineFile(fileStream),
                                                              caption: msg,
                                                              title: msg
                                                              );
                }
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
            }
        }

        public static async Task<Telegram.Bot.Types.Message> Edit(ITelegramBotClient botClient, long chatId, int messageId, string msg, OptionMessage option = null)
        {
            try
            {
                Telegram.Bot.Types.Message message;
                if (string.IsNullOrWhiteSpace(msg))
                {
                    return null;
                }

                if (option == null || option.MenuInlineKeyboardMarkup == null)
                {
                    message = await botClient.EditMessageTextAsync(
                            chatId: chatId,
                            messageId: messageId,
                            text: msg,
                            parseMode: ParseMode.Html);
                }
                else
                {
                    message = await botClient.EditMessageTextAsync(
                            chatId: chatId,
                            messageId: messageId,
                            text: msg,
                            parseMode: ParseMode.Html,
                            replyMarkup: option.MenuInlineKeyboardMarkup);

                }

                return message;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }

        }

        public static async Task<Telegram.Bot.Types.Message> Edit(ITelegramBotClient botClient, Update update, string msg, OptionMessage option = null)
        {
            try
            {
                long chatId = update.GetChatId();
                int messageId = update.GetMessageId();

                var editmessage = await Edit(botClient, chatId, messageId, msg, option);
                return editmessage;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
        }

        public static async Task<Telegram.Bot.Types.Message> EditCaption(ITelegramBotClient botClient, long chatId, int messageId, string msg, OptionMessage option = null)
        {
            try
            {
                Telegram.Bot.Types.Message message;
                if (string.IsNullOrWhiteSpace(msg))
                {
                    return null;
                }

                if (option == null || option.MenuInlineKeyboardMarkup == null)
                {
                    message = await botClient.EditMessageCaptionAsync(
                            chatId: chatId,
                            messageId: messageId,
                            caption: msg,
                            parseMode: ParseMode.Html);
                }
                else
                {
                    message = await botClient.EditMessageCaptionAsync(
                            chatId: chatId,
                            messageId: messageId,
                            caption: msg,
                            parseMode: ParseMode.Html,
                            replyMarkup: option.MenuInlineKeyboardMarkup);

                }

                return message;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }

        }

        public static async Task<Telegram.Bot.Types.Message> EditCaption(ITelegramBotClient botClient, Update update, string msg, OptionMessage option = null)
        {
            try
            {
                long chatId = update.GetChatId();
                int messageId = update.GetMessageId();

                var editmessage = await EditCaption(botClient, chatId, messageId, msg, option);
                return editmessage;
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
                return null;
            }
        }

        public static async Task NotifyFromCallBack(ITelegramBotClient botClient, string callbackQueryId, string msg, bool showAlert = true)
        {
            try
            {
                await botClient.AnswerCallbackQueryAsync(callbackQueryId, msg, showAlert);
            }
            catch (Exception ex)
            {
                GetInstance().InvokeErrorLog(ex);
            }
        }
    }
}
