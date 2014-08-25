using System.Collections.Generic;
using OS.Common.Models.Enums;

namespace OS.Common.Models
{
    /// <summary>
    ///  分页实体
    /// </summary>
    public class PageModel
    {
        public PageModel()
        {
            FilterDics=new Dictionary<string, string>();
        }



        private int _CuurntPage = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage {
            get
            {
                if (_CuurntPage<=0)
                {
                    return 1;
                }
                return _CuurntPage;
            }
            set { _CuurntPage = value; }
        }


        private int _PageSize = 20;

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_PageSize <= 0)
                {
                    return 20;
                }
                return _PageSize;
            }
            set { _PageSize = value; }
        }


        public int StartRow {
            get { return (CurrentPage - 1)*PageSize; }
        }

        /// <summary>
        /// 排序类型
        /// </summary>
        public SortType SortType { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }


        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public IDictionary<string, string> FilterDics { get; set; }
    }
}
