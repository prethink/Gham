using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Gham.Commands.Base
{
    public class CommandBase
    {
        internal ITelegramBotClient _botClient;
        internal CancellationToken cancellationToken;
        internal Router _router;
    }
}
