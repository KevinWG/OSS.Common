namespace OSS.Common;

/// <summary>
///  应用设置信息
/// </summary>
public class AppSecret : IAppSecret
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public AppSecret()
    {
    }

    /// <summary>
    ///  构造函数
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="appSecret"></param>
    public AppSecret(string appId, string appSecret)
    {
        app_id     = appId;
        app_secret = appSecret;
    }

    /// <inheritdoc />
    public string app_id { get; set; }

    /// <inheritdoc />
    public string app_secret { get; set; }

}