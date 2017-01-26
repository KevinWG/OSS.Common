using System;
using System.Threading;

namespace OSS.Common.Modules.AsynModule
{
    internal class AsynBlock :  IAsynBlock
    {
        /// <summary>
        ///  post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="asynAction"></param>
        /// <returns></returns>
        public bool Asyn<T>(Action<T> asynAction, T t)
        {
            return ThreadPool.QueueUserWorkItem(obj =>
            {
                var para = (T) obj;
                asynAction(para);
            }, t);
        }
    }
}
