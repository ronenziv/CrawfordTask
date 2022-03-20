using System;
using System.Collections.Generic;

namespace CrawfordTask.Common.Entities
{
    public partial class LossTypes
    {
        public LossTypes()
        {
            Claims = new HashSet<Claims>();
        }

        public int LossTypeId { get; set; }
        public string LossTypeCode { get; set; }
        public string LossTypeDescription { get; set; }
        public bool? Active { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedId { get; set; }

        public virtual ICollection<Claims> Claims { get; set; }
    }
}
