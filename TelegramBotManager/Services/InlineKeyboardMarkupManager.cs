using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotManager.Services
{
    class InlineKeyboardMarkupManager
    {
        public static InlineKeyboardButton[][] GetInlineKeyboard(string[] stringArray)
        {
            var keyboardInline = new InlineKeyboardButton[1][];
            var keyboardButtons = new InlineKeyboardButton[stringArray.Length];
            for (var i = 0; i < stringArray.Length; i++)
            {
                keyboardButtons[i] = new InlineKeyboardButton
                {
                    Text = stringArray[i],
                    CallbackData = "Some Callback Data",
                };
            }
            keyboardInline[0] = keyboardButtons;
            return keyboardInline;
        }
    }
}
