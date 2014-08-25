

namespace OS.Common.Log
{
    using System;
    using System.Text;
    using System.Web;

    public static class LogHelper
    {

        private static Log.ILogWriter writer = null;

        static LogHelper()
        {
            writer = new Log.LogWriter();
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            lock (writer)
            {
                writer.WriteAccessLog(message);
            }

        }

        /// <summary>
        /// 记录警告，用于未处理异常的捕获
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            lock (writer)
            {
                writer.WriteWarningLog(message);
            }

        }

        /// <summary>
        /// 记录错误，用于捕获到的异常信息记录
        /// </summary>
        /// <param name="message"></param>

        public static void Error( string message )
        {
            lock (writer)
            {
                writer.WriteErrLog( message );
            }

        }
    }
}
