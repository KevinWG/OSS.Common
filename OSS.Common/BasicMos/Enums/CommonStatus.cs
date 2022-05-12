#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：排序枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion


using OSS.Common.Extension;
using System;

namespace OSS.Common.BasicMos.Enums
{
    /// <summary>
    ///     通用状态码 （如果需要更多状态需要自定义枚举，此值不再新增）
    ///     不同的领域对象可能会一到多个
    /// </summary>
    [Obsolete]
    public enum CommonStatus
    {
        /// <summary>
        ///    删除
        /// </summary>
        [OSDescribe("已删除")] Deleted = -10000,

        /// <summary>
        /// 待激活
        /// </summary>
        [OSDescribe("待激活")] UnActived = -100,

        /// <summary>
        ///   正常
        /// </summary>
        [OSDescribe("正常")] Original = 0,
        
        /// <summary>
        ///  结束
        /// </summary>
        [OSDescribe("已结束")] Finished = 10000
    }
}
