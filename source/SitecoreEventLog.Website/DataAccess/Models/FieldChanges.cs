using System.Collections.Generic;

namespace SitecoreEventLog.Website.DataAccess.Models
{
    public class FieldChanges
    {
        public FieldChanges()
        {
            Changes = new List<Change>();
        }

        public List<Change> Changes { get; set; }
    }
}