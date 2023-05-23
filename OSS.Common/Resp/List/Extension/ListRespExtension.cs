namespace OSS.Common.Resp;

/// <summary>
/// 通行token列表扩展
/// </summary>
public static class ListRespExtension
{
    /// <summary>
    ///  转化为通行token列表
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="listRes"></param>
    /// <returns></returns>
    public static TokenListResp<TData> ToTokenListResp<TData>(this ListResp<TData> listRes)
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
    public static TokenListResp<TData> ToTokenListResp<TData>(this ListResp<TData> listRes,
                                                              string tokenColumnName, Func<TData, string> tokenKeySelector,
                                                              Func<TData, string> tokenValueTokenSelector)
    {
        var res = listRes.ToTokenListResp();
        res.AddColumnToken(tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
        return res;
    }

}