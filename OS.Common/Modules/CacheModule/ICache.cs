using System;
using System.Collections.Generic;

namespace OS.Common.Modules.CacheModule
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 添加缓存，已存在不更新
        /// </summary>
        /// <typeparam name="T">添加缓存对象类型</typeparam>
        /// <param name="key">添加对象的key</param>
        /// <param name="obj">值</param>
        /// <param name="slidingExpiration">相对过期的TimeSpan 如果使用固定时间  =TimeSpan.Zero</param>
        /// <param name="absoluteExpiration"> 绝对过期时间 </param>
        /// <param name="db"> 缓存分区db </param>
        /// <returns>是否添加成功</returns>
        bool Add<T>(string key, T obj, TimeSpan slidingExpiration, DateTime? absoluteExpiration = null,
            int db = 0);

        /// <summary>
        /// 添加缓存,如果存在则更新
        /// </summary>
        /// <typeparam name="T">添加缓存对象类型</typeparam>
        /// <param name="key">添加对象的key</param>
        /// <param name="obj">值</param>
        /// <param name="slidingExpiration">相对过期的TimeSpan 如果使用固定时间  =TimeSpan.Zero</param>
        /// <param name="absoluteExpiration"> 绝对过期时间 </param>
        /// <param name="regionName"> 缓存分区db </param>
        /// <returns>是否添加成功</returns>
        bool AddOrUpdate<T>(string key, T obj, TimeSpan slidingExpiration, DateTime? absoluteExpiration = null,
            int db = 0);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">获取缓存对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="db">缓存分区db</param>
        /// <returns>获取指定key对应的值 </returns>
        T Get<T>(string key, int db=0);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">获取缓存对象类型</typeparam>
        /// <param name="keys">  key列表   </param>
        /// <param name="db">缓存分区db</param>
        /// <returns> 获取多个不同类型key对应的不同值 </returns>
        IDictionary<string, object> Get<T>(IEnumerable<String> keys, int db=0);

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns>是否成功</returns>
        bool Remove(string key, int db=0);

        /// <summary>
        ///   判断是否存在缓存对象
        /// </summary>
        /// <param name="key">  key值  </param>
        /// <param name="db"> 缓存分区db </param>
        /// <returns></returns>
        bool Contains(string key, int db=0);
    }
}