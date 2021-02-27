using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotManager.Models
{
    public class TelegramCommand
    {
        public string Command { get; set; }
        public string Response { get; set; }

        public override string ToString()
        {
            return Command + " - " + Response;
        }
    }
}
