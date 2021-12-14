﻿#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

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

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    /// 列表结果实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    [Obsolete()]
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
    ///  列表结果实体（附带列表对应通行token字典
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    [Obsolete()]
    public class TokenListResp<TType> : ListResp<TType>, ITokenList<TType>
    {
        /// <inheritdoc />
        public TokenListResp()
        {
        }

        ///// <inheritdoc />
        //public TokenListResp(ListResp<TType> listRes) : base(listRes.data)
        //{
        //    ret     = listRes.ret;
        //    sys_ret = listRes.sys_ret;
        //    msg     = listRes.msg;
        //}

        /// <inheritdoc />
        public TokenListResp(IList<TType> data) : base(data)
        {
        }

        /// <inheritdoc />
        public Dictionary<string, Dictionary<string, string>> pass_tokens { get; set; }

    }


    /// <summary>
    /// 通行token列表扩展
    /// </summary>
    [Obsolete]
    public static class TokenListRespMap
    {
        /// <summary>
        ///  转化为通行token列表
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="listRes"></param>
        /// <returns></returns>
        public static TokenListResp<TData> ToTokenList<TData>(this ListResp<TData> listRes)
        {
            return new TokenListResp<TData>(listRes.data).WithResp(listRes);
        }

        /// <summary>
        /// 转化通行token列表
        /// -附带指定列的token处理
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="listRes"></param>
        /// <param name="tokenColumnName">关联的key列名称</param>
        /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
        /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
        /// <returns></returns>
        public static TokenListResp<TData> ToTokenList<TData>(this ListResp<TData> listRes, string tokenColumnName, Func<TData, string> tokenKeySelector,
            Func<TData, string> tokenValueTokenSelector)
        {
            return listRes.ToTokenList().AddColumnToken(tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
        }
        
        /// <summary>
        /// 转化通行token列表
        /// -附带指定列的token处理
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="pageList"></param>
        /// <param name="tokenColumnName">关联的key列名称</param>
        /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
        /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
        /// <returns></returns>
        public static TokenListResp<TData> ToPageTokenList<TData>(this ListResp<TData> pageList,
            string tokenColumnName,
            Func<TData, string> tokenKeySelector,
            Func<TData, string> tokenValueTokenSelector)
        {
            return pageList.ToTokenList()
                .AddColumnToken(tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
        }

        /// <summary>
        ///  列表token处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="listRes"></param>
        /// <param name="tokenColumnName">关联的key列名称</param>
        /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
        /// <param name="tokenValueTokenSelector">对应 tokenKeyColumnName 列的 token 值处理</param>
        /// <returns></returns>
        public static TokenListResp<TResult> AddColumnToken<TResult>(this TokenListResp<TResult> listRes,
            string tokenColumnName, Func<TResult, string> tokenKeySelector,
            Func<TResult, string> tokenValueTokenSelector)
        {
            return IListPassTokensMap.AddColumnToken(listRes, tokenColumnName, tokenKeySelector, tokenValueTokenSelector);
        }
    }
}