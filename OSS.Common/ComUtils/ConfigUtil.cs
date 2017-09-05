using System;
#if NETFW
using System.Collections.Specialized;
using System.Configuration;

namespace OSS.Common.ComUtils
{
    /// <summary>
    /// 配置信息获取
    /// </summary>
    public static class ConfigUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public static NameValueCollection AppSettings { get; }

        static ConfigUtil()
        {
            AppSettings = ConfigurationManager.AppSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetAppSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        ///  获取连接串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        ///   获取提供者名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnenctProvider(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            return ConfigurationManager.ConnectionStrings[key].ProviderName;
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
#endif
