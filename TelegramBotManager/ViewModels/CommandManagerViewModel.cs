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
using TelegramBotManager.Models;

namespace TelegramBotManager.ViewModels
{
    class CommandManagerViewModel : INotifyPropertyChanged
    {
        // Fields and constructor       
        private string _currentCommand;
        private string _currentResponse;
        public CommandManagerViewModel()
        {
            CommandsList = new ObservableCollection<TelegramCommand>();
        }

        //Properties
        public ObservableCollection<TelegramCommand> CommandsList { get; set; }
        public string CurrentCommand
        {
            get { return _currentCommand; }
            set
            {
                _currentCommand = value;
                OnPropertyChanged(nameof(CurrentCommand));
            }
        }
        public string CurrentResponse
        {
            get { return _currentResponse; }
            set
            {
                _currentResponse = value;
                OnPropertyChanged(nameof(CurrentResponse));
            }
        }

        // Commands
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
                {
                    // Creating new telegram command
                    TelegramCommand telegramCommand = new TelegramCommand()
                    {
                        Command = CurrentCommand,
                        Response = CurrentResponse
                    };

                    // Trying to get commands with the same name
                    IEnumerable<TelegramCommand> consistanceCheck = null;
                    try
                    {
                        consistanceCheck = CommandsList.Where(x => x.Command == telegramCommand.Command);
                    }                   
                    catch (Exception exception) {}
                    
                    // If command with the same name doesn't exists, add this command to commands list
                    if (consistanceCheck.Count() == 0)
                    {
                        CommandsList.Add(telegramCommand);
                        TelegramConnection.BotCommands.Add(telegramCommand);
                    }
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
