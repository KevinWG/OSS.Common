namespace OSS.Common;

/// <summary>
///  应用设置信息接口
/// </summary>
public interface IAppSecret : IAppId
{
    /// <summary>
    /// 应用账号秘钥
    /// </summary>
    public string app_secret { get; }
}


/// <summary>
///  应用信息id接口
/// </summary>
public interface IAppId
{
    /// <summary>
    /// 应用账号id
    /// </summary>
    public string app_id { get; }
}