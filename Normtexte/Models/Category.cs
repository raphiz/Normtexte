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
            public string Name { get; set; }

            public ObservableCollection<Category> Categories { get; set; }
            public ObservableCollection<Option> Options { get; set; }

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
        // TODO: Boilerplate code for update stuff
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
