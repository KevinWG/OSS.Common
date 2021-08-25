using System;
using System.Collections.Generic;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  列表通行token接口
    /// </summary>
    public interface IListPassTokens<TType>
    {
        /// <summary>
        ///  列表
        /// </summary>
        IList<TType> data { get; }

        /// <summary>
        ///  列表关联外部字段token字典
        /// </summary>
        public Dictionary<string, Dictionary<string,string>> pass_tokens { get; set; }
    }

    /// <summary>
    /// 通行token列表扩展
    /// </summary>
    internal static class IListPassTokensMap
    {
        /// <summary>
        ///  处理列表token处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="listRes"></param>
        /// <param name="tokenColumnName">关联的key列名称</param>
        /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
        /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
        /// <returns></returns>
        public static IListPassTokens<TResult> AddColumnToken<TResult>(this IListPassTokens<TResult> listRes, string tokenColumnName, Func<TResult, string> tokenKeySelector, Func<TResult, string> tokenValueTokenSelector)
        {
            if (string.IsNullOrEmpty(tokenColumnName)|| tokenKeySelector == null || tokenValueTokenSelector == null)
            {
                throw new ArgumentNullException($"{nameof(tokenColumnName)},{nameof(tokenKeySelector)},{nameof(tokenValueTokenSelector)}"," 参数不能为空!");
            }

            if (listRes.data != null)
            {
                if (listRes.pass_tokens == null)
                {
                    listRes.pass_tokens = new Dictionary<string, Dictionary<string, string>>();
                }

                listRes.pass_tokens[tokenColumnName] = GeneratePassToken(listRes.data, tokenKeySelector, tokenValueTokenSelector);
            }
            return listRes;
        }

        public static Dictionary<string, string> GeneratePassToken<TResult>(this IList<TResult> items, Func<TResult, string> keyValueSelector, Func<TResult, string> keyValueTokenSelector)
        {
            var dics = new Dictionary<string, string>(items.Count);
            foreach (var dataItem in items)
            {
                var key = keyValueSelector(dataItem);
                if (!string.IsNullOrEmpty(key))
                {
                    dics[key] = keyValueTokenSelector(dataItem);
                }
            }
            return dics;
        }


    }


}