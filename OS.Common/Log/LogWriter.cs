using System;
using System.IO;
using System.Text;
using OS.Common.Models.Enums;

namespace OS.Common.Log
{
    class LogWriter : ILogWriter
    {
        private readonly string m_LogDirPath = null;
        private const string LOG_FORMAT = "{0:T}       {1}      {2}:{3}";

        public LogWriter()
        {
            m_LogDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"log\");
            if (!Directory.Exists(m_LogDirPath))
                Directory.CreateDirectory(m_LogDirPath);
        }

        private string getLogFilePath(string type)
        {
            StringBuilder sb = new StringBuilder(m_LogDirPath);
            sb.Append(DateTime.Now.ToString("yyyyMMdd"));
            sb.Append("_");
            sb.Append(type);
            sb.Append(".txt");
            return sb.ToString();
        }


        private object obj = new object();

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="category"></param>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        public void WriteLog(LogLevelEnum logLevel, string category,string msg,object key=null)
        {
            lock (obj)
            {
                using (StreamWriter sw = new StreamWriter(getLogFilePath(logLevel.ToString()), true, Encoding.Default))
                {
                    sw.WriteLine(LOG_FORMAT, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), key, category, msg);
                    sw.Close();
                }
            }
        }

    }
}
