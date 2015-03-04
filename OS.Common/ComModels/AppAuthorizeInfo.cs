namespace OS.Common.ComModels
{
    /// <summary>
    /// 用户验证model
    /// </summary>
    public class AppAuthorizeInfo
    {
        /// <summary>
        ///   应用版本
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        ///   应用来源
        /// </summary>
        public int AppSource { get; set; }

        /// <summary>
        /// 应用客户端类型
        /// </summary>
        public int AppClient { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceID { get; set; }


        /// <summary>
        ///  Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///  sign标识
        /// </summary>
        public string Sign { get; set; }

    }

}
