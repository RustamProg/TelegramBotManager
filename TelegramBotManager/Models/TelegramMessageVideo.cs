using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotManager.Models
{
    public class TelegramMessageVideo: TelegramMessage
    {
        public string StickerURI { get; set; }
    }
}
