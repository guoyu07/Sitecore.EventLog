using SitecoreEventLog.Website.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreEventLog.Website.DataAccess.Repositories
{
    public class EventRepository
    {
        public List<Event> GetEventsByItemId(Guid itemId, int maximumRows, int startRows)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("ItemId", itemId);
                db.AddParameter("MaximumRows", maximumRows);
                db.AddParameter("StartRows", startRows);

                return db.ExecuteStoredProcedureQuery<Event>("dbo.GetEventsByItemId");
            }
        }

        public Event AddEventItem(Event item)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("ItemId", item.ItemId);
                db.AddParameter("ItemPath", item.ItemPath);
                db.AddParameter("EventType", item.EventType);
                db.AddParameter("Date", item.Date);
                db.AddParameter("UserName", item.UserName);
                db.AddParameter("SourceDatabase", item.SourceDatabase);
                db.AddParameter("ItemVersion", item.ItemVersion);
                db.AddParameter("PublishDetailId", item.PublishDetailId);
                db.AddParameter("SaveDetailId", item.SaveDetailId);

                return db.ExecuteStoredProcedureQuery<Event>("dbo.AddEventItem").FirstOrDefault();
            }
        }

        public Event GetEventItemById(int id)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("Id", id);

                return db.ExecuteStoredProcedureQuery<Event>("dbo.GetEventItemById").FirstOrDefault();
            }
        }

        public int CountEventsByItemId(Guid itemId)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("ItemId", itemId);

                return (int)db.ExecuteStoredProcedureScaler("dbo.CountEventsByItemId");
            }
        }

        public int CountEvents()
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                return (int)db.ExecuteStoredProcedureScaler("dbo.CountEvents");
            }
        }

        public List<Event> GetPagedEvents(int maximumRows, int startRows)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("MaximumRows", maximumRows);
                db.AddParameter("StartRows", startRows);

                //TODO get PublishDetail inside Events

                return db.ExecuteStoredProcedureQuery<Event>("dbo.GetPagedEvents");
            }
        }
    }
}