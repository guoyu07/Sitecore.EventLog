using Newtonsoft.Json;
using System.Web;

namespace SitecoreEventLog.Website.DataAccess.Models
{
    public class SaveDetail
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Changes { get; set; }

        public string FormattedChanges
        {
            get
            {
                if (Changes.StartsWith("{"))
                {
                    var o = JsonConvert.DeserializeObject<FieldChanges>(Changes);
                    return string.Format("<pre>{0}</pre>", HttpUtility.HtmlEncode(JsonConvert.SerializeObject(o, Formatting.Indented)));
                }

                return Changes;
            }
        }
    }
}