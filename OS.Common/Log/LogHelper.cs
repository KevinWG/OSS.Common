

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks.Dataflow;
using OS.Common.Models.Enums;

namespace OS.Common.Log
{
    using System;

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
        /// <param name="category"></param>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        internal LogInfo(LogLevelEnum loglevel,string category,string msg,object key=null)
        {
            Level = loglevel;
            Category = category;
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
        public string Category { get; set; }
        /// <summary>
        ///   key值  可以是id等
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 日志写模块
    /// </summary>
    public static class LogHelper 
    {

        #region 异步模块定义

        private static Dictionary<string,ILogWriter> logCateActions=
            new Dictionary<string, ILogWriter>();

        private static ILogWriter defaultWriter = null;

        /// <summary>
        ///   构造函数
        /// </summary>
        static LogHelper()
        {
            defaultWriter = new Log.LogWriter();
        }

        /// <summary>
        /// 返回操作日志方法
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private static ILogWriter CreateLogWriter(string category)
        {
            if (string.IsNullOrEmpty(category))
                throw new NoNullAllowedException("注册日志 category 不能不空！");
            if(logCateActions.ContainsKey(category))
                throw new ArgumentException(string.Format("日志模块已存在 {0} 模块",category));

            if (!string.IsNullOrEmpty(category) && logCateActions.ContainsKey(category))
            {
                return logCateActions[category];
            }
            return defaultWriter;
        }
         
        /// <summary>
        ///   异步缓冲池
        /// </summary>
        private static ActionBlock<LogInfo> logActionList=new ActionBlock<LogInfo>(info =>
        {
            var act = CreateLogWriter(info.Category);
            act.WriteLog(new LogInfo(info.Level,info.Category,info.Message,info.Key));
        },new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 3 } );

        /// <summary>
        /// 注册日志存储模块
        /// </summary>
        /// <param name="category"></param>
        /// <param name="writer"></param>
        public static void RegisterLogWrite(string category,ILogWriter writer )
        {
            logCateActions.Add(category, writer);
        }

        #endregion


        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="category">  分类  </param>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        public static void Info(string category,string msg,object key=null)
        {
            Log(new LogInfo(LogLevelEnum.Info, category, msg, key));
        }

        /// <summary>
        /// 记录警告，用于未处理异常的捕获
        /// </summary>
        /// <param name="category">  分类  </param>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        public static void Warning(string category, string msg, object key = null)
        {
            Log(new LogInfo(LogLevelEnum.Warning, category, msg, key));
        }

        /// <summary>
        /// 记录错误，用于捕获到的异常信息记录
        /// </summary>
        /// <param name="category">  分类  </param>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        public static void Error(string category, string msg, object key = null)
        {
            Log(new LogInfo(LogLevelEnum.Error, category, msg, key));
        }

        /// <summary>
        /// 记录错误，用于捕获到的异常信息记录
        /// </summary>
        /// <param name="category">  分类  </param>
        /// <param name="msg"> 日志信息  </param>
        /// <param name="key">  关键值  </param>
        public static void Trace(string category, string msg, object key = null)
        {
            Log(new LogInfo(LogLevelEnum.Trace, category, msg, key));
        }

        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="info"></param>
        public static void Log(LogInfo info)
        {
            logActionList.Post(info);
        }
    }
}
