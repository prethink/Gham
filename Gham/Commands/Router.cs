using Gham.Attributes;
using Gham.Commands.Common;
using Gham.Commands.Keyboard;
using Gham.Helpers;
using Gham.Helpers.Extensions;
using Gham.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using static Gham.Helpers.Extensions.Step;

namespace Gham.Commands
{
    public class Router
    {
        #region RelpyCommands
        public const string START               = "/start";
        public const string MENU                = "/menu";
        public const string MY_ID               = "/myid";
        public const string MAIN_MENU           = "Главное меню";
        public const string INLINE_MENU         = "Inline меню";
        public const string ECHO                = "ЭХО";

        #endregion

        #region InlineCommand
        #endregion

        delegate Task MessageCommand(ITelegramBotClient botclient, Update update);
        delegate Task CommandInline(Update update,InlineCallbackCommand command);
        private Dictionary<string, MessageCommand> _priorityCommand = new Dictionary<string, MessageCommand>();
        private Dictionary<string, MessageCommand> _commands = new Dictionary<string, MessageCommand>();
        private ITelegramBotClient _botClient;

        private Access _accessCommand;
        private Keyboard.Menu _menuKeyboardCommand;
        private Inline.Menu _menuInlineCommand;
        private Common.User _userCommand;


        private Dictionary<string, MessageCommand> messageCommands;
        private Dictionary<string, MessageCommand> messageCommandsPriority;
        private Dictionary<InlineCallbackCommands, MessageCommand> inlineCommands;

        public Router(ITelegramBotClient botClient)
        {
            _botClient              = botClient;
            messageCommands = new Dictionary<string, MessageCommand>();
            messageCommandsPriority = new Dictionary<string, MessageCommand>();
            inlineCommands = new Dictionary<InlineCallbackCommands, MessageCommand>();
            RegisterCommnad();
        }

        public void RegisterCommnad()
        {
            var messageMethods = MethodFinder.FindMessageMenuHandlers();
            var inlineMethods = MethodFinder.FindInlineMenuHandlers();
            foreach (var method in messageMethods)
            {
                bool priority = method.GetCustomAttribute<MessageMenuHandlerAttribute>().Priority;
                foreach (var command in method.GetCustomAttribute<MessageMenuHandlerAttribute>().Commands)
                {
                    Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(MessageCommand), method, false);
                    messageCommands.Add(command, (MessageCommand)serverMessageHandler);
                    if(priority)
                    {
                        messageCommandsPriority.Add(command, (MessageCommand)serverMessageHandler);
                    }
                }
            }

            foreach (var method in inlineMethods)
            {
                foreach (var command in method.GetCustomAttribute<InlineCallbackHandlerAttribute>().Commands)
                {
                    Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(MessageCommand), method, false);
                    inlineCommands.Add(command, (MessageCommand)serverMessageHandler);

                }
            }
        }



        public async Task ExecuteCommandByMessage(string command, Update update)
        {
            try
            {
                if(await StartHasDeepLink(command, update))
                    return;

                if(await IsHaveNextStep(command,update))
                    return;

                foreach (var commandExecute in messageCommands)
                {
                    if (command.ToLower() == commandExecute.Key.ToLower())
                    {
                        await commandExecute.Value(_botClient,update);
                        return;
                    }
                }

                await Access.CommandMissing(_botClient,update);
            }
            catch(Exception ex)
            {
                //TODO Logging
            }

        }



        public async Task ExecuteCommandByCallBack(Update update)
        {
            var command = InlineCallbackCommand.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                try
                {
                    foreach (var commandCallback in inlineCommands)
                    {
                        if (command.CommandType == commandCallback.Key)
                        {
                            await commandCallback.Value(_botClient, update);
                            return;
                        }
                    }

                    await Access.CommandMissing(_botClient, update, "CallBack - " + command.CommandType);
                }
                catch (Exception ex)
                {
                    TelegramService.GetInstance().InvokeErrorLog(ex);
                }
            }
        }

        public async Task<bool> IsHaveNextStep(string command, Update update)
        {
            if (update.HasStep())
            {
                foreach (var commandExecute in messageCommandsPriority)
                {
                    if (command.ToLower() == commandExecute.Key.ToLower())
                    {
                        await commandExecute.Value(_botClient, update);
                        return true;
                    }
                }
                await update.GetStepOrNull().Value(_botClient, update);
                return true;
            }
            return false;
        }

        public async Task<bool> StartHasDeepLink(string command, Update update)
        {
            if (command.ToLower().Contains("start") && command.Contains(" "))
            {
                var spl = command.Split(' ');
                if (!string.IsNullOrEmpty(spl[1]))
                {
                    await Access.StartWithArguments(_botClient, update, spl[1]);
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}
