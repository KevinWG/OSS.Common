using System;
using System.Collections.Concurrent;
using OS.Common.Modules.AsynModule;

namespace OS.Common.Modules.LogModule
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
        /// <param name="msg"></param>
        /// <param name="msgKey"></param>
        /// <param name="moduleName"></param>
        internal LogInfo(LogLevelEnum loglevel, object msg, string msgKey = null, string moduleName = ModuleNames.Default)
        {
            Level = loglevel;
            ModuleName = moduleName;
            Msg = msg;
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
        /// 错误编号
        /// </summary>
        public string ErrorCode { get;internal set; }
    }

    /// <summary>
    /// 日志写模块
    /// </summary>
    public static class LogUtil
    {
  
        /// <summary>
        /// 记录日志操作的异步模块
        /// </summary>
        internal static string LogAsynModuleName
        {
            get; 
            set;
        }

        private static readonly ConcurrentDictionary<string, ILogWriter> _logDirs =
            new ConcurrentDictionary<string, ILogWriter>();
        
        /// <summary>
        /// 通过模块名称获取日志模块实例
        /// </summary>
        /// <param name="logModule"></param>
        /// <returns></returns>
        public static ILogWriter GetLogWrite(string logModule)
        {
            if (string.IsNullOrEmpty(logModule))
                logModule = ModuleNames.Default;

            if (_logDirs.ContainsKey(logModule))
                return _logDirs[logModule];

            var log = OsConfig.Provider.GetLogWrite(logModule) ?? new LogWriter();
            _logDirs.TryAdd(logModule, log);

            return log;
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
            info.ErrorCode = GetErrorCode();
            if (string.IsNullOrEmpty(info.ModuleName))
                info.ModuleName = ModuleNames.Default;

            var logWrite = GetLogWrite(info.ModuleName);
            AsynUtil.Asyn(logWrite.WriteLog, info, LogAsynModuleName); // logActionList.Post(info);
            return info.ErrorCode;
        }

        /// <summary>
        /// 生成错误编号
        /// </summary>
        /// <returns></returns>
        private static string GetErrorCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
