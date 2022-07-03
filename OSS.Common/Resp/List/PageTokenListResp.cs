using System.Collections.Generic;

namespace OSS.Common.Resp;

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
    public PageTokenListResp(PageList<TModel> pList) : base(pList.total, pList.data)
    {
    }

    /// <inheritdoc />
    public PageTokenListResp(int totalCount, IList<TModel> list) : base(totalCount, list)
    {
    }

    /// <inheritdoc />
    public Dictionary<string, Dictionary<string, string>> pass_tokens { get; set; }
}