namespace OSS.Common.Domain
{
    /// <summary>
    /// 领域容器
    /// </summary>
    /// <typeparam name="TReq"></typeparam>
    /// <typeparam name="TRes"></typeparam>
    public static class DomainContainer<TReq, TRes>
        where TReq : IDomainReq<TReq, TRes>
    {
        /// <summary>
        ///  获取 执行实例
        /// </summary>
        /// <returns></returns>
        public static IDomainExecutor<TReq, TRes> GetExecutor()
        {
            return InsContainer<IDomainExecutor<TReq, TRes>>.Instance;
        }

        /// <summary>
        ///  设置容器内映射的执行实例类型
        /// </summary>
        /// <typeparam name="TExecutor"></typeparam>
        public static void SetExecutor<TExecutor>()
            where TExecutor : IDomainExecutor<TReq, TRes>, new()
        {
            InsContainer<IDomainExecutor<TReq, TRes>>.Set<TExecutor>();
        }

        /// <summary>
        ///  设置容器内映射的执行实例创建方法
        /// </summary>
        /// <param name="insCreator"></param>
        public static void SetExecutor(Func<IDomainExecutor<TReq, TRes>?>? insCreator)
        {
            InsContainer<IDomainExecutor<TReq, TRes>>.Set(insCreator);
        }

        /// <summary>
        ///  设置容器内映射的具体实例
        /// </summary>
        /// <typeparam name="TExecutor"></typeparam>
        /// <param name="ins"></param>
        public static void SetExecutor<TExecutor>(TExecutor? ins)
            where TExecutor : IDomainExecutor<TReq, TRes>, new()
        {
            InsContainer<IDomainExecutor<TReq, TRes>>.Set(ins);
        }

    }
}