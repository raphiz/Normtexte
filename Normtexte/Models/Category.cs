using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Normtexte.Models
{
    public class Category : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; OnPropertyChanged(nameof(Name));
            }
        }

        ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                // TODO: Does this work?!
                _categories.CollectionChanged += (s, e) => OnPropertyChanged(nameof(Children));
                OnPropertyChanged(nameof(Categories), nameof(Children));
            }
        }

        ObservableCollection<Option> _options;
        public ObservableCollection<Option> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                // TODO: Does this work?!
                _options.CollectionChanged += (s, e) => OnPropertyChanged(nameof(Children));
                OnPropertyChanged(nameof(Categories), nameof(Children));
            }
        }

        public List<Object> Children
        {
            get
            {
                List<Object> result = new List<Object>();
                if (Categories != null) result.AddRange(Categories);
                if (Options != null) result.AddRange(Options);
                return result;
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(params string[] propertyName)
        {
            foreach(string property in propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion 
    }
}
