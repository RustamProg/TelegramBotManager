using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotManager.Models
{
    public class TelegramMessage
    {
        public string Message { get; set; }
        public Chat ChatID { get; set; }
        public string DateTime { get; set; }
        public InlineKeyboardMarkup Keyboard { get; set; }
    }
}
