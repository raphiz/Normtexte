using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Normtexte.Models
{
    // TODO: INotifyPropertyChanged stuff
    public class Option
    {
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public string Unit { get; set; }
        public ObservableCollection<Price> Prices { get; set; }
    }

}
