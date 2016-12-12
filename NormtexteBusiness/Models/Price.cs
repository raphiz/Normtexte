using System.ComponentModel.DataAnnotations;

namespace Normtexte.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }

        public double From { get; set; }
        public double To { get; set; }
        public double PricePerUnit { get; set; }
    }
}