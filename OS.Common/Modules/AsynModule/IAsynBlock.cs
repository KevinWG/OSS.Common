using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace OS.Common.Modules.AsynModule
{
    public interface IAsynBlock
    {
        /// <summary>
        /// post
        /// </summary>
        /// <param name="asynAction"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Asyn<T>(Action<T> asynAction, T t);
    }
}
