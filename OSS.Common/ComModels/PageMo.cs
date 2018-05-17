#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：通用分页实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
using System;
using System.Collections.Generic;
using OSS.Common.ComModels.Enums;

namespace OSS.Common.ComModels
{
    /// <summary>
    /// 搜索实体
    /// </summary>
    public class SearchMo
    {
        /// <summary>
        ///   构造函数
        /// </summary>
        public SearchMo()
        {
            filters = new Dictionary<string, string>();
            orders = new Dictionary<string, SortType>();
        }

        private int _curntPage = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        public int cur_page
        {
            get => _curntPage<=0 ? 1 : _curntPage;
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
        ///    获取起始行
        /// </summary>
        public int GetStartRow()=>(cur_page - 1) * size;
        

        /// <summary>
        /// 排序集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, SortType> orders { get; set; }

        /// <summary>
        /// 搜索关键字集合      适用于多个查询条件
        /// </summary>
        public Dictionary<string, string> filters { get; set; }
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
        public PageListMo(int ret, string message = "")
            : base(ret, message)
        {
        }
        /// <summary>
        ///   正常赋值时的实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="totalCount"></param>
        public PageListMo(long totalCount, List<TModel> list)
        {
            data = list;
            this.total = totalCount;
        }

        /// <summary>
        /// 实体列表
        /// </summary>
        public List<TModel> data { get; set; }
        

        /// <summary>
        /// 总数
        /// </summary>
        public long total { get; set; }
        
    }

    /// <summary>
    /// 分页实体扩展
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
                throw new ArgumentNullException(nameof(convertFun), "转化方法不能为空！");
            }

            if (!pageList.IsSuccess() || pageList.data == null)
                return new PageListMo<TResult>(pageList.ret, pageList.msg);

            var resultList = pageList.data.ConvertAll(e=>convertFun(e));
            return new PageListMo<TResult>(pageList.total, resultList);
        }
    }

}
