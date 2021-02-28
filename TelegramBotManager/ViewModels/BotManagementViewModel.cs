using personal_game_library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TelegramBotManager.Views;

namespace TelegramBotManager.ViewModels
{
    class BotManagementViewModel : INotifyPropertyChanged
    {
        private string _botName;
        private string _accessToken;
        private UserControl _selectedControl;
        public ObservableCollection<UserControl> Controls { get; set; }

        public BotManagementViewModel()
        {
            Controls = new ObservableCollection<UserControl>()
            {
                new SendMessageView(),
                new ReplyMarkupView(),
                new CommandsManagerView(),
                new StickersAlbumView(),
            };
            _selectedControl = Controls[0];
        }

        // Properties
        public UserControl SelectedControl
        {
            get { return _selectedControl; }
            set
            {
                _selectedControl = value;
                OnPropertyChanged(nameof(SelectedControl));
            }
        }
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

        // Commands
        private RelayCommand _openView;
        public RelayCommand OpenView
        {
            get
            {
                return _openView ?? (_openView = new RelayCommand(obj =>
                {
                    SelectedControl = obj as UserControl;
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
