using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class Customer
    {
        public Customer()
        {
            InverseCreatedByNavigation = new HashSet<UserDetails>();
            InverseUpdatedByNavigation = new HashSet<UserDetails>();
            LowerBody = new HashSet<LowerBody>();
            UpperBody = new HashSet<UpperBody>();
            Users = new HashSet<Users>();
            WorkCreatedByNavigation = new HashSet<Work>();
            WorkOutCreatedByNavigation = new HashSet<WorkOut>();
            WorkOutUpdatedByNavigation = new HashSet<WorkOut>();
            WorkUpdatedByNavigation = new HashSet<Work>();
            WorkUser = new HashSet<Work>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public bool? IsOnWhatsApp { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string ShopName { get; set; }
        public string BranchName { get; set; }

        public virtual UserDetails CreatedByNavigation { get; set; }
        public virtual Shops Shop { get; set; }
        public virtual UserDetails UpdatedByNavigation { get; set; }
        public virtual ICollection<UserDetails> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<UserDetails> InverseUpdatedByNavigation { get; set; }
        public virtual ICollection<LowerBody> LowerBody { get; set; }
        public virtual ICollection<UpperBody> UpperBody { get; set; }
        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<Work> WorkCreatedByNavigation { get; set; }
        public virtual ICollection<WorkOut> WorkOutCreatedByNavigation { get; set; }
        public virtual ICollection<WorkOut> WorkOutUpdatedByNavigation { get; set; }
        public virtual ICollection<Work> WorkUpdatedByNavigation { get; set; }
        public virtual ICollection<Work> WorkUser { get; set; }

        /*
        public static implicit operator Customer(Customer v)
        {
            throw new NotImplementedException();
        }
        */
    }
}
