using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotManager.Services;

namespace TelegramBotManager.Models
{
    /// <summary>
    /// Singleton class to managebot
    /// </summary>
    public sealed class TelegramConnection
    {
        private string _accessToken;
        private ITelegramBotClient _botClient;
        private ReplyKeyboardMarkup _keyboard = new ReplyKeyboardMarkup(new[] 
        {
            new KeyboardButton("Hello"),
            new KeyboardButton("Goodbye"),
            /*new[]
            {
                new KeyboardButton("Hello"),
                new KeyboardButton("Hi"),
            },
            new[]
            {
                new KeyboardButton("Goodbye"),
                new KeyboardButton("Bye"),
            }*/
        });

        /// <summary>
        /// Realization of singleton class
        /// </summary>
        private static readonly Lazy<TelegramConnection> lazy =
            new Lazy<TelegramConnection>(() => new TelegramConnection());
        public static TelegramConnection Instance { get { return lazy.Value; } }

        public static List<TelegramCommand> BotCommands = new List<TelegramCommand>() { new TelegramCommand { Command = "hhh", Response = "TestTES" } }; 
        public ITelegramBotClient BotClient
        {
            get
            {
                return _botClient;
            }
            private set
            {
                _botClient = value;
            }
        }

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
            BotClient = new TelegramBotClient(_accessToken);
        }

        /// <summary>
        /// Simple method that shows bot id and bot name (Use it for testing connection)
        /// </summary>
        public void PrintBotName()
        {           
            var me = BotClient.GetMeAsync().Result;
            MessageBox.Show($"Hello, my name is {me.FirstName} and my id is {me.Id}");
        }

        /// <summary>
        /// Starts receiving messages
        /// </summary>
        public void StartReceivingMessages()
        {
            BotClient.OnMessage += OnGotMessage;
            BotClient.StartReceiving();
        }

        /// <summary>
        /// Stops receiving messages
        /// </summary>
        public void StopReceivingMessages()
        {
            BotClient.StopReceiving();
        }

        public async void SendMessage(TelegramMessage telMessage)
        {
            await BotClient.SendTextMessageAsync(
                            chatId: telMessage.ChatID,
                            text: telMessage.Message,
                            replyMarkup: telMessage.Keyboard
                        );
        }

        private async void OnGotMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text == "/register")
                {
                    var existingUsers = ChatsSavesManager.OpenChatsToJson();
                    if (existingUsers != null && !existingUsers.ContainsKey("@" + e.Message.Chat.Username))
                    {
                        Dictionary<string, int> userToRegister = new Dictionary<string, int>() { { "@" + e.Message.Chat.Username, Convert.ToInt32(e.Message.Chat.Id) } };
                        ChatsSavesManager.SaveChatsToJson(userToRegister);
                        await BotClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: $"Your account @{e.Message.Chat.Username} was successfully registered!",
                            replyMarkup: _keyboard
                        );
                    }
                    else
                    {
                        await BotClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: $"Account with username @{e.Message.Chat.Username} already registered or users list file didn't found",
                            replyMarkup: _keyboard
                        );
                    }
                    return;
                }
                try
                {
                    var foundCommands = BotCommands.Where(x => x.Command == e.Message.Text);
                    string messageToSend = foundCommands.First().Response;
                    await BotClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: messageToSend,
                            replyMarkup: _keyboard
                        );
                }
                catch (Exception sendMsgException)
                {
                    await BotClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: $"[Send message Exception]: {sendMsgException.Message} - {DateTime.Now.ToString("D")} \n I don't know what to answer this command \n Your username is {e.Message.Chat.Username}"
                        );
                }
            }
        }
    }
}
