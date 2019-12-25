using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class WorkOutDetails
    {
        public int? WorkId { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string BranchName { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public decimal Pay { get; set; }
        public string addedBy { get; set; }

        public virtual UserDetails CreatedByNavigation { get; set; }
        public virtual UserDetails UpdatedByNavigation { get; set; }
        public virtual Work Work { get; set; }
    }
}
