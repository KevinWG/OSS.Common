namespace OSS.Common.Resp;

/// <summary>
///  分页实体（附带列表对应通行token字典
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class TokenPageListResp<TModel> : PageListResp<TModel>, ITokenList<TModel>
{
    /// <inheritdoc />
    public TokenPageListResp()
    {
       
    }


    /// <inheritdoc />
    public TokenPageListResp(IPageList<TModel> pList) : base(pList.total, pList.data)
    {
    }

    /// <inheritdoc />
    public TokenPageListResp(int totalCount, IList<TModel> list) : base(totalCount, list)
    {
    }

    /// <inheritdoc />
    public Dictionary<string, Dictionary<string, string>> pass_tokens { get; set; }  = new();
}