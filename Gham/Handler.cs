﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Diagnostics;
using Telegram.Bot.Exceptions;
using Gham.Helpers.Extensions;
using System.Reflection;
using Gham.Commands;

namespace Gham
{
    public class Handler
    {
        Router _router;
        public Handler()
        {
            _router = new Router();
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Unknown:
                        break;
                    case UpdateType.Message:
                        await HandleMessageRoute(botClient, update, cancellationToken);
                        break;
                    case UpdateType.InlineQuery:
                        break;
                    case UpdateType.ChosenInlineResult:
                        break;
                    case UpdateType.CallbackQuery:
                        break;
                    case UpdateType.EditedMessage:
                        break;
                    case UpdateType.ChannelPost:
                        break;
                    case UpdateType.EditedChannelPost:
                        break;
                    case UpdateType.ShippingQuery:
                        break;
                    case UpdateType.PreCheckoutQuery:
                        break;
                    case UpdateType.Poll:
                        break;
                    case UpdateType.PollAnswer:
                        break;
                    case UpdateType.MyChatMember:
                        break;
                    case UpdateType.ChatMember:
                        break;
                    case UpdateType.ChatJoinRequest:
                        break;
                }
            }
            catch (Exception ex)
            {
                //TODO Logging exception
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };
                //TODO Logging exception
                //return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                //TODO Logging exception
            }

        }

        async Task HandleMessageRoute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch (update.Message.Type)
            {
                case MessageType.Unknown:
                    break;
                case MessageType.Text:
                    await HandleMessageText(botClient, update, cancellationToken);
                    break;
                case MessageType.Photo:
                    break;
                case MessageType.Audio:
                    break;
                case MessageType.Video:
                    break;
                case MessageType.Voice:
                    break;
                case MessageType.Document:
                    break;
                case MessageType.Sticker:
                    break;
                case MessageType.Location:
                    break;
                case MessageType.Contact:
                    await HandleMessageContact(botClient, update, cancellationToken);
                    break;
                case MessageType.Venue:
                    break;
                case MessageType.Game:
                    break;
                case MessageType.VideoNote:
                    break;
                case MessageType.Invoice:
                    break;
                case MessageType.SuccessfulPayment:
                    break;
                case MessageType.WebsiteConnected:
                    break;
                case MessageType.ChatMembersAdded:
                    break;
                case MessageType.ChatMemberLeft:
                    break;
                case MessageType.ChatTitleChanged:
                    break;
                case MessageType.ChatPhotoChanged:
                    break;
                case MessageType.MessagePinned:
                    break;
                case MessageType.ChatPhotoDeleted:
                    break;
                case MessageType.GroupCreated:
                    break;
                case MessageType.SupergroupCreated:
                    break;
                case MessageType.ChannelCreated:
                    break;
                case MessageType.MigratedToSupergroup:
                    break;
                case MessageType.MigratedFromGroup:
                    break;
                case MessageType.Poll:
                    break;
                case MessageType.Dice:
                    break;
                case MessageType.MessageAutoDeleteTimerChanged:
                    break;
                case MessageType.ProximityAlertTriggered:
                    break;
                case MessageType.WebAppData:
                    break;
                case MessageType.VideoChatScheduled:
                    break;
                case MessageType.VideoChatStarted:
                    break;
                case MessageType.VideoChatEnded:
                    break;
                case MessageType.VideoChatParticipantsInvited:
                    break;
            }
        }

        async Task HandleMessageText(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                var chatId = update.Message.Chat.Id;
                var messageText = update.Message.Text;

                await _router.ExecuteCommandByMessage(messageText , update);
            }
            catch (Exception ex)
            {
                //TODO Logging exception
            }

        }

        async Task HandleMessageContact(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                long chatId = update.GetChatId();
            }
            catch (Exception ex)
            {
                //TODO Logging exception
            }
        }
    }


}
