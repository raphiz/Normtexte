
using Normtexte.Commands;
using Normtexte.Models;
using Normtexte.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Normtexte.ViewModels
{
    class MainWindowViewModel
    {
        public ObservableCollection<Category> Categories { get; private set; }
        public ICommand PasteCommand { get; private set; }
        public bool CanPasteOptionFromExcel { get { return true; } }

        public ICommand CopyCommand { get; private set; }
        public bool CanCopyOptionToClipboard { get { return View.SelectedOption() != null; } }

        public ITreeView View { get; internal set; }

        public MainWindowViewModel()
        {
            InitializeTestData();
            PasteCommand = new PasteOptionFromExcelCommand(this);
            CopyCommand = new CopyOptionToClipboardCommand(this);
        }

        internal void CreateOptionFromClipboard()
        {
            string[] data = ClipboardUtils.GetExcelData();
            if (data == null)
            {
                Toaster.Warning(Properties.Resources.pasteFailTitle, Properties.Resources.pasteFailMessage);
                return;
            }
            if (data.Length < 5)
            {
                Toaster.Warning(Properties.Resources.pasteFailTitle, Properties.Resources.pasteFailIncompleteMessage);
            }
        }

        internal void CopySelectedOptionToClipboad()
        {
            Option option = View.SelectedOption();
            if (option != null)
            {
                ClipboardUtils.SetExcelData(option.LongText, option.Unit, 10, "à Fr.", 100, "Fr");
                Toaster.Success(Properties.Resources.copySuccessTitle);
            }
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

            var prices = new ObservableCollection<Price>()
            {
                new Price() { from=0, to=10, pricePerUnit=100 },
                new Price() { from = 10, to = 100, pricePerUnit = 80 }
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
