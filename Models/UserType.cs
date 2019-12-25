using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<Users>();
        }

        public short UserTypeId { get; set; }
        public string UserType1 { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
