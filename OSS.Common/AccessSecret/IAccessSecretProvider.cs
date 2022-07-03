using System.Threading.Tasks;

namespace OSS.Common;

/// <summary>
///  访问秘钥提供者
/// </summary>
public interface IAccessSecretProvider: IAccessProvider<IAccess>
{
}

/// <summary>
///  访问信息提供者
/// </summary>
public interface IAccessProvider<TAccess>
    where TAccess : IAccess
{
    /// <summary>
    /// 获取访问秘钥信息
    /// </summary>
    /// <returns></returns>
    Task<TAccess> GetAccessSecret();
}