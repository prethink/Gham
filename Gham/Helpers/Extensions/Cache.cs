using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Gham.Helpers.Extensions
{
    public static class Cache
    {
        static Dictionary<long, UserCache> _userHandlerData = new();
        public static void CreateCacheData(this Update update)
        {
            long userId = update.GetChatId();
            ClearCacheData(update);
            _userHandlerData.Add(userId, new UserCache());
        }

        public static KeyValuePair<long, UserCache> GetCacheData(this Update update)
        {
            long userId = update.GetChatId();
            var data = _userHandlerData.FirstOrDefault(x => x.Key == userId);
            if (data.Equals(default(KeyValuePair<long, UserCache>)))
            {
                CreateCacheData(update);
                return _userHandlerData.FirstOrDefault(x => x.Key == userId);
            }
            else
            {
                return data;
            }
        }

        public static void ClearCacheData(this Update update)
        {
            long userId = update.GetChatId();
            if (HasCacheData(update))
            {
                _userHandlerData.Remove(userId);
            }

        }

        public static bool HasCacheData(this Update update)
        {
            long userId = update.GetChatId();
            return _userHandlerData.ContainsKey(userId);
        }
    }

    public class UserCache
    {

    }
}
