using System;

namespace SitecoreEventLog.Website.DataAccess.Models
{
    public class Event
    {
        public int Id { get; set; }
        public Guid ItemId { get; set; }
        public string ItemPath { get; set; }
        public EventType EventType { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string SourceDatabase { get; set; }
        public int? ItemVersion { get; set; }
        public int? PublishDetailId { get; set; }
        public int? SaveDetailId { get; set; }

        public int? DetailId
        {
            get
            {
                if (EventType == EventType.Publish)
                {
                    return PublishDetailId;
                }
                else if (EventType == EventType.Save)
                {
                    return SaveDetailId;
                }

                return null;
            }
        }
    }
}