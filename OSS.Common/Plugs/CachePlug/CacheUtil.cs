#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：全局插件 -  缓存插件辅助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Concurrent;

namespace OSS.Common.Plugs.CachePlug
{
    /// <summary>
    /// 缓存的辅助类
    /// </summary>
    public static class CacheUtil
    {
        private static readonly ConcurrentDictionary<string, ICachePlug> _cacheDirs = new ConcurrentDictionary<string, ICachePlug>();

        /// <summary>
        /// 通过模块名称获取
        /// </summary>
        /// <param name="cacheModule"></param>
        /// <returns></returns>
        public static ICachePlug GetCache(string cacheModule)
        {
            if (string.IsNullOrEmpty(cacheModule))
                cacheModule = ModuleNames.Default;

            if (_cacheDirs.ContainsKey(cacheModule))
                return _cacheDirs[cacheModule];

            var cache = OsConfig.CacheProvider?.Invoke(cacheModule) ?? new DefaultCachePlug();

            _cacheDirs.TryAdd(cacheModule, cache);
            return cache;
        }


        /// <summary>
        /// 添加缓存,如果存在则更新
        /// </summary>
        /// <typeparam name="T">添加缓存对象类型</typeparam>
        /// <param name="key">添加对象的key</param>
        /// <param name="obj">值</param>
        /// <param name="slidingExpiration">相对过期的TimeSpan，如果使用固定时间  =TimeSpan.Zero</param>
        /// <param name="absoluteExpiration"> 绝对过期时间 </param>
        /// <param name="moduleName">模块名称</param>
        /// <returns> 是否添加成功 </returns>
        public static bool AddOrUpdate<T>(string key, T obj, TimeSpan slidingExpiration, DateTime? absoluteExpiration = null,
           string moduleName = ModuleNames.Default)
        {
            return GetCache(moduleName).AddOrUpdate(key, obj, slidingExpiration, absoluteExpiration);
        }


        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">获取缓存对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>获取指定key对应的值 </returns>
        public static T Get<T>(string key, string moduleName = ModuleNames.Default)
        {
            return GetCache(moduleName).Get<T>(key);
        }

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>是否成功</returns>
        public static bool Remove(string key, string moduleName = ModuleNames.Default)
        {
            return GetCache(moduleName).Remove(key);
        }

    }
}
