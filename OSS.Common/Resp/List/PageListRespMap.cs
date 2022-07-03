using System;

namespace OSS.Common.Resp;

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
    /// <param name="valueTokenGenerator">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static PageTokenListResp<TData> ToPageTokenList<TData>(this PageListResp<TData> pageList,
                                                                  string tokenColumnName,
                                                                  Func<TData, string> tokenKeySelector,
                                                                  Func<TData, string> valueTokenGenerator)
    {
        return pageList.ToPageTokenList()
            .AddColumnToken(tokenColumnName, tokenKeySelector, valueTokenGenerator);
    }

    /// <summary>
    ///  处理分页列表token处理
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="listRes"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="valueTokenGenerator">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static PageTokenListResp<TResult> AddColumnToken<TResult>(this PageTokenListResp<TResult> listRes,
                                                                     string tokenColumnName, Func<TResult, string> tokenKeySelector,
                                                                     Func<TResult, string> valueTokenGenerator)
    {
        return IListPassTokensMap.AddColumnToken(listRes, tokenColumnName, tokenKeySelector,
            valueTokenGenerator);
    }
}