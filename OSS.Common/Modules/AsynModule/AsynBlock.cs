using System;
using System.Threading.Tasks;

namespace OSS.Common.Modules.AsynModule
{
    internal class AsynBlock :  IAsynBlock
    {
        /// <summary>
        ///  post
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <param name="t"></param>
        /// <param name="asynAction"></param>
        /// <returns></returns>
        public async Task Asyn<TPara>(Action<TPara> asynAction, TPara t)
        {
            await Task.Run(()=> asynAction(t));
            // ThreadPool.QueueUserWorkItem(obj =>
            //{
            //    var para = (T) obj;
            //    asynAction(para);
            //}, t);
        }
    }
}
