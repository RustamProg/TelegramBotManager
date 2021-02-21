using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;

namespace TelegramBotManager.Models
{
    class TelegramConnection
    {
        private string _accessToken;
        public TelegramConnection(string accessToken)
        {
            _accessToken = accessToken;
        }

        public void PrintBotName()
        {
            TelegramBotClient botClient = new TelegramBotClient(_accessToken);
            var me = botClient.GetMeAsync().Result;
            MessageBox.Show($"Hello, my name is {me.FirstName} and my id is {me.Id}");
        }        
    }
}
