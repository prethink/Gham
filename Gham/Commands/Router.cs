using Gham.Commands.Common;
using Gham.Commands.Keyboard;
using Gham.Helpers;
using Gham.Helpers.Extensions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public const string MAIN_MENU           = "Главное меню";
        public const string INLINE_MENU         = "Inline меню";
        public const string ECHO                = "ЭХО";
        #endregion

        #region InlineCommand
        #endregion

        delegate Task CommandReplay(Update update);
        delegate Task CommandInline(Update update,CallbackCommand command);
        private Dictionary<string, CommandReplay> _priorityCommand = new Dictionary<string, CommandReplay>();
        private Dictionary<string, CommandReplay> _commands = new Dictionary<string, CommandReplay>();
        private ITelegramBotClient _botClient;

        private Access _accessCommand;
        private Keyboard.Menu _menuKeyboardCommand;
        private Inline.Menu _menuInlineCommand;
        private Common.User _userCommand;


        public Router(ITelegramBotClient botClient)
        {
            _botClient              = botClient;

            _accessCommand          = new Access(_botClient);
            _menuKeyboardCommand    = new Keyboard.Menu(_botClient);
            _menuInlineCommand      = new Inline.Menu(_botClient);
            _userCommand            = new Common.User(_botClient);


            RegisterPriorityCommand();
            RegisterCommand();
        }

        public void RegisterPriorityCommand()
        {
            _priorityCommand.Add(START, _accessCommand.Start);
            _priorityCommand.Add(MAIN_MENU, _menuKeyboardCommand.MainMenu);
            _priorityCommand.Add(INLINE_MENU, _menuInlineCommand.MenuCallBack);
        }

        public void RegisterCommand()
        {
            _commands.Add(ECHO, _userCommand.Echo);
            foreach (var command in _priorityCommand)
            {
                _commands.Add(command.Key,command.Value);
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

                foreach (var commandExecute in _commands)
                {
                    if (command.ToLower() == commandExecute.Key.ToLower())
                    {
                        await commandExecute.Value(update);
                        return;
                    }
                }

                await _accessCommand.CommandMissing(update);
            }
            catch(Exception ex)
            {

            }

        }



        public async Task ExecuteCommandByCallBack(Update update)
        {
            var command = CallbackCommand.GetCommandByCallback(update.CallbackQuery.Data);
            if(command != null)
            {

            }
        }

        public async Task<bool> IsHaveNextStep(string command, Update update)
        {
            if (update.HasStep())
            {
                foreach (var commandExecute in _priorityCommand)
                {
                    if (command.ToLower() == commandExecute.Key.ToLower())
                    {
                        await commandExecute.Value(update);
                        return true;
                    }
                }
                await update.GetStepOrNull().Value(update);
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
                    await _accessCommand.StartWithArguments(update, spl[1]);
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}
