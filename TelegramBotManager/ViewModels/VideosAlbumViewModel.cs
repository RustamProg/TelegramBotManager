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
    class VideosAlbumViewModel
    {
        // Fields and constructor
        private string _linkText;

        public VideosAlbumViewModel()
        {
            VideosLinks = new ObservableCollection<string>()
            {
                "https://i.artfile.ru/4724x2953_1142326_[www.ArtFile.ru].jpg",
                "https://i.artfile.ru/4724x2953_1142326_[www.ArtFile.ru].jpg",
            };
        }

        // Properties
        public static ObservableCollection<string> VideosLinks { get; set; }
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
        private RelayCommand _saveVideo;
        public RelayCommand SaveVideo
        {
            get
            {
                return _saveVideo ?? (_saveVideo = new RelayCommand(obj =>
                {
                    if (LinkText.Length > 0 && !VideosLinks.Contains(LinkText))
                    {
                        VideosLinks.Add(LinkText);
                    }
                    else if (VideosLinks.Count >= 15)
                    {
                        MessageBox.Show("Unfortunately, you can't add more than 15 videos, due to problems with scrollviewer (it will be fixed very soon)");
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
