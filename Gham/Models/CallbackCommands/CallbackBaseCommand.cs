using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gham.Models.CallbackCommands
{
    public class CallbackBaseCommand
    {
        [JsonProperty("0")]
        public long MessageIdBase { get; set; }
        public CallbackBaseCommand(long messageId)
        {
            MessageIdBase = messageId;
        }
    }
}
