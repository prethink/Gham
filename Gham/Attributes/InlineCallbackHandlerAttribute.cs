﻿using Gham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gham.Attributes
{
    internal class InlineCallbackHandlerAttribute : Attribute
    {
        public List<InlineCallbackCommands> Commands { get; set; }

        public InlineCallbackHandlerAttribute(params InlineCallbackCommands[] commands)
        {
            Commands = commands.ToList();
        }
    }
}
