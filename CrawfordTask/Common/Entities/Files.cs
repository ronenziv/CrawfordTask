using System;
using System.Collections.Generic;

namespace CrawfordTask.Common.Entities
{
    public partial class Files
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string FileName { get; set; }
        public string BlobName { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedId { get; set; }
    }
}
