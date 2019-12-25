using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class Security
    {
        public Security()
        {
            Users = new HashSet<Users>();
        }

        public short QuestionId { get; set; }
        public string Question { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
