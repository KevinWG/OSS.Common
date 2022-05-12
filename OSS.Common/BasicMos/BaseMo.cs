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

namespace OSS.Common.BasicMos
{
    /// <summary>
    ///  基础实体
    /// </summary>
    /// <typeparam name="IdType"></typeparam>
   [Obsolete]
    public class BaseMo<IdType>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public IdType id { get; set; }
    }

}