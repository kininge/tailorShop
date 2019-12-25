using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class StatusChnage
    {
        public int WorkId { get; set; }
        public byte ClothId { get; set; }
        public int? AssignTo { get; set; }
        public byte? Status { get; set; }
        public decimal? PaidTo { get; set; }
        public Boolean ClothType { get; set; }
    }
}
