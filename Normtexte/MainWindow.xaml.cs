using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Normtexte
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static RoutedUICommand PasteFromExcel = new RoutedUICommand("Aus Excel Einfüen", "PasteFromExcel", typeof(MainWindow));

        private void PasteFromExcelCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
