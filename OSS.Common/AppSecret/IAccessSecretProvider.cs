﻿using OSS.Common.Resp;
using System.Threading.Tasks;

namespace OSS.Common;

/// <summary>
///  访问秘钥提供者
/// </summary>
public interface IAccessSecretProvider: IAppSecretProvider<IAccessSecret>
{
}

/// <summary>
///  访问秘钥提供者
/// </summary>
public interface IAppSecretProvider<TSecret>
    where TSecret : IAccessSecret
{
    /// <summary>
    /// 获取访问秘钥信息
    /// </summary>
    /// <returns></returns>
    Task<IResp<TSecret>> GetAccessSecret();
}