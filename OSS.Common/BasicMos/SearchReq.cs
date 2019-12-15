using System.Collections.Generic;
using OSS.Common.BasicMos.Enums;

namespace OSS.Common.BasicMos
{
    /// <summary>
    /// 搜索实体
    /// </summary>
    public class SearchReq
    {
        /// <summary>
        ///   构造函数
        /// </summary>
        public SearchReq()
        {
            filters = new Dictionary<string, string>();
            orders  = new Dictionary<string, SortType>();
        }


        private int _curntPage = 1;

        /// <summary>
        /// 当前页
        /// </summary>
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
        /// 搜索关键字集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, string> filters { get; set; }



        /// <summary>
        ///    获取起始行
        /// </summary>
        public int GetStartRow() => (cur_page - 1) * size;
    }


}
