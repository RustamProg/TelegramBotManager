using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBotManager.Models
{
    class TelegramConnection
    {
        private string _accessToken;
        private ITelegramBotClient _botClient;
        public TelegramConnection(string accessToken)
        {
            _accessToken = accessToken;
            _botClient = new TelegramBotClient(_accessToken);
        }

        public void PrintBotName()
        {           
            var me = _botClient.GetMeAsync().Result;
            MessageBox.Show($"Hello, my name is {me.FirstName} and my id is {me.Id}");
        }

        public void StartReceivingMessages()
        {
            _botClient.OnMessage += OnGotMessage;
            _botClient.StartReceiving();
        }

        public void StopReceivingMessages()
        {
            _botClient.StopReceiving();
        }
        private async void OnGotMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text == "/start")
                {
                    await _botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Let's begin to work!"
                        );
                }
            }
        }
    }
}
