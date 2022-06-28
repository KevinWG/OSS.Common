﻿using System;
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
    /// 搜索实体
    /// </summary>
    public class SearchReq<FType>
    {
        /// <summary>
        ///   构造函数
        /// </summary>
        public SearchReq()
        {

        }

        /// <inheritdoc />
        public SearchReq(int size, int currentPage) : this()
        {
            this.size = size;
            this.page = currentPage;
        }

        private int _curntPage = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        [Obsolete("请使用 page ")]
        public int current
        {
            get => _curntPage <= 0 ? 1 : _curntPage;
            set => _curntPage = value;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page
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
        private Dictionary<string, SortType> _orders;
        /// <summary>
        /// 排序集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, SortType> orders
        {
            get { return _orders == null ? _orders = new Dictionary<string, SortType>() : _orders; }
        }

        /// <summary>
        /// 过滤器
        /// </summary>
        public FType filter { get; set; }

        /// <summary>
        ///    获取起始行
        /// </summary>
        public int GetStartRow() => (page - 1) * size;
    }

    /// <inheritdoc />
    public class SearchReq : SearchReq<Dictionary<string, string>>
    {
        /// <inheritdoc />
        public SearchReq()
        {
            filter = new Dictionary<string, string>();
        }

        /// <summary>
        /// 搜索关键字集合      适用于多个查询条件
        /// </summary>
        [Obsolete("请使用filter代替")]
        public Dictionary<string, string> filters => filter;
    }


}