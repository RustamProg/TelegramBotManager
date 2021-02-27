using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TelegramBotManager.ViewModels
{
    class ReplyMarkupViewModel : INotifyPropertyChanged
    {
        //Properties
        public static ObservableCollection<string> MarkupButtons { get; set; }
        public static ObservableCollection<bool> AllCheckedButtons { get; set; }
        public ReplyMarkupViewModel()
        {
            MarkupButtons = new ObservableCollection<string>()
            { 
                "Button1", "Button2", "Button3",
                "Button4", "Button5", "Button6",
                "Button7", "Button8", "Button9"
            };
            AllCheckedButtons = new ObservableCollection<bool>()
            {
                true, true, true, false, false, false, false, false, false,
            };
        }

        // Property changed event handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
