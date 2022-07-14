using System.Collections.Generic;

namespace OSS.Common.Resp;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPageList<T>
{
    int total { get; }
    IList<T> data { get; }
}