using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Gham.Helpers
{
    public static class MenuGenerator
    {
        /// <summary>
        /// Генерирует меню для бота
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов</param>
        /// <param name="menu">Коллекция меню</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню</param>
        /// <returns>Готовое меню</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<string> menu, string mainMenu = "")
        {
            List<List<KeyboardButton>> buttons = new();

            int row = 0;
            int currentElement = 0;

            foreach (var item in menu)
            {
                if (currentElement == 0)
                {
                    buttons.Add(new List<KeyboardButton>());
                    buttons[row].Add(new KeyboardButton(item));
                }
                else
                {
                    buttons[row].Add(new KeyboardButton(item));
                }

                currentElement++;

                if (currentElement >= maxColumn)
                {
                    currentElement = 0;
                    row++;
                }
            }

            if (!string.IsNullOrWhiteSpace(mainMenu))
            {
                buttons.Add(new List<KeyboardButton>());
                if (currentElement != 0)
                    row++;
                buttons[row].Add(mainMenu);
            }

            ReplyKeyboardMarkup replyKeyboardMarkup = new(buttons)
            {
                ResizeKeyboard = true
            };

            return replyKeyboardMarkup;
        }

        public static InlineKeyboardMarkup InlineCallBackOrUrl(int maxColumn, List<IInlineContent> menu)
        {
            List<List<InlineKeyboardButton>> buttons = new();

            int row = 0;
            int currentElement = 0;

            foreach (var item in menu)
            {
                if (currentElement == 0)
                {
                    buttons.Add(new List<InlineKeyboardButton>());
                    buttons[row].Add(GetInlineButton(item));
                }
                else
                {
                    buttons[row].Add(GetInlineButton(item));
                }

                currentElement++;

                if (currentElement >= maxColumn)
                {
                    currentElement = 0;
                    row++;
                }
            }

            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }

        public static InlineKeyboardButton GetInlineButton(IInlineContent inlineData)
        {
            if(inlineData is InlineCallbackCommand)
            {
                return InlineKeyboardButton.WithCallbackData(inlineData.GetTextButton(), inlineData.GetContent());
            }
            else if(inlineData is InlineURL)
            {
                return InlineKeyboardButton.WithUrl(inlineData.GetTextButton(), inlineData.GetContent());
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        public static InlineKeyboardMarkup UnitInlineKeyboard(params InlineKeyboardMarkup[] keyboards)
        {
            List<IEnumerable<InlineKeyboardButton>> buttons = new();
            foreach (var keyboard in keyboards)
            {
                buttons.AddRange(keyboard.InlineKeyboard);
            }
            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }
    }
}
