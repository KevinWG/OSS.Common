namespace OSS.Common.Resp;

/// <summary>
/// 分页列表
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPageList<T>
{
    /// <summary>
    ///  总数
    /// </summary>
    int total { get; }

    /// <summary>
    ///  数据
    /// </summary>
    IList<T>? data { get; }
}