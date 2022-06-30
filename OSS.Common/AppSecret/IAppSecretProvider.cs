using OSS.Common.Resp;
using System.Threading.Tasks;

namespace OSS.Common;

/// <summary>
///  应用秘钥提供者
/// </summary>
public interface IAppSecretProvider: IAppSecretProvider<IAppSecret>
{
}

/// <summary>
///  应用秘钥提供者
/// </summary>
public interface IAppSecretProvider<TAppSecret>
    where TAppSecret : IAppSecret
{
    /// <summary>
    /// 获取应用秘钥信息
    /// </summary>
    /// <returns></returns>
    Task<IResp<TAppSecret>> GetAppSecret();
}