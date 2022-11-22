using Gham.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace Gham.Commands.Calendar
{
    internal class CalendarHandler
    {
        [InlineCallbackHandler(Models.InlineCallbackCommands.CalendarGetMonth)]
        public static async Task GetMonth(ITelegramBotClient botClient, Update update)
        {
            try
            {

            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        [InlineCallbackHandler(Models.InlineCallbackCommands.CalendarGetYear)]
        public static async Task GetYear(ITelegramBotClient botClient, Update update)
        {
            try
            {

            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        [InlineCallbackHandler(Models.InlineCallbackCommands.CalendarGetDate)]
        public static async Task GetDate(ITelegramBotClient botClient, Update update)
        {
            try
            {

            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }


    }
}
