using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TelegramBotManager.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        // private fields
        private UserControl currentWindow;


        //Constructor
        public MainWindowViewModel()
        {
            
        }

        // Properties
        public UserControl CurrentWindow
        {
            get { return currentWindow; }
            set
            {
                OnPropertyChanged(nameof(CurrentWindow));
            }
        }

        // Commands

        // Property changed event handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
