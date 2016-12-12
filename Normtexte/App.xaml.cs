using NormtexteUI.ViewModels;
using System.Windows;

namespace NormtexteUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // TODO: use inject?!
            var service = new WindowService();
            service.ShowMainWindow();
        }
    }
}
