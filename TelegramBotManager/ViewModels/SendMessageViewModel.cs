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
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotManager.Models;
using TelegramBotManager.Services;

namespace TelegramBotManager.ViewModels
{
    class SendMessageViewModel: INotifyPropertyChanged
    {
        // Fields      
        private string _currentMessage;
        private string _chatID;
        private Dictionary<string, int> _chatList = new Dictionary<string, int>();
        private bool _isReplyMarkupEnabled = false;
        private List<string> _chatKeysList;
        private bool _isStickersPopupOpen = false;
        private bool _isImagesPopupOpen = false;

        public SendMessageViewModel()
        {
            Messages = new ObservableCollection<TelegramMessage>();
            _chatList = _chatList.LoadChatsFromJsonToDictionary();
            _chatKeysList = _chatList.Keys.ToList<string>();
            StickersLinks = ResourcesCollections.StickersLinks;
            ImagesLinks = ResourcesCollections.ImagesLinks;
            ChatID = _chatList.First().Key;
        }

        //Properties
        public ObservableCollection<string> StickersLinks { get; set; }
        public ObservableCollection<string> ImagesLinks { get; set; }
        public ObservableCollection<TelegramMessage> Messages { get; set; }
        public string ChatID
        {
            get { return _chatID; }
            set
            {
                _chatID = value;
                OnPropertyChanged(nameof(ChatID));
            }
        }
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                _currentMessage = value;
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }
        public bool IsReplyMarkupEnabled
        {
            get { return _isReplyMarkupEnabled; }
            set
            {
                _isReplyMarkupEnabled = value;
                OnPropertyChanged(nameof(IsReplyMarkupEnabled));
            }
        }
        public List<string> ChatKeysList
        {
            get { return _chatKeysList; }
            set
            {
                _chatKeysList = value;
                OnPropertyChanged(nameof(ChatKeysList));
            }
        }
        public bool IsStickersPopupOpen
        {
            get { return _isStickersPopupOpen; }
            set
            {
                _isStickersPopupOpen = value;
                OnPropertyChanged(nameof(IsStickersPopupOpen));
            }
        }
        public bool IsImagesPopupOpen
        {
            get { return _isImagesPopupOpen; }
            set
            {
                _isImagesPopupOpen = value;
                OnPropertyChanged(nameof(IsImagesPopupOpen));
            }
        }

        // Commands
        private RelayCommand _sendMessage;
        public RelayCommand SendMessage
        {
            get
            {
                return _sendMessage ?? (_sendMessage = new RelayCommand(obj =>
                {
                    if (CurrentMessage.Length > 0 && ChatID != null && _chatList.ContainsKey(ChatID))
                    {
                        Chat chat = new Chat(){ Id = _chatList[ChatID] };
                        InlineKeyboardMarkup keyboardToSend = null;

                        // If needed to send with keyboard reply
                        if (IsReplyMarkupEnabled)
                        {
                            // Count number of enabled buttons
                            int buttonsLength = 0;
                            for (int i = 0; i < 9; i++)
                            {
                                if (ReplyMarkupViewModel.AllCheckedButtons[i])
                                {
                                    buttonsLength++;
                                }
                            }

                            // Creating array of enabled button's texts
                            string[] buttonItem = new string[buttonsLength];
                            for (int button = 0; button < 9; button++)
                            {
                                if (ReplyMarkupViewModel.AllCheckedButtons[button])
                                {
                                    buttonItem[button] = ReplyMarkupViewModel.MarkupButtons[button];
                                }
                            }

                            // Creating keyboard
                            keyboardToSend = new InlineKeyboardMarkup(InlineKeyboardMarkupManager.GetInlineKeyboard(buttonItem));
                        }
                        
                        TelegramMessage messageToSend = new TelegramMessage()
                        {
                            Message = CurrentMessage,
                            ChatID = chat,
                            DateTime = DateTime.Now.ToString("dd.MM.yyyy"),
                            Keyboard = keyboardToSend
                        };
                        Messages.Add(messageToSend);
                        TelegramConnection.Instance.SendMessage(messageToSend);
                        CurrentMessage = "";
                    }
                }));
            }
        }

        private RelayCommand _switchPopup;
        public RelayCommand SwitchPopup
        {
            get
            {
                return _switchPopup ?? (_switchPopup = new RelayCommand(obj =>
                {
                    IsStickersPopupOpen = !IsStickersPopupOpen;
                }));
            }
        }

        private RelayCommand _sendSticker;
        public RelayCommand SendSticker
        {
            get
            {
                return _sendSticker ?? (_sendSticker = new RelayCommand(obj =>
                {
                    Chat chat = new Chat() { Id = _chatList[ChatID] };
                    TelegramMessageSticker messageToSend = new TelegramMessageSticker()
                    {
                        ChatID = chat,
                        StickerURI = obj.ToString(),
                        Message = "[Sended sticker]"
                    };
                    Messages.Add(messageToSend);
                    TelegramConnection.Instance.SendSticker(messageToSend);
                    CurrentMessage = "";
                }));
            }
        }

        private RelayCommand _sendImage;
        public RelayCommand SendImage
        {
            get
            {
                return _sendImage ?? (_sendImage = new RelayCommand(obj =>
                {
                    Chat chat = new Chat() { Id = _chatList[ChatID] };
                    TelegramMessageImage messageToSend = new TelegramMessageImage()
                    {
                        ChatID = chat,
                        ImageURI = obj.ToString(),
                        Message = "[Sended Image]: " + CurrentMessage,
                        Caption = CurrentMessage
                    };
                    Messages.Add(messageToSend);
                    TelegramConnection.Instance.SendImage(messageToSend);
                    CurrentMessage = "";
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
