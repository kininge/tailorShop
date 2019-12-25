using System;
using System.Collections.Generic;

namespace tailorShop.Models
{
    public partial class Users
    {
        public int LogInid { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public short UserTypeId { get; set; }
        public short QuestionId { get; set; }
        public string Answer { get; set; }

        public virtual Security Question { get; set; }
        public virtual UserDetails User { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
