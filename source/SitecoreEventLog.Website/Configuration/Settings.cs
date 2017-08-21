namespace SitecoreEventLog.Website.Configuration
{
    public static class Settings
    {
        private static bool DEFAULT_TRACECHANGES = false;
        private static int DEFAULT_MAXLENGTH = 1000;

        private static string _connectionString;
        private static bool? _traceChanges;
        private static int? _traceValueMaxLength;
        private static bool? _traceOldValue;
        private static bool? _traceNewValue;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = Sitecore.Configuration.Settings.GetSetting("SitecoreEventLog.ConnectionString");
                }
                return _connectionString;
            }
        }

        public static bool TraceChanges
        {
            get
            {
                if (!_traceChanges.HasValue)
                {
                    _traceChanges = Sitecore.Configuration.Settings.GetBoolSetting("SitecoreEventLog.TraceChanges", DEFAULT_TRACECHANGES);
                }
                return _traceChanges.Value;
            }
        }

        public static int TraceValueMaxLength
        {
            get
            {
                if (!_traceValueMaxLength.HasValue)
                {
                    _traceValueMaxLength = Sitecore.Configuration.Settings.GetIntSetting("SitecoreEventLog.TraceValueMaxLength", DEFAULT_MAXLENGTH);
                }
                return _traceValueMaxLength.Value;
            }
        }

        public static bool TraceOldvalue
        {
            get
            {
                if (!_traceOldValue.HasValue)
                {
                    _traceOldValue = Sitecore.Configuration.Settings.GetBoolSetting("SitecoreEventLog.TraceOldvalue", true);
                }
                return _traceOldValue.Value;
            }
        }

        public static bool TraceNewValue
        {
            get
            {
                if (!_traceNewValue.HasValue)
                {
                    _traceNewValue = Sitecore.Configuration.Settings.GetBoolSetting("SitecoreEventLog.TraceNewValue", true);
                }
                return _traceNewValue.Value;
            }
        }
   }
}