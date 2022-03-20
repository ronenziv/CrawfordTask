using System;
using System.Collections.Generic;

namespace CrawfordTask.Common.Entities
{
    public partial class LegacyActivityTypes
    {
        public string Code { get; set; }
        public string CategoryCode { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedId { get; set; }
    }
}
