using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotManager.Services
{
    public static class ResourcesCollections
    {
        public static ObservableCollection<string> StickersLinks { get; set; }
        public static ObservableCollection<string> ImagesLinks { get; set; }
        static ResourcesCollections()
        {
            StickersLinks = new ObservableCollection<string>()
            {
                "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp",
                "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/sticker-dali.webp",
                "https://chpic.su/_data/stickers/k/knight_vk/knight_vk_001.webp",
            };
            ImagesLinks = new ObservableCollection<string>()
            {
                "https://i.artfile.ru/4724x2953_1142326_[www.ArtFile.ru].jpg",
                "https://i.artfile.ru/4724x2953_1142326_[www.ArtFile.ru].jpg",
            };
        }
    }
}
