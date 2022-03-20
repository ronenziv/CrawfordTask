using System;
using System.Collections.Generic;

namespace CrawfordTask.Common.Entities
{
    public partial class FileActivityLinks
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int? ActivityId { get; set; }
        public string LegacyActivityId { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedId { get; set; }
    }
}
