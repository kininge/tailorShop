using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class Work
    {
        public Work()
        {
            LowerBody = new HashSet<LowerBody>();
            UpperBody = new HashSet<UpperBody>();
            WorkOut = new HashSet<WorkOut>();
        }

        public int WorkId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public decimal? Advance { get; set; }

        public virtual UserDetails CreatedByNavigation { get; set; }
        public virtual UserDetails UpdatedByNavigation { get; set; }
        public virtual UserDetails User { get; set; }
        public virtual ICollection<LowerBody> LowerBody { get; set; }
        public virtual ICollection<UpperBody> UpperBody { get; set; }
        public virtual ICollection<WorkOut> WorkOut { get; set; }
    }
}
