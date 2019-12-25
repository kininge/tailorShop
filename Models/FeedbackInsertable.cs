using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class FeedbackInsertable
    {
        public FeedbackInsertable()
        {
            LowerBody = new HashSet<LowerBody>();
            UpperBody = new HashSet<UpperBody>();
        }

        public byte Fit { get; set; }
        public byte Style { get; set; }
        public byte Quality { get; set; }
        public string Feedback1 { get; set; }
        public int WorkId { get; set; }
        public byte ClothId { get; set; }
        public bool ClothType { get; set; }

        public virtual ICollection<LowerBody> LowerBody { get; set; }
        public virtual ICollection<UpperBody> UpperBody { get; set; }
    }
}
