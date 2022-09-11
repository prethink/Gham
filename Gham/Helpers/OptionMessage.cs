using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Gham.Helpers
{
    public class OptionMessage
    {
        public ReplyKeyboardMarkup MenuReplyKeyboardMarkup { get; set; }

        public InlineKeyboardMarkup MenuInlineKeyboardMarkup { get; set; }
        public bool ClearMenu = false;
    }
}
