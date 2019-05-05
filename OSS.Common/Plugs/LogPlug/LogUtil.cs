#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局插件 -  日志插件实体及辅助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;

namespace OSS.Common.Plugs.LogPlug
{

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevelEnum
    {
        /// <summary>
        /// 跟踪查看
        /// </summary>
        Trace,

        /// <summary>
        /// 信息
        /// </summary>
        Info,

        /// <summary>
        /// 错误
        /// </summary>
        Error,

        /// <summary>
        /// 警告
        /// </summary>
        Warning,
    }

    /// <summary>
    /// 日志实体
    /// </summary>
    public sealed class LogInfo
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public LogInfo()
        {
        }

        /// <summary>
        /// 日志构造函数
        /// </summary>
        /// <param name="loglevel"></param>
        /// <param name="logMsg"></param>
        /// <param name="msgKey"></param>
        /// <param name="moduleName"></param>
        internal LogInfo(LogLevelEnum loglevel, object logMsg, string msgKey = null, string moduleName = ModuleNames.Default)
        {
            Level = loglevel;
            ModuleName = moduleName;
            this.Msg = logMsg;
            MsgKey = msgKey;
        }

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevelEnum Level { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///   key值  可以是自定义的标识  
        ///   根据此字段可以处理当前module下不同复杂日志信息
        /// </summary>
        public string MsgKey { get; set; }

        /// <summary>
        /// 日志信息  可以是复杂类型  如 具体实体类
        /// </summary>
        public object Msg { get; set; }

        /// <summary>
        /// 编号（全局唯一）
        /// </summary>
        public string LogCode { get; set; }
    }

    /// <summary>
    /// 日志写模块
    /// </summary>
    public static class LogUtil
    {
        private static readonly DefaultLogPlug defaultCache = new DefaultLogPlug();
        /// <summary>
        /// 通过模块名称获取日志模块实例
        /// </summary>
        /// <param name="logModule"></param>
        /// <returns></returns>
        public static ILogPlug GetLogWrite(string logModule)
        {
            if (string.IsNullOrEmpty(logModule))
                logModule = ModuleNames.Default;

            return OsConfig.LogWriterProvider?.Invoke(logModule) ?? defaultCache;
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="msgKey">  关键值  </param>
        /// <param name="moduleName"> 模块名称 </param>
        public static string Info(object msg, string msgKey = null, string moduleName = ModuleNames.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Info, msg, msgKey, moduleName));
        }

        /// <summary>
        /// 记录警告，用于未处理异常的捕获
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="msgKey">  关键值  </param>
        /// <param name="moduleName">模块名称</param>
        public static string Warning(object msg, string msgKey = null, string moduleName = ModuleNames.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Warning, msg, msgKey, moduleName));
        }

        /// <summary>
        /// 记录错误，用于捕获到的异常信息记录
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="msgKey">  关键值  </param>
        /// <param name="moduleName">模块名称</param>
        public static string Error(object msg, string msgKey = null, string moduleName = ModuleNames.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Error, msg, msgKey, moduleName));
        }

        /// <summary>
        /// 记录错误，用于捕获到的异常信息记录
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="msgKey">  关键值  </param>
        /// <param name="moduleName">模块名称</param>
        public static string Trace(object msg, string msgKey = null, string moduleName = ModuleNames.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Trace, msg, msgKey, moduleName));
        }


        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="info"></param>
        private static string Log(LogInfo info)
        {
            if (string.IsNullOrEmpty(info.ModuleName))
                info.ModuleName = ModuleNames.Default;

            var logWrite = GetLogWrite(info.ModuleName);

            logWrite.SetLogCode(info);
            logWrite.WriteLog(info);

            return info.LogCode;
        }

    }
}
