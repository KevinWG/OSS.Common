namespace OSS.Common;

/// <summary>
///  访问秘钥实体
/// </summary>
public class AccessSecret : IAccessSecret
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public AccessSecret():this(string.Empty,string.Empty)
    {
    }
    
    /// <summary>
    ///  构造函数
    /// </summary>
    /// <param name="accessKey"></param>
    /// <param name="accessSecret"></param>
    public AccessSecret(string accessKey, string accessSecret)
    {
        access_key     = accessKey;
        access_secret = accessSecret;
    }

    /// <inheritdoc />
    public string access_key { get; set; }

    /// <inheritdoc />
    public string access_secret { get; set; }

}