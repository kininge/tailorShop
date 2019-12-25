using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class LowerTask
    {
        public int WorkId { get; set; }
        public string Name { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string ShopName { get; set; }
        public string BranchName { get; set; }
        public byte ClothId { get; set; }
        public string ClothName { get; set; }
        public decimal? Height { get; set; }
        public decimal? Knee { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Seat { get; set; }
        public decimal? Thigh { get; set; }
        public decimal? Bottom { get; set; }
        public decimal? Underline { get; set; }
        public string Note { get; set; }
        public double Status { get; set; }
        public decimal Price { get; set; }
        public string AssignTo { get; set; }
        public decimal? PaidTo { get; set; }
        public long? FeedbackId { get; set; }

        public virtual UserDetails AssignToNavigation { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Work Work { get; set; }
    }
}
