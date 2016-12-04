using netoaster;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Normtexte.Models;
using Normtexte.ViewModels;

namespace Normtexte
{
    public partial class MainWindow : Window, ITreeView
    {     
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            (DataContext as MainWindowViewModel).View = this as ITreeView;
        }

        private void OptionTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ICommand command = (DataContext as MainWindowViewModel).CopyCommand;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        private void OptionTree_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            ICommand command = (DataContext as MainWindowViewModel).CopyCommand;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        public Option SelectedOption()
        {
            var selected = OptionTree.SelectedItem;
            return (selected is Option) ? selected as Option : null;
        }
    }

}
