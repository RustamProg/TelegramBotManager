using personal_game_library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TelegramBotManager.Models;
using TelegramBotManager.Views;

namespace TelegramBotManager.ViewModels
{
    class TelegramLogInViewModel: INotifyPropertyChanged
    {
        // Private fields
        private string _accessToken;


        // Constructor
        public TelegramLogInViewModel()
        {

        }

        // Properties
        public string AccessToken
        {
            get { return _accessToken; }
            set
            {
                _accessToken = value;
                OnPropertyChanged(nameof(AccessToken));
            }
        }

        // Commands
        private RelayCommand connectToBot;
        public RelayCommand ConnectToBot
        {
            get
            {
                return connectToBot ?? (connectToBot = new RelayCommand(obj =>
                {
                    /*TelegramConnection telegramConnection = new TelegramConnection(AccessToken);
                    telegramConnection.StartReceivingMessages();*/
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
