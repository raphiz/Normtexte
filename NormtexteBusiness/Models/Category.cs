using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Normtexte.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Names { set; get; }
        public List<Category> Categories { set; get; }
        public List<Option> Options { set; get; }
    }
}