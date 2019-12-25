using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            LowerBody = new HashSet<LowerBody>();
            UpperBody = new HashSet<UpperBody>();
        }

        public long FeedbackId { get; set; }
        public byte Fit { get; set; }
        public byte Style { get; set; }
        public byte Quality { get; set; }
        public string Feedback1 { get; set; }

        public virtual ICollection<LowerBody> LowerBody { get; set; }
        public virtual ICollection<UpperBody> UpperBody { get; set; }
    }
}
