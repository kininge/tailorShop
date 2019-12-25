using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class Register
	{
        public Register()
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

        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public bool? IsOnWhatsApp { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public short ShopId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsTestData { get; set; }
        public bool? IsDelete { get; set; }

		public int? UserId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public short UserTypeId { get; set; }
		public short QuestionId { get; set; }
		public string Answer { get; set; }

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
    }
}
