namespace OSS.Common.Domain
{
    /// <summary>
    /// 领域的请求接口
    /// </summary>
    /// <typeparam name="TReq"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    public interface IDomainReq<in TReq, out TRes>
        where TReq : IDomainReq<TReq, TRes>
    {
    }
}