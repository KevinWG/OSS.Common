using System;
using System.Collections.Generic;

namespace OSS.Common.Extention
{
    /// <summary>
    ///  列表扩展方法
    /// </summary>
    public static class ListExtention
    {
        /// <summary>
        /// List合并
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="source">来源对象</param>
        /// <param name="func">重复项依据方法 true-没有重复项，可以合并，fale-有重复项，不能合并</param>
        /// <returns>返回合并后的目标对象 - 排除重复项</returns>
        public static IList<T> Merged<T>(this IList<T> target, IList<T> source, Func<IList<T>, T, bool> func)
        {
            foreach (var t in source)
            {
                if (func(target, t))
                {
                    target.Add(t);
                }
            }
            return target;
        }

#if !NET40
        /// <summary>
        ///  .net standard 下的list转化扩展方法
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<TResult> ConvertAll<TPara, TResult>(this List<TPara> list, Func<TPara, TResult> func)
        {
            if (list == null)
                return null;
            var resultList = new List<TResult>(list.Count);
            list.ForEach(e => resultList.Add(func(e)));
            return resultList;
        }
#endif

    }
}
