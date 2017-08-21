using System;

namespace SitecoreEventLog.Website.DataAccess.Models
{
    public class Change
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}