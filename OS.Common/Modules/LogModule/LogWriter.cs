using System;
using System.IO;
using System.Text;

namespace OS.Common.LogModule
{
    internal class LogWriter : ILogWriter
    {
        private readonly string logDirPath = null;
        private const string logFormat = "{0:T}       {1}      {2}:{3}";

        public LogWriter()
        {
            logDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"log\");
            if (!Directory.Exists(logDirPath))
                Directory.CreateDirectory(logDirPath);
        }

        private string getLogFilePath(string type)
        {
            StringBuilder sb = new StringBuilder(logDirPath);
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
        /// <param name="info"></param>
        public void WriteLog(LogInfo info)
        {
            lock (obj)
            {
                using (StreamWriter sw = new StreamWriter(getLogFilePath(info.Level.ToString()), true, Encoding.Default))
                {
                    sw.WriteLine(logFormat, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), info.Key, info.ModuleName, info.Message);
                    sw.Close();
                }
            }
        }

    }
}
