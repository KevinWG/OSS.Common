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

namespace OSS.Common.BasicMos.Enums
{
    /// <summary>
    ///     通用状态码 （如果需要更多状态需要自定义枚举，此值不再新增）
    ///     不同的领域对象可能会一到多个
    /// </summary>
    public enum CommonStatus
    {
        /// <summary>
        ///     删除标识 【软删除】
        /// </summary>
        Delete = -1000,

        /// <summary>
        /// 自定义扩展   【未生效】
        /// </summary>
        UnActived = -100,

        /// <summary>
        ///   默认初始状态
        /// </summary>
        Original = 0,

        /// <summary>
        ///  终结状态
        /// </summary>
        Finished = 10000
    }
}
