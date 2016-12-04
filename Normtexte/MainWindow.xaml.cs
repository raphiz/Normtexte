using netoaster;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Normtexte
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static RoutedUICommand PasteFromExcel = new RoutedUICommand("Aus Excel Einfüen", "PasteFromExcel", typeof(MainWindow));
        public static RoutedUICommand CopyToClipboard = new RoutedUICommand("Option in Zwischenablage Kopieren", "CopyForExcel", typeof(MainWindow));

        private void CopyToClipboardCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: SelectedItem is WRONG!?! MosueOver
            if(OptionTree.SelectedItem is Option)
            {
                Option option = OptionTree.SelectedItem as Option;
                string data = String.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\"", option.LongText, option.Unit, 10, "à Fr.", 100,  "Fr");
                Clipboard.SetText(
                  data, TextDataFormat.CommaSeparatedValue
                  );

                SuccessToaster.Toast(this, title: "Auswah in Zwischenablage kopiert!",
                   position: ToasterPosition.PrimaryScreenBottomRight);
            }


        }

        private void PasteFromExcelCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            // TODO: Extract!
            IDataObject dataObj = Clipboard.GetDataObject();
            string clipboardRawData = dataObj.GetData(DataFormats.CommaSeparatedValue) as string;
            if (clipboardRawData == null)
            {
                WarningToaster.Toast(this, title: "Problem beim Einfügen", 
                                           message: "Die Zwischenablage scheint nicht aus Excel zu kommen ☹",
                                           position: ToasterPosition.PrimaryScreenBottomRight);
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

        private void OptionTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CopyToClipboard.Execute(null, null);
        }

        private void OptionTree_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            CopyToClipboard.Execute(null, null);

        }
    }

    public class Category : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
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
