namespace OSS.Common;


/// <summary>
///  访问信息接口
/// </summary>
public interface IAccess
{
}

/// <summary>
///  访问秘钥Secret接口
/// </summary>
public interface IAccessSecret : IAccessKey
{
    /// <summary>
    /// 访问秘钥Secret
    /// </summary>
    public string access_secret { get; }
}

/// <summary>
///  访问秘钥Key接口
/// </summary>
public interface IAccessKey: IAccess
{
    /// <summary>
    /// 访问秘钥Key
    /// </summary>
    public string access_key { get; }
}

