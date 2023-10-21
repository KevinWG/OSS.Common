namespace OSS.Common.Resp;

/// <summary>
///  列表结果实体（附带列表对应通行token字典
/// </summary>
/// <typeparam name="TType"></typeparam>
public class TokenListResp<TType> : ListResp<TType>, ITokenList<TType>
{
    /// <inheritdoc />
    public TokenListResp()
    {
    }

      
    /// <inheritdoc />
    public TokenListResp(IList<TType> data) : base(data)
    {
    }

    /// <inheritdoc />
    public Dictionary<string, Dictionary<string, string>> pass_tokens { get; set; } = default!;

}