#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：排序枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.Extention;

namespace OSS.Common.BasicMos.Enums
{
    /// <summary>
    ///     通用状态码 （如果需要更多状态需要自定义枚举，此值不再新增）
    ///     不同的领域对象可能会一到多个
    /// </summary>
    public enum CommonStatus
    {
        /// <summary>
        ///    删除
        /// </summary>
        [OSDescript("已删除")] Deleted = -1000,

        /// <summary>
        /// 待激活
        /// </summary>
        [OSDescript("待激活")] UnActived = -100,

        /// <summary>
        ///   正常
        /// </summary>
        [OSDescript("正常")] Original = 0,

        /// <summary>
        ///  结束
        /// </summary>
        [OSDescript("已结束")] Finished = 10000
    }
}
