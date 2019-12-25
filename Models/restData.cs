using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class RestData
    {
        public int workId { get; set; }
        public string name { get; set; }
        public string shop { get; set; }
        public string branch { get; set; }
        public DateTime? deliveryDate { get; set; }
    }
}
