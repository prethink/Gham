using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gham.Models.CallbackCommands
{
    public class CallendarCommand : CallbackBaseCommand
    {
        [JsonProperty("1")]
        public DateTime Date { get; set; }
        public CallendarCommand(DateTime date)
        {
            Date = date;
        }
    }
}
