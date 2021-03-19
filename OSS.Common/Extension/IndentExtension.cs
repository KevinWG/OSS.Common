using System;
using System.Collections.Generic;

namespace OSS.Common.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class IndentExtension
    {
        /// <summary>
        /// 平级转化为递进结构
        /// </summary>
        /// <typeparam name="TFlat"></typeparam>
        /// <typeparam name="TIndent"></typeparam>
        /// <param name="sourceList"></param>
        /// <param name="convert"></param>
        /// <param name="getParentColumnValue"></param>
        /// <param name="getIndentParentColumnValue"></param>
        /// <param name="defaultParentValue"></param>
        /// <returns></returns>
        public static IList<TIndent> ToIndent<TFlat, TIndent>(this IList<TFlat> sourceList,
           Func<TFlat, IList<TIndent>, TIndent> convert,
           Func<TFlat, string> getParentColumnValue,
           Func<TFlat, string> getIndentParentColumnValue,
           string defaultParentValue)
        {
            return sourceList.ToIndent<TFlat, TIndent, string>(convert, getParentColumnValue, getIndentParentColumnValue, defaultParentValue);
        }

        /// <summary>
        /// 平级转化为递进结构
        /// </summary>
        /// <typeparam name="TFlat"></typeparam>
        /// <typeparam name="TIndent"></typeparam>
        /// <typeparam name="TParentVal"></typeparam>
        /// <param name="sourceList"></param>
        /// <param name="convert">参数：TFlat实体，子级TIndentList-可能为空，输出：TIndent实体</param>
        /// <param name="getParentCompareValue"></param>
        /// <param name="getIndentParentColumnValue"></param>
        /// <param name="defaultParentValue"></param>
        /// <returns></returns>
        public static IList<TIndent> ToIndent<TFlat, TIndent, TParentVal>(this IList<TFlat> sourceList,
            Func<TFlat, IList<TIndent>, TIndent> convert,
            Func<TFlat, TParentVal> getParentCompareValue,
            Func<TFlat, TParentVal> getIndentParentColumnValue,
            TParentVal defaultParentValue)
        {
            var indentList = new List<TIndent>();
            foreach (var item in sourceList)
            {
                if (getParentCompareValue(item).Equals(defaultParentValue))
                {
                    var children = sourceList.ToIndent(convert, getParentCompareValue, getIndentParentColumnValue, getIndentParentColumnValue(item));

                    var indentItem = convert(item, children);
                    indentList.Add(indentItem);
                }
            }
            return indentList;
        }

        /// <summary>
        /// 递进结构转化为平级结构
        /// </summary>
        /// <typeparam name="TIndent"></typeparam>
        /// <typeparam name="TFlat"></typeparam>
        /// <param name="sourceList"></param>
        /// <param name="convert"></param>
        /// <param name="getChildren"></param>
        /// <returns></returns>
        public static IList<TFlat> ToFlat<TIndent, TFlat>(this IList<TIndent> sourceList,
             Func<TIndent, TFlat> convert,
             Func<TIndent, IList<TIndent>> getChildren)
        {
            var flatList = new List<TFlat>();
            foreach (var item in sourceList)
            {
                var flatItem = convert(item);
                flatList.Add(flatItem);

                var children = getChildren(item);
                if (children?.Count > 0)
                {
                    var childFlatList = children.ToFlat(convert, getChildren);
                    flatList.AddRange(childFlatList);
                }
            }
            return flatList;
        }


        /// <summary>
        /// 递进结构转化为平级结构
        /// </summary>
        /// <typeparam name="TIndent"></typeparam>
        /// <typeparam name="TFlat"></typeparam>
        /// <param name="sourceList"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static IList<TFlat> ToFlat<TIndent, TFlat>(this IList<TIndent> sourceList,
           Func<TIndent, TFlat> convert)
            where TIndent : IIndentChild<TIndent>
        {
            var flatList = new List<TFlat>();
            foreach (var item in sourceList)
            {
                var flatItem = convert(item);
                flatList.Add(flatItem);

                var children = item.children;
                if (children?.Count > 0)
                {
                    var childFlatList = children.ToFlat(convert);
                    flatList.AddRange(childFlatList);
                }
            }
            return flatList;
        }
    }



    public interface IIndentChild <TIndent>{ 
        public List<TIndent> children { get; set; }
    }

   
}
