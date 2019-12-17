#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：OSSCore —— 实体基类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-4-21
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.BasicMos.Enums;

namespace OSS.Common.BasicMos
{
 

    /// <summary>
    ///  基础实体
    /// </summary>
    /// <typeparam name="IdType"></typeparam>
    public class BaseMo<IdType>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public IdType id { get; set; }
    }

    /// <inheritdoc />
    [Obsolete("转移至 OSS.Infrastructure 类库下")]
    public class BaseUIdAndStateMo<IdType> : BaseUIdMo<IdType>
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public CommonStatus status { get; set; }
    }
    /// <inheritdoc />
    [Obsolete("转移至 OSS.Infrastructure 类库下")]
    public class BaseUIdMo<IdType> : BaseMo<IdType>
    {
        /// <summary>
        ///  用户Id
        /// </summary>
        public string u_id { get; set; }
    }

    /// <inheritdoc />
    [Obsolete("转移至 OSS.Infrastructure 类库下")]
    public class BaseStateMo<IdType> : BaseMo<IdType>
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public CommonStatus status { get; set; }
    }
}