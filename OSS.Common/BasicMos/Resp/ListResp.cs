#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  列表结果实体（附带列表对应通行token字典
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class TokenListResp<TType> : ListResp<TType>
    {   
        /// <inheritdoc />
        public TokenListResp()
        {
        }

        /// <inheritdoc />
        public TokenListResp(IList<TType> data):base(data)
        {
        }

        /// <summary>
        ///  和结果列表对应的token字典
        /// </summary>
        public Dictionary<string, string> tokens { get; internal set; }
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
        /// <param name="tokenKeySelector"></param>
        /// <param name="tokenValueSelector"></param>
        /// <returns></returns>
        public static TokenListResp<TResult> WithToken<TResult>(this TokenListResp<TResult> listRes, Func<TResult, string> tokenKeySelector, Func<TResult, string> tokenValueSelector)
        {
            if (tokenKeySelector == null || tokenValueSelector == null)
            {
                throw new ArgumentNullException("tokenSelector can not be null!");
            }

            if (listRes.data != null)
            {
                listRes.tokens = listRes.data.ToDictionary(x => tokenKeySelector(x), x => tokenValueSelector(x));
            }
            return listRes;
        }

    }
}