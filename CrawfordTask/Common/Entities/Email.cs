using System;
using System.Collections.Generic;

namespace CrawfordTask.Common.Entities
{
    public partial class Email
    {
        public int Id { get; set; }
        public int LegacyActivityId { get; set; }
        public string RecipientTo { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public string SentBy { get; set; }
        public DateTime? SentTime { get; set; }
        public string PartyTypeCode { get; set; }
    }
}
