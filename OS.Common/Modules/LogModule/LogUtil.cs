
using System;
using System.Collections.Generic;
using OS.Common.Modules;
using OS.Common.Modules.AsynModule;

namespace OS.Common.LogModule
{

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

    public class LogInfo
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
        /// <param name="key"></param>
        /// <param name="moduleName"></param>
        internal LogInfo(LogLevelEnum loglevel,string msg,object key=null,string moduleName=ModuleLogKeys.Default)
        {
            Level = loglevel;
            ModuleName = moduleName;
            Message = msg;
            Key = key;
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
        ///   key值  可以是id等
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误编号
        /// </summary>
        public string ErrorCode { get; internal set; }
    }

    /// <summary>
    /// 日志写模块
    /// </summary>
    public static class LogUtil
    {
        ///// <summary>
        /////   异步缓冲池
        ///// </summary>
        //private static ActionBlock<LogInfo> logActionList=new ActionBlock<LogInfo>(info =>
        //{
        //    var act = CreateLogWriter(info.Category);
        //    act.WriteLog(new LogInfo(info.Level,info.Category,info.Message,info.Key));
        //},new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 3 } );

        /// <summary>
        /// 记录日志操作的异步模块
        /// </summary>
        internal static string LogAsynModuleName
        {
            get; 
            set;
        }
    

        internal static Dictionary<string, ILogWriter> logModules=
            new Dictionary<string, ILogWriter>();


        /// <summary>
        /// 通过模块名称获取日志模块实例
        /// </summary>
        /// <param name="logModule"></param>
        /// <returns></returns>
        public static ILogWriter GetLogWrite(string logModule)
        {
            if (!string.IsNullOrEmpty(logModule)&&logModules.ContainsKey(logModule))
            {
                return logModules[logModule];
            }
            return logModules[ModuleCacheKeys.Default];
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        /// <param name="moduleName"> 模块名称 </param>
        public static string Info(string msg, object key=null, string moduleName=ModuleCacheKeys.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Info, msg, key, moduleName));
        }

        /// <summary>
        /// 记录警告，用于未处理异常的捕获
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        /// <param name="moduleName">模块名称</param>
        public static string Warning(string msg, object key = null, string moduleName=ModuleCacheKeys.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Warning, msg, key, moduleName));
        }

        /// <summary>
        /// 记录错误，用于捕获到的异常信息记录
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        /// <param name="moduleName">模块名称</param>
        public static string Error(string msg, object key = null, string moduleName=ModuleCacheKeys.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Error, msg, key, moduleName));
        }

        /// <summary>
        /// 记录错误，用于捕获到的异常信息记录
        /// </summary>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        /// <param name="moduleName">模块名称</param>
        public static string Trace(string msg, object key = null, string moduleName=ModuleCacheKeys.Default)
        {
            return Log(new LogInfo(LogLevelEnum.Trace, msg, key, moduleName));
        }


        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="info"></param>
        public static string Log(LogInfo info)
        {
            info.ErrorCode = GetErrorCode();
            var logWrite = GetLogWrite(info.ModuleName);
         
            AsynUtil.Asyn(logWrite.WriteLog, info, LogAsynModuleName);  // logActionList.Post(info);
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
