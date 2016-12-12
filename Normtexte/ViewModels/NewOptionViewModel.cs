using NormtexteUI.Models;

namespace NormtexteUI.ViewModels
{
    public class NewOptionViewModel
    {
        public Option Option { get; set; }
        public NewOptionViewModel(Option option)
        {
            Option = option;
        }
    }
}
