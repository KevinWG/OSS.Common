
namespace OSS.Common.BasicMos
{
    /// <summary>
    ///  应用设置信息接口
    /// </summary>
    public interface IAppSecret
    {
        /// <summary>
        /// 应用账号id
        /// </summary>
        public string app_id { get;  }

        /// <summary>
        /// 应用账号秘钥
        /// </summary>
        public string app_secret { get;  }
    }


    /// <summary>
    ///  应用设置信息
    /// </summary>
    public class AppSecret: IAppSecret
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AppSecret()
        {
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public AppSecret(string appId, string appSecret)
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
        
    }
}
