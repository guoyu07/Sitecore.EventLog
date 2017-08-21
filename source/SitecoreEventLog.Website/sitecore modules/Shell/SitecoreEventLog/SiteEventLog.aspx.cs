using System.Collections.Generic;
using System.Web;
using SitecoreEventLog.Website.DataAccess;
using SitecoreEventLog.Website.DataAccess.Repositories;
using SitecoreEventLog.Website.DataAccess.Models;

namespace SitecoreEventLog.Website.sitecore_modules.Shell.SitecoreEventLog
{
    public partial class SiteEventLog : System.Web.UI.Page
    {
        private EventRepository EventRepository = new EventRepository();
        
        public List<Event> SiteEventLogGrid_GetData(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            var itemId = HttpUtility.UrlDecode(Request.QueryString["id"]);
            List<Event> events = EventRepository.GetPagedEvents(maximumRows, startRowIndex);
            totalRowCount = EventRepository.CountEvents();
            return events;
        }
    }
}