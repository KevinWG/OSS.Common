using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using OSS.Common.ComModels.Enums;
using OSS.Common.Extention;

namespace OSS.Common.ComModels
{

    /// <summary>
    ///  
    /// </summary>
    public static class SearchMoExtention
    {
        /// <summary>
        /// 添加过滤条件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="columns"></param>
        public static void AddFilterColumn(this SearchMo model, NameValueCollection columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (model.FilterDics==null)
                {
                    model.FilterDics=new Dictionary<string, string>(columns.Count);
                }
                model.FilterDics.Add(columns.GetKey(i),columns[i]);
            }
        }

        public static SearchMo ToSearchModel(this NameValueCollection collections)
        {
            var model = new SearchMo();
            model.CurrentPage = collections["CurrentPage"].ToInt32();
            model.PageSize = collections["PageSize"].ToInt32();
            return model;
        }
    }

    /// <summary>
    /// 搜索实体
    /// </summary>
    public class SearchMo
    {
        public SearchMo()
        {
            FilterDics = new Dictionary<string, string>();
            OrderDics=new Dictionary<string, SortType>();
        }

        private int _curntPage = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get
            {
                if (_curntPage<=0)
                {
                    return 1;
                }
                return _curntPage;
            }
            set { _curntPage = value; }
        }

        private int _pageSize = 20;

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    return 20;
                }
                return _pageSize;
            }
            set { _pageSize = value; }
        }
        
        /// <summary>
        ///    起始行 -只读属性
        /// </summary>
        public int StartRow
        {
            get { return (CurrentPage - 1)*PageSize; }
        }

        /// <summary>
        /// 排序集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, SortType> OrderDics { get; set; }

        /// <summary>
        /// 搜索关键字集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, string> FilterDics { get; set; }
    }



    /// <summary>
    ///  分页实体
    /// </summary>
    public class PageListMo<TModel> : ResultMo where TModel : class, new()
    {

        /// <summary>
        ///    空构造函数  照顾  json序列化 
        /// </summary>
        public PageListMo()
        {
        }


        /// <summary>
        ///   出错时  构造函数    
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public PageListMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        ///   正常赋值时的实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="searchMo"></param>
        /// <param name="total"></param>
        public PageListMo(long total, List<TModel> list, SearchMo searchMo)
        {

            if (searchMo == null)
                throw new ArgumentNullException("searchMo", "searchmodel 不能为空");

            Search = searchMo;
            Data = list;
            Total = total;

            _pageSize = searchMo.PageSize;
        }

        /// <summary>
        ///   正常赋值时的实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        public PageListMo(long total, int pageSize, List<TModel> list)
        {
            if (pageSize < 1)
            {
                throw new ArgumentNullException("pageSize", "pageSize 必须大于0");
            }
            Total = total;
            Data = list;
            _pageSize = pageSize;
        }

        /// <summary>
        /// 实体列表
        /// </summary>
        public List<TModel> Data { get; set; }

        /// <summary>
        /// 搜索对象
        /// </summary>
        public SearchMo Search { get; set; }


        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }

        private int _pageSize;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pageSize == 0)
                {
                        return 1;
                }
                return _pageSize;
            }
            set { _pageSize = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get { return (int)Math.Ceiling((double)Total / PageSize); }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class PageListMoMap
    {
        /// <summary>
        ///   转化pageList列表实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pageList"></param>
        /// <param name="convertFun"></param>
        /// <returns></returns>
        public static PageListMo<TResult> ConvertToPageList<TPara, TResult>(this PageListMo<TPara> pageList,
            Func<TPara, TResult> convertFun)
            where TResult : class, new()
            where TPara : class, new()
        {
            if (convertFun == null)
            {
                throw new ArgumentNullException("convertFun", "转化方法不能为空！");
            }

            List<TResult> resultList = null;
            if (pageList.Data != null)
            {
                resultList = pageList.Data.ConvertAll(e => convertFun(e));
            }
            return new PageListMo<TResult>(pageList.Total, resultList, pageList.Search);
        }
    }

}
