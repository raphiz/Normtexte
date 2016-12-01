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

            categoryBaustelleneinrichtung.Categories = new List<Category>()
            {
                categoryVorarbeiten,
                categorySubGeruesten,
                categorySubGebuehren
            };
            
            var prices = new List<Price>()
            {
                new Price() { from=0, to=10, pricePerUnit=100 },
                new Price() { from = 10, to = 100, pricePerUnit = 80 }
        };

            var optionFassadenGerueste = new Option() { Prices= prices, ShortText = "Fassadengerüste" , LongText="Bestehende Gaube komplett abbrechen und zur Entsorgung bereitstellen. \n Dach im Bereich der neuen Gaube ausdecken,\nLattung und Konterlattung abbrechen,\nUnterdach und Dämmung ausbauen und zur Entsorgung bereitstellen"};
            var optionRollGerueste = new Option() { ShortText = "Rollgerüste" };
            var optionBockGerueste = new Option() { ShortText = "Bockgerüste" };
            var optionTreppenGerueste = new Option() { ShortText = "Treppengerüste" };
            var optionGeruestAmDach = new Option() { ShortText = "Gerüste am Dach" };

            categorySubGeruesten.Options = new List<Option>()
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

        public List<Category> Categories { get; set; }
        public List<Option> Options { get; set; }

        public List<Object> Children {
            get {
                List<Object>result = new List<Object>();
                if (Categories != null) result.AddRange(Categories);
                if (Options != null) result.AddRange(Options);
                return result;
            }
        }

    }

    public class Option
    {
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public string Unit { get; set; }
        public List<Price> Prices { get; set; }
    }

    public class Price
    {
        public int from { get; set; }
        public int to { get; set; }
        public int pricePerUnit { get; set; }
    }
}
