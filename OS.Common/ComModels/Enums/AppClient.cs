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
        [Description("PC")] PC = 0,

        /// <summary>
        /// IOS
        /// </summary>
        [Description("IOS")] IOS = 1,

        /// <summary>
        /// 安卓
        /// </summary>
        [Description("安卓")] Android = 2,

        /// <summary>
        /// WP
        /// </summary>
        [Description("WindowsPhone")] WindowsPhone = 3,
    }


    /// <summary>
    ///   应用客户端类型
    /// </summary>
    public enum WebBrowserClient
    {
        /// <summary>
        /// 非浏览器
        /// </summary>
        None = 0,

        /// <summary>
        /// 正常浏览器
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 微信浏览器
        /// </summary>
        WeiXinBrowser = 2
    }


}