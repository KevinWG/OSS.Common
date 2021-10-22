
using System.Threading.Tasks;

namespace OSS.Common.Domain
{

    /// <summary>
    ///  领域执行处理接口
    /// </summary>
    /// <typeparam name="TReq"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    public interface IDomainExecutor<in TReq, TRes>
        where TReq : IDoaminReq<TReq, TRes>
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<TRes> ExecuteAsync(TReq req);
    }

}
