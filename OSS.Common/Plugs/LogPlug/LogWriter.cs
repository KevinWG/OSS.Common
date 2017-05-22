#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：全局插件 -  日志插件默认实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System;
using System.IO;
using System.Text;

namespace OSS.Common.Plugs.LogPlug
{
    /// <summary>
    /// 系统默认写日志模块
    /// </summary>
    public class LogWriter : ILogWriter
    {
        private readonly string _logBaseDirPath = null;
        private const string _logFormat = "{0:T}       Key:{1}   Detail:{2}";

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogWriter()
        {
            // todo  测试地址是否ok
#if NETFW
            _logBaseDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"log");
#else
            _logBaseDirPath = Path.Combine(AppContext.BaseDirectory, @"log");
#endif
            if (!Directory.Exists(_logBaseDirPath))
                Directory.CreateDirectory(_logBaseDirPath); 
        }

        private string getLogFilePath(string module, LogLevelEnum level)
        {
            string dirPath = string.Format(@"{0}\{1}\{2}\",_logBaseDirPath, module, level);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return string.Concat(dirPath, DateTime.Now.ToString("yyyyMMddHH"), ".txt");
        }

        private object obj = new object();

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="info"></param>
        public void WriteLog(LogInfo info)
        {
            lock (obj)
            {
                string filePath = getLogFilePath(info.ModuleName, info.Level);
#if NETFW
                using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
#else
                using (StreamWriter sw = new StreamWriter(new FileStream(filePath,FileMode.Append,FileAccess.Write), Encoding.UTF8))
#endif
                {
                    sw.WriteLine(format: _logFormat, arg0: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), arg1: info.MsgKey, arg2: info.Msg);
                  
                }
            }
        }

        /// <summary>
        /// 生成错误编号
        /// </summary>
        /// <returns></returns>
        public string GetLogCode(LogInfo info)
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

    }
}
