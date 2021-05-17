#region Copyright (C) 2014 OSS

/*       
　　	文件功能描述：验证属性attribute

　　	创建人:OSSCore
    创建人Email：1985088337@qq.com
    创建日期：2019.08.25

*/

#endregion


using System.Collections.Generic;

namespace OSS.Common.Extension
{
    /// <summary>
    ///  字典扩展
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        ///  有则修改，无则添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value) 
        {
            try
            {
                if (dic.ContainsKey(key))
                {
                    dic[key] = value;
                }
                else
                {
                    dic.Add(key, value);
                }
                return true;
            }
            catch 
            {              
            }
            return false;
        }
    }
}
