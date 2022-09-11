using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Gham.Commands
{
    public class Router
    {
        #region Commands
        public const string START = "/start";
        #endregion


        public Router()
        {
            RegisterCommand();
        }

        public void RegisterCommand()
        {

        }

        public async Task ExecuteCommandByMessage(string command, Update update)
        {

        }

        public void ExecuteCommandByInline()
        {

        }
    }
}
