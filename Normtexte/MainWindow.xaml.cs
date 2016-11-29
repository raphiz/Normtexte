﻿using System;
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

            Categories = new ObservableCollection<Category>();
            Categories.Add(new Category(){ Name = "Vorbedinungen" });
            Categories.Add(new Category() { Name = "Vorarbeiten" });
            Categories.Add(new Category() { Name = "Baustelleneinrichtung" });

            DataContext = this;
        }

    }

    public class Category
    {
        public string Name { get; set; }

        public ObservableCollection<Category> Members { get; set; }

        public Category()
        {
            this.Members = new ObservableCollection<Category>();
        }
    }

    class Normtext
    {

    }
}
