using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Gham.Helpers
{
    public class InlineCallbackCommand : IInlineContent
    {
        [JsonIgnore]
        public string ButtonName { get; set; }
        public string CommandName { get; set; }
        public List<string> Argumnets { get; set; }

        public InlineCallbackCommand(string commandName, List<string> args)
        {
            CommandName = commandName;
            Argumnets = args;
        }

        public static InlineCallbackCommand GetCommandByCallbackOrNull(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<InlineCallbackCommand>(data);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public string GetTextButton()
        {
            return ButtonName;
        }

        public object GetContent()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class InlineURL : IInlineContent
    {
        public string ButtonName { get; set; }
        public string URL { get; set; }

        public InlineURL(string buttonName, string url)
        {
            ButtonName = buttonName;
            URL = url;
        }

        public object GetContent()
        {
            return URL;
        }

        public string GetTextButton()
        {
            return ButtonName;
        }
    }

    public class InlineWebApp : IInlineContent
    {
        public string ButtonName { get; set; }
        public string WebAppUrl { get; set; }

        public InlineWebApp(string buttonName, string webAppUrl)
        {
            ButtonName = buttonName;
            WebAppUrl = webAppUrl;
        }

        public object GetContent()
        {
            var webApp = new WebAppInfo().Url = WebAppUrl;
            return webApp;
        }

        public string GetTextButton()
        {
            return ButtonName;
        }
    }


    public interface IInlineContent
    {
        public string GetTextButton();
        public object GetContent();

    }
}
