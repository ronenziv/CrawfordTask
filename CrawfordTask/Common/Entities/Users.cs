using System;
using System.Collections.Generic;

namespace CrawfordTask.Common.Entities
{
    public partial class Users
    {
        public Users()
        {
            ClaimsCreated = new HashSet<Claims>();
            ClaimsLastUpdated = new HashSet<Claims>();
            ClaimsLossAdjuster = new HashSet<Claims>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Claims> ClaimsCreated { get; set; }
        public virtual ICollection<Claims> ClaimsLastUpdated { get; set; }
        public virtual ICollection<Claims> ClaimsLossAdjuster { get; set; }
    }
}
