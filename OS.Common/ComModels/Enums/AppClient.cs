using System.ComponentModel;

namespace OS.Common.ComModels.Enums
{

    /// <summary>
    ///   应用客户端类型
    /// </summary>
    public enum AppClient
    {
        /// <summary>
        /// 网页端
        /// </summary>
        [Description("Web")] Web = 0,

        /// <summary>
        /// IOS
        /// </summary>
        [Description("IOS")] IOS = 1,

        /// <summary>
        /// 安卓
        /// </summary>
        [Description("安卓")] Android = 2,

        /// <summary>
        /// 微信等公众账号浏览器端
        /// </summary>
        [Description("公众账号")] PublicAccount = 3
    }
}
