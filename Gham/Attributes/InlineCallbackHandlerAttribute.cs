using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gham.Attributes
{
    internal class InlineCallbackHandlerAttribute : Attribute
    {
        public List<string> Commands { get; set; }
        public bool Priority { get; private set; }

        public InlineCallbackHandlerAttribute(bool priority = false, params string[] commands)
        {
            Commands = commands.ToList();
            Priority = priority;
        }
    }
}
