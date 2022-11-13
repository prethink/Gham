using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Gham.Helpers.Extensions
{
    public static class User
    {
        public static long GetChatId(this Update update)
        {
            if (update.Message != null)
                return update.Message.Chat.Id;

            if (update.CallbackQuery != null)
                return update.CallbackQuery.Message.Chat.Id;


            throw new Exception("Не удалось получить чат ID");
        }

        public static int GetMessageId(this Update update)
        {
            var data = update.GetCacheData();
            if (data?.BotMessage?.MessageId > 0)
            {
                var messageId = data.BotMessage.MessageId;
                return messageId;
            }

            if (update.CallbackQuery != null)
                return update.CallbackQuery.Message.MessageId;


            throw new Exception("Не удалось получить ID чата");
        }
    }
}
