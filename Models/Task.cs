using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class TodayTask
    {
        public string ShopName { get; set; }
        public DateTime? Today { get; set; }
        public int? Days { get; set; }
        public Boolean isTestData { get; set; }
        public int? AssignTo { get; set; }
    }
}
