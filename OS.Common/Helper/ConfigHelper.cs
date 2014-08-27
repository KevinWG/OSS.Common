using System;
using System.Collections.Specialized;
using System.Configuration;

namespace OS.Common.Helper
{
    public static class ConfigHelper
    {
        public static NameValueCollection AppSettings { get; private set; }

        static ConfigHelper()
        {
            AppSettings = ConfigurationManager.AppSettings;
        }

        public static string GetAppSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetConnectionString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static StringDictionary GetConnectionStrings()
        {
            StringDictionary reval = new StringDictionary();
            ConnectionStringSettingsCollection StrConnColl = ConfigurationManager.ConnectionStrings;
            for (int i = 0; i < StrConnColl.Count; i++)
            {
                reval.Add(StrConnColl[i].Name, StrConnColl[i].ConnectionString);
            }
            return reval;

        }
    }
}
