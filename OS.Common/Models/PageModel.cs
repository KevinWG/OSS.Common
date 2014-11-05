using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using OS.Common.Models.Enums;

namespace OS.Common.Models
{

    public static class SearchModelExtention
    {
        /// <summary>
        /// 添加过滤条件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="columns"></param>
        public static void AddFilterColumn(this SearchModel model, NameValueCollection columns)
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
    }

     

    /// <summary>
    /// 搜索实体
    /// </summary>
    public class SearchModel
    {

        public SearchModel()
        {
            FilterDics = new Dictionary<string, string>();
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
        
        public int StartRow
        {
            get { return (CurrentPage - 1)*PageSize; }
        }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public SortType Sort { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public IDictionary<string, string> FilterDics { get; set; }


    }



    /// <summary>
    ///  分页实体
    /// </summary>
    public class PageListModel : SearchModel 
    {

        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get { return (int)Math.Ceiling((double)Total/PageSize); }
        }
    }

    /// <summary>
    ///  分页实体
    /// </summary>
    public class PageListModel<TModel> : PageListModel where TModel : class ,new()
    {
        protected PageListModel()
        {

        }

        public PageListModel(List<TModel> list, SearchModel model, long total)
        {
            List = list;
            CurrentPage = model.CurrentPage;
            FilterDics = model.FilterDics;
            OrderBy = model.OrderBy;
            Sort = model.Sort;
            PageSize = model.PageSize;
            CurrentPage = model.CurrentPage;
            Total = total;
        }

        /// <summary>
        /// 实体列表
        /// </summary>
        public List<TModel> List { get; private set; }
    }


    public class SimplePageList<TModel> 
    {
        public SimplePageList(List<TModel> list, long total)
        {
            List = list;
            Total = total;
        }

        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 实体列表
        /// </summary>
        public List<TModel> List { get; private set; }
    }

}
