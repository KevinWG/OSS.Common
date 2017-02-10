#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：基础异步的默认实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
using System;
using System.Threading.Tasks;

namespace OSS.Common.Modules.AsynModule
{
    internal class AsynBlock :  IAsynBlock
    {
        /// <summary>
        ///  异步方法
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
