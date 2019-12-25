using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class UpperBody
    {
        public int WorkId { get; set; }
        public byte ClothId { get; set; }
        public string ClothName { get; set; }
        public decimal? Height { get; set; }
        public decimal? Front { get; set; }
        public decimal? Collar { get; set; }
        public decimal? Shoulder { get; set; }
        public decimal? Sleeve { get; set; }
        public decimal? SleeveWidth { get; set; }
        public decimal? Cuff { get; set; }
        public string Note { get; set; }
        public byte? Status { get; set; }
        public decimal Price { get; set; }
        public int? AssignTo { get; set; }
        public decimal? PaidTo { get; set; }
        public long? FeedbackId { get; set; }

        public virtual UserDetails AssignToNavigation { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Work Work { get; set; }
    }
}
