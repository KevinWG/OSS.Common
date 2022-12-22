using System;
using System.Collections.Generic;

namespace OSS.Common
{
    /// <summary>
    /// 排序类型
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// 倒序  由大到小
        /// </summary>
        DESC,

        /// <summary>
        /// 顺序  由小到大
        /// </summary>
        ASC
    }

    /// <summary>
    ///  搜索请求基类
    /// </summary>
    public class BaseSearchReq
    {
        /// <summary>
        /// 搜索请求
        /// </summary>
        public BaseSearchReq():this(20,1)
        {
        }

        /// <summary>
        ///  搜索请求
        /// </summary>
        /// <param name="size"></param>
        /// <param name="currentPage"></param>
        public BaseSearchReq(int size, int currentPage)
        {
            this.size = size;
            this.page = currentPage;
        }

        private int _curntPage = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        public int page
        {
            get => _curntPage <= 0 ? 1 : _curntPage;
            set => _curntPage = value;
        }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 是否请求获取数量
        /// </summary>
        public bool req_count { get; set; } = true;

        /// <summary>
        ///    获取起始行
        /// </summary>
        public int GetStartRow() => (page - 1) * size;
    }

    /// <summary>
    /// 搜索实体
    /// </summary>
    public class SearchReq<FType>: BaseSearchReq
    {
        /// <summary>
        ///   构造函数
        /// </summary>
        public SearchReq()
        {
        }

        private Dictionary<string, SortType> _orders;

        /// <summary>
        /// 排序集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, SortType> orders => _orders ??= new Dictionary<string, SortType>();

        /// <summary>
        /// 过滤器
        /// </summary>
        public FType filter { get; set; }
    }

    /// <inheritdoc />
    public class SearchReq : SearchReq<Dictionary<string, string>>
    {
        /// <inheritdoc />
        public SearchReq()
        {
            filter = new Dictionary<string, string>();
        }
    }


}
