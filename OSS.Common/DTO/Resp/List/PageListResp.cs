#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用分页实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion


namespace OSS.Common.Resp
{
    /// <summary>
    ///  分页实体
    /// </summary>
#pragma warning disable CS8766
    public class PageListResp<TModel> : ListResp<TModel>, IPageList<TModel>
#pragma warning restore CS8766
    {
        /// <summary>
        ///    空构造函数  照顾  json序列化 
        /// </summary>
        public PageListResp()
        {
        }

        /// <summary>
        /// 分页响应实体
        /// </summary>
        /// <param name="pList"></param>
        public PageListResp(IPageList<TModel> pList) :this(pList.total,pList.data)
        {
        }

        /// <summary>
        ///  分页响应实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="totalCount"></param>
        public PageListResp(int totalCount, IList<TModel> list):base(list)
        {
            total = totalCount;
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

    }



 
}
