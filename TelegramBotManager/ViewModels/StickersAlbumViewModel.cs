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

namespace TelegramBotManager.ViewModels
{
    class StickersAlbumViewModel : INotifyPropertyChanged
    {
        // Fields and constructor
        private ObservableCollection<string> _imageLinks;
        private string _linkText;

        public StickersAlbumViewModel()
        {
            ImageLinks = new ObservableCollection<string>()
            {
                "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp",
                "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/sticker-dali.webp",
                "https://chpic.su/_data/stickers/k/knight_vk/knight_vk_001.webp",
            };
        }

        // Properties
        public ObservableCollection<string> ImageLinks { get; set; }
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
                    if (LinkText.Length > 0 && !ImageLinks.Contains(LinkText))
                    {
                        ImageLinks.Add(LinkText);
                    }
                    else if (ImageLinks.Count >= 15)
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
