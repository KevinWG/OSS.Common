#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用分页实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  分页实体（附带列表对应通行token字典
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class PageTokenListResp<TModel> : PageListResp<TModel>
    {
        /// <inheritdoc />
        public PageTokenListResp()
        {
        }


        /// <inheritdoc />
        public PageTokenListResp(long totalCount, IList<TModel> list):base(totalCount, list)
        {
        }

        /// <summary>
        ///  和结果列表对应的token字典
        /// </summary>
        public Dictionary<string, string> tokens { get;internal set; }
    }

    /// <summary>
    ///  分页实体
    /// </summary>
    public class PageListResp<TModel> : ListResp<TModel>
    {

        /// <summary>
        ///    空构造函数  照顾  json序列化 
        /// </summary>
        public PageListResp()
        {
        }

        /// <summary>
        ///   正常赋值时的实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="totalCount"></param>
        public PageListResp(long totalCount, IList<TModel> list)
        {
            data = list;
            this.total = totalCount;
        }

        /// <summary>
        /// 总数
        /// </summary>
        public long total { get; set; }
        
    }

    /// <summary>
    /// 分页实体扩展
    /// </summary>
    public static class PageListRespMap
    {
        /// <summary>
        ///  处理响应转化
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pageRes"></param>
        /// <param name="pageList"></param>
        /// <param name="convertFun"></param>
        /// <returns></returns>
        public static PageListResp<TResult> WithPageList<TPara, TResult>(this PageListResp<TResult> pageRes, PageListResp<TPara> pageList,
            Func<TPara, TResult> convertFun)
            where TResult : class, new()
            where TPara : class, new()
        {
            pageRes.WithResp(pageList, convertFun);

            pageRes.total = pageList.total;
            return pageRes;
        }
        
        /// <summary>
        ///  处理列表token处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pageRes"></param>
        /// <param name="tokenKeySelector"></param>
        /// <param name="tokenValueSelector"></param>
        /// <returns></returns>
        public static PageTokenListResp<TResult> WithToken< TResult>(this PageTokenListResp<TResult> pageRes,Func<TResult,string> tokenKeySelector,Func<TResult,string> tokenValueSelector)
        {
            if (tokenKeySelector==null|| tokenValueSelector==null)
            {
                throw new ArgumentNullException("tokenSelector can not be null!");
            }
            if (pageRes.data!=null)
            {
                pageRes.tokens = pageRes.data.ToDictionary(x => tokenKeySelector(x), x => tokenValueSelector(x));
            }

            return pageRes;
        }

    }

}
