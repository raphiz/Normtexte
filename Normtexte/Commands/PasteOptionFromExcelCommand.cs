
using Normtexte.ViewModels;
using System.Windows.Input;
using System;

namespace Normtexte.Commands
{
    class PasteOptionFromExcelCommand : ICommand
    {

        public PasteOptionFromExcelCommand(MainWindowViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        private MainWindowViewModel _ViewModel;

        // TODO: What about RoutedCommand?!
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanPasteOptionFromExcel;
        }

        public void Execute(object parameter)
        {
            _ViewModel.CreateOptionFromClipboard();
        }
    }
}
