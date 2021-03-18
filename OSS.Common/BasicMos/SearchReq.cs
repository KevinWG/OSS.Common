using System;
using System.Collections.Generic;
using OSS.Common.BasicMos.Enums;

namespace OSS.Common.BasicMos
{
    /// <summary>
    /// 搜索实体
    /// </summary>
    public class SearchReq<FType>
    {
        /// <summary>
        ///   构造函数
        /// </summary>
        public SearchReq()
        {
            //filters = new Dictionary<string, string>();
            orders  = new Dictionary<string, SortType>();
        }

        /// <inheritdoc />
        public SearchReq(int size, int currentPage) : this()
        {
            this.size = size;
            this.current = currentPage;
        }

        private int _curntPage = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        public int current
        {
            get => _curntPage <= 0 ? 1 : _curntPage;
            set => _curntPage = value;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        [Obsolete("请使用 current ")]
        public int cur_page
        {
            get => _curntPage <= 0 ? 1 : _curntPage;
            set => _curntPage = value;
        }

        private int _pageSize = 20;

        /// <summary>
        /// 页面大小
        /// </summary>
        public int size
        {
            get => _pageSize <= 0 ? 20 : _pageSize;
            set => _pageSize = value;
        }

        /// <summary>
        /// 是否请求获取数量
        /// </summary>
        public bool req_count { get; set; } = true;

        /// <summary>
        /// 排序集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, SortType> orders { get; set; }


        /// <summary>
        /// 过滤器
        /// </summary>
        public FType filter { get; set; }

        /// <summary>
        ///    获取起始行
        /// </summary>
        public int GetStartRow() => (current - 1) * size;
    }


    /// <inheritdoc />
    public class SearchReq : SearchReq<Dictionary<string, object>>
    {
        /// <inheritdoc />
        public SearchReq()
        {
            filter = new Dictionary<string, object>();
        }

        /// <summary>
        /// 搜索关键字集合      适用于多个查询条件
        /// </summary>
        [Obsolete("请使用filter代替")]
        public Dictionary<string, object> filters { get { return filter; } }
    }


}
