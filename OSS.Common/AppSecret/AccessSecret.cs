namespace OSS.Common;

/// <summary>
///  访问秘钥实体
/// </summary>
public class AccessSecret : IAccessSecret
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public AccessSecret()
    {
    }



    /// <summary>
    ///  构造函数
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="appSecret"></param>
    public AccessSecret(string appId, string appSecret)
    {
        access_key     = appId;
        access_secret = appSecret;
    }

    /// <inheritdoc />
    public string access_key { get; set; }

    /// <inheritdoc />
    public string access_secret { get; set; }

}