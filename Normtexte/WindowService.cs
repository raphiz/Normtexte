using NormtexteUI.Models;
using NormtexteUI.ViewModels;
using NormtexteUI.Views;

namespace NormtexteUI
{
    public class WindowService
    {
        private static MainWindow _mainWindow;
        private static MainWindowViewModel _mainWindowViewModel;
        internal void ShowMainWindow()
        {
            if (_mainWindowViewModel == null)
            {
                _mainWindowViewModel = new MainWindowViewModel(this);
                _mainWindow = new MainWindow {DataContext = _mainWindowViewModel};
            }
            
            _mainWindow.Show();
        }
        internal bool ShowOptionWindow(Option option)
        {
            var vm = new NewOptionViewModel(option);
            var win = new NewOptionWindow {DataContext = vm, Owner = _mainWindow};

            win.Owner.ShowInTaskbar = false;
            var result = win.ShowDialog() ?? false;
            win.Owner.ShowInTaskbar = true;

            return result;
        }
    }
}