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
    /// <summary>
    /// Singleton class to managebot
    /// </summary>
    public sealed class TelegramConnection
    {
        private string _accessToken;
        private ITelegramBotClient _botClient;
        private Dictionary<string, string> _botCommands = new Dictionary<string, string>() { { "/start", "Let's get started" } };

        /// <summary>
        /// Realization of singleton class
        /// </summary>
        private static readonly Lazy<TelegramConnection> lazy =
            new Lazy<TelegramConnection>(() => new TelegramConnection());
        public static TelegramConnection Instance { get { return lazy.Value; } }

        /// <summary>
        /// You can set access token, but you can't get it back
        /// </summary>
        public string AccessToken
        {
            private get { return _accessToken; }
            set
            {
                _accessToken = value;
                SetConnection(_accessToken);
            }
        }

        /// <summary>
        /// Sets connection to the bot using Access Token
        /// </summary>
        /// <param name="accessToken">Access token of the bot. You can get it in @BotFather telegram</param>
        private void SetConnection(string accessToken)
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

        public async void SendMessage(TelegramMessage telMessage)
        {
            await _botClient.SendTextMessageAsync(
                            chatId: telMessage.ChatID,
                            text: telMessage.Message
                        );
        }

        private async void OnGotMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                try
                {
                    string messageToSend = _botCommands[e.Message.Text];
                    await _botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: messageToSend
                        );
                }
                catch (Exception sendMsgException)
                {
                    await _botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: $"[Send message Exception]: {sendMsgException.Message} - {DateTime.Now.ToString("D")} \n I don't know what to answer this command"
                        );
                }
            }
        }
    }
}
