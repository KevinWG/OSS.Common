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

namespace OSS.Common.Domain
{
    /// <summary>
    ///  基础领域对象
    /// </summary>
    /// <typeparam name="IdType"></typeparam>
    public class BaseMo<IdType>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public IdType id { get; set; }
    }
}