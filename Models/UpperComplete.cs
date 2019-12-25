using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class UpperComplete
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
        public double Status { get; set; }
        public decimal Price { get; set; }
        public string AssignTo { get; set; }
        public decimal? PaidTo { get; set; }
        public long FeedbackId { get; set; }
        public byte Fit { get; set; }
        public byte Style { get; set; }
        public byte Quality { get; set; }
        public string Feedback1 { get; set; }

        public virtual UserDetails AssignToNavigation { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Work Work { get; set; }
    }
}
