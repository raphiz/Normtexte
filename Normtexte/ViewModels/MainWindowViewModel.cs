
using NormtexteUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NormtexteUI.Helpers;

namespace NormtexteUI.ViewModels
{
    internal class MainWindowViewModel
    {
        public ObservableCollection<Category> Categories { get; private set; }
        public ICommand PasteCommand { get; private set; }

        public ICommand CopyCommand { get; private set; }
        private WindowService Service { get; set; }

        public MainWindowViewModel(WindowService service)
        {
            InitializeTestData();
            Service = service;
            CopyCommand = new RelayCommand<object>(CopySelectedOptionToClipboad, raw => raw is Option);
            PasteCommand = new RelayCommand<object>(raw => CreateOptionFromClipboard(), raw => true);
        }

        internal void CreateOptionFromClipboard()
        {
            var data = ClipboardHelpers.GetExcelData();
            if (data == null)
            {
                Toaster.Warning(Properties.Resources.pasteFailTitle, Properties.Resources.pasteFailMessage);
                return;
            }
            if (data.Length < 5)
            {
                Toaster.Warning(Properties.Resources.pasteFailTitle, Properties.Resources.pasteFailIncompleteMessage);
            }

            var option = new Option
            {
                LongText = data[1],
                Unit = data[2],
                Prices = new ObservableCollection<Price>
                {
                    new Price { From = 0, To = 0, PricePerUnit = double.Parse(data[5]) },
                }
            };
            Service.ShowOptionWindow(option);

        }

        internal void CopySelectedOptionToClipboad(object raw)
        {
            var option = raw as Option;
            ClipboardHelpers.SetExcelData(option.LongText, option.Unit, 10, "à Fr.", 100, "Fr");
            Toaster.Success(Properties.Resources.copySuccessTitle);
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

            var categorySubGeruesten = new Category() { Name = "Gerüsten" };
            var categorySubGebuehren = new Category() { Name = "Gebühren" };

            categoryBaustelleneinrichtung.Categories = new ObservableCollection<Category>()
            {
                categoryVorarbeiten,
                categorySubGeruesten,
                categorySubGebuehren
            };

            var prices = new ObservableCollection<Price>()
            {
                new Price() { From=0, To=10, PricePerUnit=100 },
                new Price() { From = 10, To = 100, PricePerUnit = 80 }
        };

            var optionFassadenGerueste = new Option() { Prices = prices, ShortText = "Fassadengerüste", LongText = "Bestehende Gaube komplett abbrechen und zur Entsorgung bereitstellen. \n Dach im Bereich der neuen Gaube ausdecken,\nLattung und Konterlattung abbrechen,\nUnterdach und Dämmung ausbauen und zur Entsorgung bereitstellen" };
            var optionRollGerueste = new Option() { ShortText = "Rollgerüste" };
            var optionBockGerueste = new Option() { ShortText = "Bockgerüste" };
            var optionTreppenGerueste = new Option() { ShortText = "Treppengerüste" };
            var optionGeruestAmDach = new Option() { ShortText = "Gerüste am Dach" };

            categorySubGeruesten.Options = new ObservableCollection<Option>()
            {
                optionFassadenGerueste,
                optionRollGerueste,
                optionBockGerueste,
                optionTreppenGerueste,
                optionGeruestAmDach
            };
        }
    }
}
