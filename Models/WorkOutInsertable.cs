using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class WorkOutInsertable
    {
        public int? WorkId { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public int? CreatedBy { get; set; }
        public decimal Pay { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsTestData { get; set; }

        public virtual UserDetails CreatedByNavigation { get; set; }
        public virtual UserDetails UpdatedByNavigation { get; set; }
        public virtual Work Work { get; set; }
    }
}
