using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using SitecoreEventLog.Website.DataAccess.Models;
using SitecoreEventLog.Website.DataAccess.Repositories;
using System;
using System.Linq;

namespace SitecoreEventLog.Website.EventHandlers
{
    public class DeleteEventHandler
    {
        private readonly EventRepository _eventRepository;

        public DeleteEventHandler()
        {
            _eventRepository = new EventRepository();
        }

        public void OnItemDeleted(object sender, EventArgs args)
        {
            bool isItemDeletedArgs = args is SitecoreEventArgs;
            if (!isItemDeletedArgs)
                return;
            try
            {
                var deleteEventArgs = args as SitecoreEventArgs;
                var item = (Item)deleteEventArgs.Parameters.First();
                ID parentId = null;
                Item parentItem = null;
                if (deleteEventArgs.Parameters.Count() > 1)
                {
                    parentId = (ID)deleteEventArgs.Parameters[1];
                    parentItem = Database.GetDatabase("master").GetItem(parentId);                    
                }                

                var @event = new DataAccess.Models.Event()
                {
                    ItemId = item.ID.ToGuid(),
                    ItemPath = (parentItem != null) ? string.Format("{0}{1}", parentItem.Paths.FullPath, item.Paths.Path) : item.Paths.Path,
                    ItemVersion = item.Version?.Number,
                    SourceDatabase = item.Database.Name,
                    EventType = EventType.Delete,
                    Date = DateTime.Now.ToLocalTime(),
                    UserName = Sitecore.Context.User?.Name
                };

                _eventRepository.AddEventItem(@event);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("EventLog DeleteEvenhandler threw an exception", ex);
            }
        }
    }
}