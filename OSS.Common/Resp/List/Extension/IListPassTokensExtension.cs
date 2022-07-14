using System;
using System.Collections.Generic;

namespace OSS.Common.Resp;

/// <summary>
/// 通行token列表扩展
/// </summary>
public static class IListPassTokensExtension
{
    /// <summary>
    ///  处理列表token处理
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TTokenList"></typeparam>
    /// <param name="listRes"></param>
    /// <param name="tokenColumnName">关联的key列名称</param>
    /// <param name="tokenKeySelector">对应 tokenKeyColumnName 列的 token key 选择器</param>
    /// <param name="valueTokenGenerator">对应 tokenKeyColumnName 列的 token 值处理</param>
    /// <returns></returns>
    public static TTokenList AddColumnToken<TTokenList, TResult>(this TTokenList listRes, string tokenColumnName,
                                                                 Func<TResult, string> tokenKeySelector, Func<TResult, string> valueTokenGenerator)
        where TTokenList : ITokenList<TResult>
    {
        if (string.IsNullOrEmpty(tokenColumnName) || tokenKeySelector == null || valueTokenGenerator == null)
            throw new ArgumentNullException($"{nameof(tokenColumnName)},{nameof(tokenKeySelector)},{nameof(valueTokenGenerator)}",
                " 参数不能为空!");

        if (listRes.data == null) 
            return listRes;

        listRes.pass_tokens ??= new Dictionary<string, Dictionary<string, string>>();
        listRes.pass_tokens[tokenColumnName] = GenerateColumnTokens(listRes.data, tokenKeySelector, valueTokenGenerator);

        return listRes;
    }


    /// <summary>
    ///  生成列表对应的token
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="items"></param>
    /// <param name="keyValueSelector"></param>
    /// <param name="keyValueTokenGenerator"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GenerateColumnTokens<TResult>(this IList<TResult> items,
                                                                           Func<TResult, string> keyValueSelector, Func<TResult, string> keyValueTokenGenerator)
    {
        var dic = new Dictionary<string, string>(items.Count);
        foreach (var dataItem in items)
        {
            var key = keyValueSelector(dataItem);
            if (!string.IsNullOrEmpty(key))
            {
                dic[key] = keyValueTokenGenerator(dataItem);
            }
        }
        return dic;
    }
}