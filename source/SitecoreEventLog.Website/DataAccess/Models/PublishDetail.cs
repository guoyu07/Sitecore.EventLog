using System;

namespace SitecoreEventLog.Website.DataAccess.Models
{
    public class PublishDetail
    {
        public int Id { get; set; }
        public string Cultures { get; set; }
        public string TargetDatabases { get; set; }
        public bool IsSitePublish { get; set; }
        public bool WithSubItems { get; set; }
        public bool WithRelatedItems { get; set; }
        public Guid? RootItemId { get; set; }
        public string RootItemPath { get; set; }
        public string PublishType { get; set; }
    }
}