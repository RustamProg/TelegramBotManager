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
using TelegramBotManager.Views;

namespace TelegramBotManager.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        // private fields
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
        }

        // Properties
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
        private RelayCommand testCommand; // TEST COMMAND NEEDS TO BE DELETED
        public RelayCommand TestCommand
        {
            get
            {
                return testCommand ?? (testCommand = new RelayCommand(obj => {
                    CurrentWindow = obj as UserControl;
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
