
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Common.Resp;

/// <summary>
/// 分页列表接口扩展
/// </summary>
public static class PageListExtension
{
    /// <summary>
    /// 转化为分页列表
    /// </summary>
    /// <returns></returns>
    public static async Task<PageListResp<TResult>> ToPageResp<TResult>(this Task<PageList<TResult>> taskPageList)
    {
        var pageList = await taskPageList;
        return new PageListResp<TResult>(pageList);
    }

  
    /// <summary>
    ///  转化为分页列表
    /// </summary>
    /// <returns></returns>
    public static PageListResp<TResult> ToPageResp<TResult>(this PageList<TResult> pageList)
    {
        return new PageListResp<TResult>(pageList);
    }


    /// <summary>
    ///  转化为分页列表
    /// </summary>
    /// <returns></returns>
    public static PageListResp<TResult> ToPageResp<TPara,TResult>(this PageList<TPara> pageList,Func<TPara, TResult> convert)
    {
        return new PageListResp<TResult>(pageList.total, pageList.data.Select(convert).ToList());
    }


    /// <summary>
    /// 转化为通行token分页列表
    /// </summary>
    /// <returns></returns>
    public static async Task<TokenPageListResp<TResult>> ToTokenPageResp<TResult>(this Task<PageList<TResult>> taskPageList)
    {
        var pageList = await taskPageList;
        return new TokenPageListResp<TResult>(pageList);
    }

    /// <summary>
    ///  转化为通行token分页列表
    /// </summary>
    /// <returns></returns>
    public static TokenPageListResp<TResult> ToTokenPageResp<TResult>(this PageList<TResult> pageList)
    {
        return new TokenPageListResp<TResult>(pageList);
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
    public static TokenPageListResp<TData> ToTokenPageResp<TData>(this PageList<TData> pageList,
                                                                  string tokenColumnName,
                                                                  Func<TData, string> tokenKeySelector,
                                                                  Func<TData, string> valueTokenGenerator)
    {
        var res = pageList.ToTokenPageResp();

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
    public static async Task<TokenPageListResp<TData>> ToTokenPageResp<TData>(this Task<PageList<TData>> taskPageList,
                                                                              string tokenColumnName,
                                                                              Func<TData, string> tokenKeySelector,
                                                                              Func<TData, string> valueTokenGenerator)
    {
        var pageListResp = await taskPageList;
        return pageListResp.ToTokenPageResp(tokenColumnName, tokenKeySelector, valueTokenGenerator);
    }

}