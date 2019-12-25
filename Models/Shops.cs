using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class Shops
    {
        public Shops()
        {
            UserDetails = new HashSet<UserDetails>();
        }

        public short ShopId { get; set; }
        public string ShopName { get; set; }
        public string BranchName { get; set; }

        public virtual ICollection<UserDetails> UserDetails { get; set; }
    }
}
