using SitecoreEventLog.Website.DataAccess.Models;
using System.Linq;

namespace SitecoreEventLog.Website.DataAccess.Repositories
{
    public class SaveDetailRepository
    {
        public SaveDetail AddSaveDetailItem(SaveDetail item)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("Language", item.Language);
                db.AddParameter("Changes", item.Changes);

                return db.ExecuteStoredProcedureQuery<SaveDetail>("dbo.AddSaveDetailItem").FirstOrDefault();
            }
        }

        public SaveDetail GetSaveDetailItemById(int id)
        {
            using (var db = new SitecoreEventLogDatabaseCommand())
            {
                db.AddParameter("Id", id);

                return db.ExecuteStoredProcedureQuery<SaveDetail>("dbo.GetSaveDetailItemById").FirstOrDefault();
            }
        }
    }
}