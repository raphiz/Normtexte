using Normtexte.ViewModels;
using System.Windows;

namespace Normtexte
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // TODO: Dependency Injection?!
            var vm = new MainWindowViewModel();
            MainWindow = new MainWindow(vm);
            vm.View = MainWindow as ITreeView;
            MainWindow.Show();
        }
    }
}
