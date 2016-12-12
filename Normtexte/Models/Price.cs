using System.ComponentModel;

namespace NormtexteUI.Models
{
    public class Price : INotifyPropertyChanged
    {
        private double _from;
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }
        private double _to;
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
        private double _pricePerUnit;
        public double PricePerUnit
        {
            get { return _pricePerUnit; }
            set
            {
                _pricePerUnit = value;
                OnPropertyChanged(nameof(PricePerUnit));
            }
        }


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
