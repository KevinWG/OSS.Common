using System;

namespace OSS.Common.BasicMos
{
    /// <summary>
    ///  应用信息id接口
    /// </summary>
    [Obsolete("转移命名空间至（OSS.Common）")] 
    public interface IAppId
    {
        /// <summary>
        /// 应用账号id
        /// </summary>
        public string app_id { get; }
    }

    /// <summary>
    ///  应用设置信息接口
    /// </summary>
    [Obsolete("转移命名空间至（OSS.Common）")]
    public interface IAppSecret: IAppId
    {
        /// <summary>
        /// 应用账号秘钥
        /// </summary>
        public string app_secret { get;  }
    }

    /// <summary>
    ///  应用设置信息
    /// </summary>
    [Obsolete("转移命名空间至（OSS.Common）")]
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

        /// <inheritdoc />
        public string app_id { get; set; }

        /// <inheritdoc />
        public string app_secret { get; set; }
        
    }
}
