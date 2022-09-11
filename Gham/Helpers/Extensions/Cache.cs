using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Gham.Helpers.Extensions
{
    public static class Cache
    {
        static Dictionary<long, UserCache> _userHandlerData = new();
        public static void CreateCacheData(this ITelegramBotClient bot, long userId)
        {
            ClearCacheData(bot, userId);
            _userHandlerData.Add(userId, new UserCache());
        }

        public static KeyValuePair<long, UserCache> GetCacheData(this ITelegramBotClient bot, long userId)
        {
            var data = _userHandlerData.FirstOrDefault(x => x.Key == userId);
            if (data.Equals(default(KeyValuePair<long, UserCache>)))
            {
                CreateCacheData(bot, userId);
                return _userHandlerData.FirstOrDefault(x => x.Key == userId);
            }
            else
            {
                return data;
            }
        }

        public static void ClearCacheData(this ITelegramBotClient bot, long userId)
        {
            if (HasCacheData(bot, userId))
            {
                _userHandlerData.Remove(userId);
            }

        }

        public static bool HasCacheData(this ITelegramBotClient bot, long userId)
        {
            return _userHandlerData.ContainsKey(userId);
        }
    }

    public class UserCache
    {
        public int? IncidentId { get; set; }
        public int? ExecuterId { get; set; }

        public void Clear()
        {
            IncidentId = null;
            ExecuterId = null;
        }
    }
}
