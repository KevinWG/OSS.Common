using System;

namespace OSS.Common.Modules.AsynModule
{
    /// <summary>
    /// 
    /// </summary>
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
