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

        /// <summary>
        /// Creating an instance of connection to the bot
        /// </summary>
        /// <param name="accessToken">Access token of the bot (You can get in @BotFather)</param>
        public TelegramConnection(string accessToken)
        {
            _accessToken = accessToken;
            _botClient = new TelegramBotClient(_accessToken);
        }

        /// <summary>
        /// Simple method that shows bot id and bot name (Use it for testing connection)
        /// </summary>
        public void PrintBotName()
        {           
            var me = _botClient.GetMeAsync().Result;
            MessageBox.Show($"Hello, my name is {me.FirstName} and my id is {me.Id}");
        }

        /// <summary>
        /// Starts receiving messages
        /// </summary>
        public void StartReceivingMessages()
        {
            _botClient.OnMessage += OnGotMessage;
            _botClient.StartReceiving();
        }

        /// <summary>
        /// Stops receiving messages
        /// </summary>
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
