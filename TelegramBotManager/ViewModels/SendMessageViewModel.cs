using personal_game_library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBotManager.Models;

namespace TelegramBotManager.ViewModels
{
    class SendMessageViewModel: INotifyPropertyChanged
    {
        // Fields
        
        private string _currentMessage;
        private string _chatID;
        private Dictionary<string, int> chatList;

        public SendMessageViewModel()
        {
            Messages = new ObservableCollection<TelegramMessage>();
            chatList = new Dictionary<string, int>()
            {
                { "@darkwolfing", 980350542 },
            };           
        }

        //Properties
        public ObservableCollection<TelegramMessage> Messages { get; set; }
        public string ChatID
        {
            get { return _chatID; }
            set
            {
                _chatID = value;
                OnPropertyChanged(nameof(ChatID));
            }
        }
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                _currentMessage = value;
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }

        // Commands
        private RelayCommand _sendMessage;
        public RelayCommand SendMessage
        {
            get
            {
                return _sendMessage ?? (_sendMessage = new RelayCommand(obj =>
                {
                    if (CurrentMessage.Length > 0 && chatList.ContainsKey(ChatID))
                    {
                        Chat chat = new Chat(){ Id = chatList[ChatID] };
                        TelegramMessage messageToSend = new TelegramMessage()
                        {
                            Message = CurrentMessage,
                            ChatID = chat,
                            DateTime = DateTime.Now.ToString("dd.MM.yyyy"),
                        };
                        Messages.Add(messageToSend);
                        TelegramConnection.Instance.SendMessage(messageToSend);
                        CurrentMessage = "";
                    }
                }));
            }
        }

        // Property changed event handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
