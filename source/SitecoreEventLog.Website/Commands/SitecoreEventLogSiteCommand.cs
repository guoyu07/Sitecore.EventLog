using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace SitecoreEventLog.Website.Commands
{
    [System.Serializable]
    public class SitecoreEventLogSiteCommand : Command
    {
        private const string IdParameterKey = "id";
        private const string LanguageParameterKey = "language";
        private const string VersionParameterKey = "version";

        public override void Execute([NotNull] CommandContext context)
        {
            Error.AssertObject(context, "context");
            if (context.Items.Length == 1)
            {
                Item item = context.Items[0];
                var parameters = new NameValueCollection();
                parameters[IdParameterKey] = item.ID.ToString();
                parameters[LanguageParameterKey] = item.Language.ToString();
                parameters[VersionParameterKey] = item.Version.ToString();
                Context.ClientPage.Start(this, "Run", parameters);
            }
        }

        protected void Run(ClientPipelineArgs args)
        {
            string str = args.Parameters[IdParameterKey];
            string name = args.Parameters[LanguageParameterKey];
            string str3 = args.Parameters[VersionParameterKey];
            var version = Sitecore.Data.Version.Parse(str3);

            Item item = Context.ContentDatabase.GetItem(str, Sitecore.Globalization.Language.Parse(name), version);
            Error.AssertItemFound(item);

            if (!SheerResponse.CheckModified()) return;

            if (!args.IsPostBack)
            {
                string forSitePageURL = "/Sitecore Modules/Shell/SitecoreEventLog/SiteEventLog.aspx";

                if (!string.IsNullOrWhiteSpace(forSitePageURL))
                {
                    Sitecore.Text.UrlString url = new Sitecore.Text.UrlString(
                        forSitePageURL);
                    url.Append(IdParameterKey, HttpUtility.UrlEncode(args.Parameters[IdParameterKey]));
                    url.Append("la", HttpUtility.UrlEncode(args.Parameters[LanguageParameterKey]));
                    url.Append("ve", HttpUtility.UrlEncode(args.Parameters[VersionParameterKey]));

                    Context.ClientPage.ClientResponse.ShowModalDialog(url.ToString(),
                        "990px", "570px",
                        "ForSitePageURL", true);
                    args.WaitForPostBack();
                }
                else
                {
                    SheerResponse.Alert("SiteEventLog.aspx url is wrong, please check the source code.",
                        true);
                }
            }
        }

        public override CommandState QueryState(CommandContext context)
        {
            Error.AssertObject(context, "context");

            if (!context.Items.Any())
            {
                return CommandState.Disabled;
            }

            return base.QueryState(context);
        }
    }
}