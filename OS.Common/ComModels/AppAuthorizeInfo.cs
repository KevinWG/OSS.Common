using OS.Common.ComModels.Enums;

namespace OS.Common.ComModels
{
    /// <summary>
    /// 用户验证model
    /// </summary>
    public class AppAuthorizeInfo
    {
        #region   用户信息
        /// <summary>
        /// id
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        ///   用户名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        ///  是否是员工
        ///   预留以后员工端接口  使用
        /// </summary>
        public bool IsEmployee { get; set; }
        #endregion

        /// <summary>
        ///   应用版本
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        ///   应用来源
        /// </summary>
        public AppSource AppSource { get; set; }

        /// <summary>
        /// 应用客户端类型
        /// </summary>
        public AppClient AppClient { get; set; }

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
