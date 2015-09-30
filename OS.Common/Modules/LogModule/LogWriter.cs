using System;
using System.IO;
using System.Text;

namespace OS.Common.Modules.LogModule
{
    internal class LogWriter : ILogWriter
    {
        private readonly string _logBaseDirPath = null;
        private const string _logFormat = "{0:T}       Key:{1}   Detail:{2}";

        public LogWriter()
        {
            _logBaseDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"log");
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

                using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.Default))
                {
                    // ReSharper disable once FormatStringProblem
                    sw.WriteLine(format: _logFormat, arg0: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), arg1: info.MsgKey, arg2: info.Msg);
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// 生成错误编号
        /// </summary>
        /// <returns></returns>
        public string GetLogCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

    }
}
