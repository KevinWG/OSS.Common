namespace OSS.Common.Resp
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        ///  转化为列表响应实体
        /// </summary>
        /// <returns></returns>
        public static async Task<ListResp<TData>> ToListResp<TData>(this Task<List<TData>> taskList)
        {
            return new ListResp<TData>(await taskList);
        }

        /// <summary>
        ///  转化为列表响应实体
        /// </summary>
        /// <returns></returns>
        public static async Task<ListResp<TData>> ToListResp<TData>(this Task<IList<TData>> taskList)
        {
            return new ListResp<TData>(await taskList);
        }

        /// <summary>
        ///  转化为列表响应实体
        /// </summary>
        /// <returns></returns>
        public static ListResp<TData> ToListResp<TData>(this IList<TData> list)
        {
            return new ListResp<TData>(list);
        }

        /// <summary>
        ///  转化为通行token列表
        /// </summary>
        /// <returns></returns>
        public static async Task<TokenListResp<TData>> ToTokenListResp<TData>(this Task<List<TData>> taskList)
        {
            return new TokenListResp<TData>(await taskList);
        }

        /// <summary>
        ///  转化为通行token列表
        /// </summary>
        /// <returns></returns>
        public static async Task<TokenListResp<TData>> ToTokenListResp<TData>(this Task<IList<TData>> taskList)
        {
            return new TokenListResp<TData>(await taskList);
        }

        /// <summary>
        ///  转化为通行token列表
        /// </summary>
        /// <returns></returns>
        public static TokenListResp<TData> ToTokenListResp<TData>(this IList<TData> list)
        {
            return new TokenListResp<TData>(list);
        }

    }
}
