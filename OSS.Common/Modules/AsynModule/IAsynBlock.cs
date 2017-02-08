#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：基础异步接口
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
        Task Asyn<T>(Action<T> asynAction, T t);
    }
}
