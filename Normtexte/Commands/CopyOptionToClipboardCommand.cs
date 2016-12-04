using Normtexte.ViewModels;
using System;
using System.Windows.Input;

namespace Normtexte.Commands
{
    class CopyOptionToClipboardCommand : ICommand
    {

        public CopyOptionToClipboardCommand(MainWindowViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        private MainWindowViewModel _ViewModel;

        // TODO: What about RoutedCommand?!
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanCopyOptionToClipboard;
        }

        public void Execute(object parameter)
        {
            _ViewModel.CopySelectedOptionToClipboad();
        }
    }
}

