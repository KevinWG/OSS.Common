#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：OSSCore —— 领域请求扩展
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-4-21
*       
*****************************************************************************/

#endregion


namespace OSS.Common.Domain
{
    /// <summary>
    ///  
    /// </summary>
    public static class DomainReqExtension
    {
        /// <summary>
        ///  执行请求方法
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Task<TRes> ExecuteAsync<TReq, TRes>(this IDomainReq<TReq, TRes> req)
            where TReq : IDomainReq<TReq, TRes>
        {
            return DomainContainer<TReq,TRes>.GetExecutor().ExecuteAsync((TReq)req);
        }
    }
}