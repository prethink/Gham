﻿using System;
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
        public static ReplyKeyboardMarkup GenerateMenu(int maxColumn, List<string> menu, string mainMenu = "")
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
    }
}
