using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gham.Helpers
{
    public class CallbackCommand
    {
        public string CommandName { get; private set; }
        public List<string> Argumnets { get; private set; }

        public CallbackCommand(string commandName, List<string> args)
        {
            CommandName = commandName;
            Argumnets = args;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static CallbackCommand GetCommandByCallback(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<CallbackCommand>(data);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }

    public class InlineCommand : IInlineContent
    {
        public string Text { get; private set; }
        public CallbackCommand Command { get; set; }

        public string GetContent()
        {
            return Command.ToString();
        }

        public string GetTextButton()
        {
            return Text; 
        }
    }

    public interface IInlineContent
    {
        public string GetTextButton();
        public string GetContent();

    }
}
