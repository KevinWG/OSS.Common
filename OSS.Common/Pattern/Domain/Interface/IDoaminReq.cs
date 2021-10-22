namespace OSS.Common.Domain
{
    /// <summary>
    /// 领域的请求接口
    /// </summary>
    /// <typeparam name="TReq"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    public interface IDoaminReq<in TReq, out TRes>
        where TReq : IDoaminReq<TReq, TRes>
    {
    }
}