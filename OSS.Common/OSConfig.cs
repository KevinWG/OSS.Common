#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局模块配置内
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.Plugs.CachePlug;
using OSS.Common.Plugs.DirConfigPlug;
using OSS.Common.Plugs.LogPlug;

namespace OSS.Common
{
    /// <summary>
    /// 基础配置模块
    /// </summary>
    public static class OsConfig
    {
        #region  Module初始化模块

        /// <summary>
        ///   日志模块提供者
        /// </summary>
        [Obsolete("请使用 OSS.Tools.Log 命名空间下 LogUtil.LogWriterProvider ")]
        public static Func<string, ILogPlug> LogWriterProvider { get; set; }

        /// <summary>
        ///   缓存模块提供者
        /// </summary>
        [Obsolete("请使用 OSS.Tools.Cache 命名空间下 CacheUtil.CacheProvider ")]
        public static Func<string, ICachePlug> CacheProvider { get; set; }

        /// <summary>
        ///   配置信息模块提供者
        /// </summary>
        [Obsolete("请使用 OSS.Tools.DirConfig 命名空间下 DirConfigUtil.DirConfigProvider ")]
        public static Func<string, IDirConfigPlug> DirConfigProvider { get; set; }

        #endregion
    }
}
