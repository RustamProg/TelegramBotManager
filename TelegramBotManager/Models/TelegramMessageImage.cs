using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotManager.Models
{
    public class TelegramMessageImage : TelegramMessage
    {
        public string ImageURI { get; set; }
        public string Caption { get; set; }
    }
}
