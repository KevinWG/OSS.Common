using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Common.Log
{
    interface ILogWriter
    {
        void WriteAccessLog(string causer, string msg);
        void WriteAccessLog(string msg);
        void WriteErrLog(string causer, string msg);
        void WriteErrLog(string msg);
        void WriteWarningLog(string causer, string msg);
        void WriteWarningLog(string msg);
    }
}
