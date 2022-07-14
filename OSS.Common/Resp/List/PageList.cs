
using System.Collections.Generic;

namespace OSS.Common.Resp
{
    /// <summary>
    /// 分页列表信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct PageList<T> : IPageList<T>
    {
        /// <summary>
        /// 分页列表信息
        /// </summary>
        /// <param name="total"></param>
        /// <param name="data"></param>
        public PageList(int total, IList<T> data)
        {
            this.total = total;
            this.data = data;
        }

        public int total { get; }

        public IList<T> data { get; }
    }
}
