#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：缓存的默认实现（只适用在Framework框架下）
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
using System;
using Microsoft.Extensions.Primitives;
#if NETFW
using System.Runtime.Caching;
#else
using Microsoft.Extensions.Caching.Memory;
#endif
namespace OSS.Common.Modules.CacheModule
{
    /// <summary>
    /// 
    /// </summary>
    public class Cache : ICache
    {
#if !NETFW
        private static readonly MemoryCache m_Cache=new MemoryCache(new MemoryCacheOptions());
#endif
        /// <summary>
        /// 添加缓存,如果存在则更新
        /// </summary>
        /// <typeparam name="T">添加缓存对象类型</typeparam>
        /// <param name="key">添加对象的key</param>
        /// <param name="obj">值</param>
        /// <param name="slidingExpiration">相对过期的TimeSpan  如果使用固定时间  =TimeSpan.Zero</param>
        /// <param name="absoluteExpiration"> 绝对过期时间,不为空则按照绝对过期时间计算 </param>
        /// <returns>是否添加成功</returns>
        public bool AddOrUpdate<T>(string key, T obj, TimeSpan slidingExpiration, DateTime? absoluteExpiration = null)
        {
            return Add(key, obj, slidingExpiration, absoluteExpiration, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        private static bool Add<T>(string key, T obj, TimeSpan slidingExpiration, DateTime? absoluteExpiration, bool isUpdate)
        {
            if (slidingExpiration == TimeSpan.Zero && absoluteExpiration == null)
                throw new ArgumentNullException("slidingExpiration", "缓存过期时间不正确,需要设置固定过期时间或者相对过期时间");
#if NETFW
            var cachePllicy = new CacheItemPolicy();

            if (slidingExpiration == TimeSpan.Zero)
                cachePllicy.AbsoluteExpiration = new DateTimeOffset(absoluteExpiration.Value);
            else
                cachePllicy.SlidingExpiration = slidingExpiration;

            if (isUpdate)
            {
                MemoryCache.Default.Set(key, obj, cachePllicy);
                return true;
            }
            return MemoryCache.Default.Add(key, obj, cachePllicy);
#else
            var opt = new MemoryCacheEntryOptions();
           if (absoluteExpiration != null)
                opt.AbsoluteExpiration = new DateTimeOffset(absoluteExpiration.Value);
            else
                opt.SlidingExpiration = slidingExpiration;

            m_Cache.Set(key, obj, opt);
            return true;
#endif
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">获取缓存对象类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>获取指定key对应的值 </returns>
        public T Get<T>(string key)
        {
#if NETFW
            return (T)MemoryCache.Default.Get(key);
#else
            return m_Cache.Get<T>(key);
#endif
        }


        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns>是否成功</returns>
        public bool Remove(string key)
        {
#if NETFW
             return MemoryCache.Default.Remove(key)!=null;
#else
             m_Cache.Remove(key);
            return true;
#endif
        }
    }
}
