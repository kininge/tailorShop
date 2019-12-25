using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tailorShop.Models
{
    public partial class LaxumiPooja
    {
        [Key]
        public decimal Year { get; set; }
        public DateTime Date { get; set; }
    }
}
