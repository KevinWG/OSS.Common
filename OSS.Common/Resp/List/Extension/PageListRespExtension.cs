using System;
using System.Threading.Tasks;

namespace OSS.Common.Resp;

/// <summary>
/// 分页实体扩展
/// </summary>
public static class PageListRespExtension
{
    /// <summary>
    ///  转化为通行token分页列表
    /// </summary>
    /// <returns></returns>
    public static async Task<TokenPageListResp<TResult>> ToTokenPageResp<TResult>(this Task<PageListResp<TResult>> taskPageListResp)
    {
        var pageListResp =await taskPageListResp;
        return new TokenPageListResp<TResult>(pageListResp).WithResp(pageListResp);
    }

    /// <summary>
    ///  转化为通行token分页列表
    /// </summary>
    /// <returns></returns>
    public static TokenPageListResp<TResult> ToTokenPageResp<TResult>(this PageListResp<TResult> pageListResp)
    {
        return new TokenPageListResp<TResult>(pageListResp).WithResp(pageListResp);
    }


    /// <summary>
    /// 转化通行token分页列表
    /// -附带指定列的token处理
    /// </summary>
    /// <param name="pageListResp"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="valueTokenGenerator">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static TokenPageListResp<TData> ToTokenPageResp<TData>(this PageListResp<TData> pageListResp,
                                                                  string tokenColumnName,
                                                                  Func<TData, string> tokenKeySelector,
                                                                  Func<TData, string> valueTokenGenerator)
    {
        var res = pageListResp.ToTokenPageResp();

        res.AddColumnToken(tokenColumnName, tokenKeySelector, valueTokenGenerator);

        return res;
    }


    /// <summary>
    ///  转化为通行token分页列表
    /// </summary>
    /// <param name="taskPageList"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="valueTokenGenerator">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static async Task<TokenPageListResp<TData>> ToTokenPageResp<TData>(this Task<PageListResp<TData>> taskPageList,
                                                                              string tokenColumnName,
                                                                              Func<TData, string> tokenKeySelector,
                                                                              Func<TData, string> valueTokenGenerator)
    {
        var pageListResp = await taskPageList;
        return pageListResp.ToTokenPageResp(tokenColumnName, tokenKeySelector, valueTokenGenerator);
    }
}