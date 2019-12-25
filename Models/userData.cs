using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tailorShop.Models
{
	public class userData
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		public string EmailAddress { get; set; }
		public string MobileNo { get; set; }
		public bool? IsOnWhatsApp { get; set; }
		public DateTime Birthdate { get; set; }
		public string Address { get; set; }
		public string ShopName { get; set; }
		public string BranchName { get; set; }
		public bool? IsTestData { get; set; }
		public string UserType1 { get; set; }
	}
}
