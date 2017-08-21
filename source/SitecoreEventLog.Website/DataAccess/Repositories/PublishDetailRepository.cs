using SitecoreEventLog.Website.DataAccess.Models;
using System.Linq;

namespace SitecoreEventLog.Website.DataAccess.Repositories
{
    public class PublishDetailRepository
    {
        public PublishDetail AddPublishDetailItem(PublishDetail item)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("Cultures", item.Cultures);
                db.AddParameter("TargetDatabases", item.TargetDatabases);
                db.AddParameter("IsSitePublish", item.IsSitePublish);
                db.AddParameter("WithSubItems", item.WithSubItems);
                db.AddParameter("WithRelatedItems", item.WithRelatedItems);
                db.AddParameter("RootItemId", item.RootItemId);
                db.AddParameter("RootItemPath", item.RootItemPath);
                db.AddParameter("PublishType", item.PublishType);

                return db.ExecuteStoredProcedureQuery<PublishDetail>("dbo.AddPublishDetailItem").FirstOrDefault();
            }
        }

        public PublishDetail GetPublishDetailItemById(int id)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("Id", id);

                return db.ExecuteStoredProcedureQuery<PublishDetail>("dbo.GetPublishDetailItemById").FirstOrDefault();
            }
        }
    }
}