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
using System.Windows.Controls;
using TelegramBotManager.Models;
using TelegramBotManager.Views;

namespace TelegramBotManager.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        // private fields
        private string _accessToken;
        private UserControl currentWindow;
        public ObservableCollection<UserControl> Windows { get; set; }

        //Constructor
        public MainWindowViewModel()
        {
            Windows = new ObservableCollection<UserControl>()
            {
                new TelegramLogInView(),
                new BotManagementView()
            };
            currentWindow = Windows[0];


            /*
             NEED TO BE DELETED!             
             */
            AccessToken = "1127413447:AAHkv1to7QHXmwIC26hqSe7lfsgYWTkgia0";
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
        public UserControl CurrentWindow
        {
            get { return currentWindow; }
            set
            {
                currentWindow = value;
                OnPropertyChanged(nameof(CurrentWindow));
            }
        }


        // Commands
        private RelayCommand openManagerCommand; // TEST COMMAND NEEDS TO BE DELETED 
        public RelayCommand OpenManagerCommand
        {
            get
            {
                return openManagerCommand ?? (openManagerCommand = new RelayCommand(obj => {
                    CurrentWindow = obj as UserControl;
                    TelegramConnection.Instance.AccessToken = AccessToken;
                    TelegramConnection.Instance.StartReceivingMessages();
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
