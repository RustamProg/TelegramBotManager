using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotManager.ViewModels
{
    class BotManagementViewModel : INotifyPropertyChanged
    {
        private string _botName;
        private string _accessToken;

        // Properties
        public string AccessToken
        {
            get
            {
                return "Access token: " + _accessToken.Substring(0, 20) + "**********...";
            }
            set
            {
                _accessToken = value;
                OnPropertyChanged(nameof(AccessToken));
            }
        }
        public string BotName
        {
            get { return _botName; }
            set
            {
                _botName = value;
                OnPropertyChanged(nameof(BotName));
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
