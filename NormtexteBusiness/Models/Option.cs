using System.ComponentModel.DataAnnotations;

namespace Normtexte.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        public string StrongText { get; set; }
        public string LongText { get; set; }
        public string Unit { get; set; }
    }
}