#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：OSSCommon - 通用应用账号配置信息
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       创建时间： 2017-5-25
*       
*****************************************************************************/

#endregion

namespace OSS.Common.ComModels
{
    /// <summary>
    /// 通用应用账号配置信息  
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AppConfig()
        {
        }
        
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public AppConfig(string appId, string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
        }

        /// <summary>
        /// 应用账号AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 应用账号AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 操作管理类型
        /// </summary>
        public AppOperateMode OperateMode { get; set; } = AppOperateMode.BySelf;

        /// <summary>
        ///  当 OperateMode = AppOperateMode.ByAgent 时
        ///  代理应用的账号AppId
        /// </summary>
        public string ByAppId { get; set; }
    }

    /// <summary>
    /// 应用操作类型
    /// </summary>
    public enum AppOperateMode
    {
        /// <summary>
        ///  自管理
        /// </summary>
        BySelf,
        /// <summary>
        /// 代理操作
        /// </summary>
        ByAgent
    }
}
