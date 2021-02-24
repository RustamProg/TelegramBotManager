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
        


        // Constructor
        public TelegramLogInViewModel()
        {

        }

        // Properties
        

        // Commands


        // Property changed event handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
