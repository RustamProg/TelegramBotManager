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
using TelegramBotManager.Services;

namespace TelegramBotManager.ViewModels
{
    class StickersAlbumViewModel : INotifyPropertyChanged
    {
        // Fields and constructor
        private string _linkText;

        public StickersAlbumViewModel()
        {
            StickersLinks = ResourcesCollections.StickersLinks;
        }

        // Properties
        public static ObservableCollection<string> StickersLinks { get; set; }
        public string LinkText
        {
            get { return _linkText; }
            set
            {
                _linkText = value;
                OnPropertyChanged(nameof(LinkText));
            }
        }

        //Commands
        private RelayCommand _saveSticker;
        public RelayCommand SaveSticker
        {
            get
            {
                return _saveSticker ?? (_saveSticker = new RelayCommand(obj =>
                {
                    if (LinkText.Length > 0 && !ResourcesCollections.StickersLinks.Contains(LinkText))
                    {
                        ResourcesCollections.StickersLinks.Add(LinkText);
                    }
                    else if (StickersLinks.Count >= 15)
                    {
                        MessageBox.Show("Unfortunately, you can't add more than 15 stickers, due to problems with scrollviewer (it will be fixed very soon)");                       
                    }
                    LinkText = "";
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
