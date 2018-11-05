using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeShop2.Models
{
    public class CoffeItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"\d{1,}\,?\d{1,}", ErrorMessage = "Use the comma character as the separator.")]
        public double Price { get; set; }

        [Required]
        public string Name { get; set; }

        [RegularExpression(@"\d{1,}\,?\d{1,}", ErrorMessage = "Use the comma character as the separator.")]
        public double Volume { get; set; } = 0.00;

        public bool IsEnable { get; set; } = true;

    }
}