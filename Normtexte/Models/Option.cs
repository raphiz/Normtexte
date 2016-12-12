using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NormtexteUI.Models
{
    public class Option : INotifyPropertyChanged
    {
        private string _shortText;
        public string ShortText
        {
            get { return _shortText; }
            set
            {
                _shortText = value;
                OnPropertyChanged(nameof(ShortText));
            }
        }

        private string _longText;
        public string LongText
        {
            get { return _longText; }
            set
            {
                _longText = value;
                OnPropertyChanged(nameof(LongText));
            }
        }
        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }
        public ObservableCollection<Price> Prices { get; set; }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(params string[] propertyName)
        {
            foreach (string property in propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion 
    }

}
