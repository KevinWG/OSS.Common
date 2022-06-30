using System.Threading.Tasks;

namespace OSS.Common;

/// <summary>
///  访问秘钥提供者
/// </summary>
public interface IAccessSecretProvider: IAccessSecretProvider<IAccessSecret>
{
}

/// <summary>
///  访问秘钥提供者
/// </summary>
public interface IAccessSecretProvider<TSecret>
    where TSecret : IAccessSecret
{
    /// <summary>
    /// 获取访问秘钥信息
    /// </summary>
    /// <returns></returns>
    Task<TSecret> GetAccessSecret();
}