using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            // TODO: Extract!
            IDataObject dataObj = System.Windows.Clipboard.GetDataObject();
            string clipboardRawData = dataObj.GetData(DataFormats.CommaSeparatedValue) as string;
            if (clipboardRawData == null)
            {
                MessageBox.Show("Oups - Die Zwischenablage scheint nicht aus Excel zu kommen :/");
                return;
            }
            string[] tokens = System.Text.RegularExpressions.Regex.Split(
                clipboardRawData.Substring(1, clipboardRawData.Length - 2), @";");
            if(tokens.Length < 5)
            {
                MessageBox.Show("Hoppla, nicht alles markiert beim Kopieren?");
            }
            string title = tokens[1];
            string unit = tokens[2];
            string price = tokens[5];

        }
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            InitializeTestData();

            DataContext = this;
        }

        private void InitializeTestData()
        {

            var categoryVorbedinungen = new Category() { Name = "Vorbedinungen" };
            var categoryVorarbeiten = new Category() { Name = "Vorarbeiten" };
            var categoryBaustelleneinrichtung = new Category() { Name = "Baustelleneinrichtung" };

            Categories = new ObservableCollection<Category>()
            {
                categoryVorbedinungen,
                categoryVorarbeiten,
                categoryBaustelleneinrichtung
            };

            var categorySubVorarbeiten = new Category() { Name = "Vorarbeiten" };
            var categorySubGeruesten = new Category() { Name = "Gerüsten" };
            var categorySubGebuehren = new Category() { Name = "Gebühren" };

            categoryBaustelleneinrichtung.Categories = new ObservableCollection<Category>()
            {
                categoryVorarbeiten,
                categorySubGeruesten,
                categorySubGebuehren
            };

            var optionFassadenGerueste = new Category() { Name = "Fassadengerüste" };
            var optionRollGerueste = new Category() { Name = "Rollgerüste" };
            var optionBockGerueste = new Category() { Name = "Bockgerüste" };
            var optionTreppenGerueste = new Category() { Name = "Treppengerüste" };
            var optionGeruestAmDach = new Category() { Name = "Gerüste am Dach" };

            categorySubGeruesten.Options = new ObservableCollection<Category>()
            {
                optionFassadenGerueste,
                optionRollGerueste,
                optionBockGerueste,
                optionTreppenGerueste,
                optionGeruestAmDach
            };


        }
    }

    public class Category
    {
        public string Name { get; set; }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Category> Options { get; set; }

    }

    public class Option
    {
        public string Text { get; set; }
        public string Unit { get; set; }
        public ObservableCollection<Price> Prices { get; set; }
    }

    public class Price
    {
        public int from { get; set; }
        public int to { get; set; }
        public int pricePerUnit { get; set; }
    }
}
