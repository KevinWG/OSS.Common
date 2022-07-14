
using System;
using System.Threading.Tasks;

namespace OSS.Common.Resp;

/// <summary>
/// 分页列表接口扩展
/// </summary>
public static class PageListExtension
{
    /// <summary>
    ///  转化为通行token分页列表
    /// </summary>
    /// <returns></returns>
    public static PageTokenListResp<TResult> ToPageTokenResp<TResult>(this PageList<TResult> pageList)
    {
        return new PageTokenListResp<TResult>(pageList);
    }

    /// <summary>
    /// 转化通行token分页列表
    /// -附带指定列的token处理
    /// </summary>
    /// <param name="pageList"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="valueTokenGenerator">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static PageTokenListResp<TData> ToPageTokenResp<TData>(this PageList<TData> pageList,
                                                                  string tokenColumnName,
                                                                  Func<TData, string> tokenKeySelector,
                                                                  Func<TData, string> valueTokenGenerator)
    {
        return pageList.ToPageTokenResp()
            .AddColumnToken(tokenColumnName, tokenKeySelector, valueTokenGenerator);
    }
    
    /// <summary>
    ///  转化为通行token分页列表
    /// </summary>
    /// <param name="taskPageList"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="valueTokenGenerator">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static async Task<PageTokenListResp<TData>> ToPageTokenResp<TData>(this Task<PageList<TData>> taskPageList,
                                                                              string tokenColumnName,
                                                                              Func<TData, string> tokenKeySelector,
                                                                              Func<TData, string> valueTokenGenerator)
    {
        var pageListResp = await taskPageList;
        return pageListResp.ToPageTokenResp(tokenColumnName, tokenKeySelector, valueTokenGenerator);
    }

}