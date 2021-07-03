#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using OSS.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  列表结果实体（附带列表对应通行token字典
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class TokenListResp<TType> : ListResp<TType>, IListPassTokens
    {   
        /// <inheritdoc />
        public TokenListResp()
        {
        }

        /// <inheritdoc />
        public TokenListResp(IList<TType> data):base(data)
        {
        }

        /// <inheritdoc />
        public Dictionary<string, string> tokens { get; internal set; }

        /// <inheritdoc />
        public Dictionary<string, Dictionary<string, string>> relate_tokens { get; internal set; }

    }

    /// <summary>
    /// 列表结果实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class ListResp<TType> : Resp<IList<TType>>
    {
        /// <inheritdoc />
        public ListResp()
        {
        }

        /// <inheritdoc />
        public ListResp(IList<TType> data)
        {
            this.data = data;
        }
    }

    /// <summary>
    /// 分页实体扩展
    /// </summary>
    public static class ListRespMap
    {
        /// <summary>
        ///  处理列表token处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="listRes"></param>
        /// <param name="keyValueSelector"></param>
        /// <param name="keyValueTokenSelector"></param>
        /// <returns></returns>
        public static TokenListResp<TResult> WithToken<TResult>(this TokenListResp<TResult> listRes, Func<TResult, string> keyValueSelector, Func<TResult, string> keyValueTokenSelector)
        {
            if (keyValueSelector == null || keyValueTokenSelector == null)
            {
                throw new ArgumentNullException("tokenSelector can not be null!");
            }

            if (listRes.data != null)
            {
                listRes.tokens = GetTokenDics(listRes.data, keyValueSelector, keyValueTokenSelector);
            }
            return listRes;
        }

        /// <summary>
        ///  处理列表token处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="listRes"></param>
        /// <param name="relateKeyName">关联的key名称，以名称寻找对应的key值和通行token</param>
        /// <param name="keyValueSelector">对应relateKeyName对应的每行key值选择器</param>
        /// <param name="keyValueTokenSelector">对应relateKeyName对应的每行token值生成器</param>
        /// <returns></returns>
        public static TokenListResp<TResult> WithRelateToken<TResult>(this TokenListResp<TResult> listRes, string relateKeyName, Func<TResult, string> keyValueSelector, Func<TResult, string> keyValueTokenSelector)
        {
            if (keyValueSelector == null || keyValueTokenSelector == null)
            {
                throw new ArgumentNullException("tokenSelector can not be null!");
            }

            if (listRes.data != null)
            {
                if (listRes.relate_tokens == null)
                {
                    listRes.relate_tokens = new Dictionary<string, Dictionary<string, string>>();
                }

                listRes.relate_tokens[relateKeyName] = GetTokenDics(listRes.data, keyValueSelector, keyValueTokenSelector);
            }
            return listRes;
        }

        private static Dictionary<string, string> GetTokenDics<TResult>(IList<TResult> items, Func<TResult, string> keyValueSelector, Func<TResult, string> keyValueTokenSelector)
        {
            var dics = new Dictionary<string, string>(items.Count);
            foreach (var dataItem in items)
            {
                var key = keyValueSelector(dataItem);
                dics[key] = keyValueTokenSelector(dataItem);
            }
            return dics;
        }


    }
}