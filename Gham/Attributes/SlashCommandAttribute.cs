using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gham.Attributes
{
    public class SlashCommandAttribute : Attribute
    {
        public List<string> Commands { get; set; }
        public bool Priority { get; private set; }


        public SlashCommandAttribute(bool priority, params string[] commands)
        {
            Commands = commands.ToList();
            Priority = priority;
        }
    }
}
