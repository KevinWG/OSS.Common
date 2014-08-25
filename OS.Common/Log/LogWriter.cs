using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OS.Common.Log
{
    class LogWriter:ILogWriter
    {
        private string m_LogDirPath = null;
        private const string LOG_FORMAT = "{0:T}\t{1}:{2}";

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


        public void WriteAccessLog(string causer, string msg)
        {
            StreamWriter sw = new StreamWriter(getLogFilePath("access"), true, Encoding.Default);
            sw.WriteLine(LOG_FORMAT, DateTime.Now, causer, msg);
            sw.Close();
        }

        public void WriteAccessLog(string msg)
        {
            WriteAccessLog(string.Empty, msg);
        }

        public void WriteErrLog(string causer, string msg)
        {
            StreamWriter sw = new StreamWriter(getLogFilePath("error"), true, Encoding.Default);
            sw.WriteLine(LOG_FORMAT, DateTime.Now, causer, msg);
            sw.Close();
        }

        public void WriteErrLog(string msg)
        {
            StackFrame sf = new StackFrame(3);
            MethodBase mb = sf.GetMethod();
            WriteErrLog(String.Format("{0}.{1}", mb.ReflectedType, mb.Name), msg);
        }


        public void WriteWarningLog(string causer, string msg)
        {

            StreamWriter sw = new StreamWriter(getLogFilePath("warning"), true, Encoding.Default);
            sw.WriteLine(LOG_FORMAT, DateTime.Now, causer, msg);
            sw.Close();
        }

        public void WriteWarningLog(string msg)
        {
            StackFrame sf = new StackFrame(3);
            MethodBase mb = sf.GetMethod();
            WriteWarningLog(String.Format("{0}.{1}", mb.ReflectedType, mb.Name), msg);
        }
    }
}
