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

namespace OSS.Common.Resp
{
    /// <summary>
    ///  分页实体（附带列表对应通行token字典
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class PageTokenListResp<TModel> : PageListResp<TModel>, ITokenList<TModel>
    {
        /// <inheritdoc />
        public PageTokenListResp()
        {
        }

        /// <inheritdoc />
        public PageTokenListResp(long totalCount, IList<TModel> list) : base(totalCount, list)
        {
        }


        ///// <inheritdoc />
        //public PageTokenListResp(PageListResp<TModel> pageRes) : base(pageRes.total, pageRes.data)
        //{
        //    ret     = pageRes.ret;
        //    sys_ret = pageRes.sys_ret;
        //    msg     = pageRes.msg;
        //}

        /// <inheritdoc />
        public Dictionary<string, Dictionary<string, string>> pass_tokens { get; set; }
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
        public PageListResp(long totalCount, IList<TModel> list):base(list)
        {
            total = totalCount;
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
        public static PageListResp<TResult> WithPageList<TPara, TResult>(this PageListResp<TResult> pageRes,
            PageListResp<TPara> pageList,
            Func<TPara, TResult> convertFun)
            where TResult : class, new()
            where TPara : class, new()
        {
            pageRes.WithResp(pageList, convertFun);
            pageRes.total = pageList.total;
            return pageRes;
        }

        /// <summary>
        ///  转化为通行token分页列表
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="pageList"></param>
        /// <returns></returns>
        public static PageTokenListResp<TData> ToPageTokenList<TData>(this PageListResp<TData> pageList)
        {
            return new PageTokenListResp<TData>(pageList.total, pageList.data)
                .WithResp(pageList);
        }


        /// <summary>
        /// 转化通行token分页列表
        /// -附带指定列的token处理
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="pageList"></param>
        /// <param name="tokenColumnName">关联的key列名称</param>
        /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
        /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
        /// <returns></returns>
        public static PageTokenListResp<TData> ToPageTokenList<TData>(this PageListResp<TData> pageList,
            string tokenColumnName,
            Func<TData, string> tokenKeySelector,
            Func<TData, string> tokenValueTokenSelector)
        {
            return pageList.ToPageTokenList()
                .AddColumnToken(tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
        }

        /// <summary>
        ///  处理分页列表token处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="listRes"></param>
        /// <param name="tokenColumnName">关联的key列名称</param>
        /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
        /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
        /// <returns></returns>
        public static PageTokenListResp<TResult> AddColumnToken<TResult>(this PageTokenListResp<TResult> listRes,
            string tokenColumnName, Func<TResult, string> tokenKeySelector,
            Func<TResult, string> tokenValueTokenSelector)
        {
            return IListPassTokensMap.AddColumnToken(listRes, tokenColumnName, tokenKeySelector,
                tokenValueTokenSelector);
        }
    }
}
