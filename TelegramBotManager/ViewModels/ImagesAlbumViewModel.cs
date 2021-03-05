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
    class ImagesAlbumViewModel
    {
        // Fields and constructor
        private string _linkText;

        public ImagesAlbumViewModel()
        {
            ImagesLinks = ResourcesCollections.ImagesLinks;
        }

        // Properties
        public static ObservableCollection<string> ImagesLinks { get; set; }
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
        private RelayCommand _saveImage;
        public RelayCommand SaveImage
        {
            get
            {
                return _saveImage ?? (_saveImage = new RelayCommand(obj =>
                {
                    if (LinkText.Length > 0 && !ResourcesCollections.ImagesLinks.Contains(LinkText))
                    {
                        ResourcesCollections.ImagesLinks.Add(LinkText);
                    }
                    else if (ResourcesCollections.ImagesLinks.Count >= 15)
                    {
                        MessageBox.Show("Unfortunately, you can't add more than 15 images, due to problems with scrollviewer (it will be fixed very soon)");
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
