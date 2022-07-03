using System;

namespace OSS.Common.Resp;

/// <summary>
/// 通行token列表扩展
/// </summary>
public static class TokenListRespMap
{
    /// <summary>
    ///  转化为通行token列表
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="listRes"></param>
    /// <returns></returns>
    public static TokenListResp<TData> ToTokenList<TData>(this ListResp<TData> listRes)
    {
        return new TokenListResp<TData>(listRes.data).WithResp(listRes);
    }

    /// <summary>
    /// 转化通行token列表
    /// -附带指定列的token处理
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="listRes"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static TokenListResp<TData> ToTokenList<TData>(this ListResp<TData> listRes, string tokenColumnName, Func<TData, string> tokenKeySelector,
                                                          Func<TData, string> tokenValueTokenSelector)
    {
        return listRes.ToTokenList().AddColumnToken(tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
    }
        
    /// <summary>
    /// 转化通行token列表
    /// -附带指定列的token处理
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="pageList"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static TokenListResp<TData> ToPageTokenList<TData>(this ListResp<TData> pageList,
                                                              string tokenColumnName,
                                                              Func<TData, string> tokenKeySelector,
                                                              Func<TData, string> tokenValueTokenSelector)
    {
        return pageList.ToTokenList()
            .AddColumnToken(tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
    }

    /// <summary>
    ///  列表token处理
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="listRes"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static TokenListResp<TResult> AddColumnToken<TResult>(this TokenListResp<TResult> listRes,
                                                                 string tokenColumnName, Func<TResult, string> tokenKeySelector,
                                                                 Func<TResult, string> tokenValueTokenSelector)
    {
        return IListPassTokensMap.AddColumnToken(listRes, tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
    }
}