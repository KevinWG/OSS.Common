#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion


namespace OSS.Common.Resp
{
    /// <summary>
    /// 列表结果实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class ListResp<TType> : Resp<IList<TType>>
    {
        /// <inheritdoc />
        public ListResp()
        {
        }

        /// <inheritdoc />
        public ListResp(IList<TType>? data)
        {
            this.data = data;
        }
    }
}