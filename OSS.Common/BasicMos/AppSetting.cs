
namespace OSS.Common.BasicMos
{
    /// <summary>
    ///  应用设置信息
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AppSetting()
        {
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public AppSetting(string appId, string appSecret)
        {
            app_id     = appId;
            app_secret = appSecret;
        }

        /// <summary>
        /// 应用账号id
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        /// 应用账号秘钥
        /// </summary>
        public string app_secret { get; set; }

        /// <summary>
        ///  应用版本
        /// </summary>
        public string app_version { get; set; }
    }
}
