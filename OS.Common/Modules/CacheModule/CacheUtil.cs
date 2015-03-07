using System;
using System.Collections.Generic;

namespace OS.Common.Modules.CacheModule
{
    public static class CacheUtil
    {
        internal static Dictionary<string, ICache> CacheModules=
            new Dictionary<string, ICache>();

        static CacheUtil()
        {
            if (!CacheModules.ContainsKey(ModuleAsynKeys.Default))
            {
                CacheModules.Add(ModuleCacheKeys.Default, new Cache());
            }
        }

        /// <summary>
        /// 通过模块名称获取
        /// </summary>
        /// <param name="cacheModule"></param>
        /// <returns></returns>
        public static ICache GetCache(string cacheModule)
        {
            if (!string.IsNullOrEmpty(cacheModule) && CacheModules.ContainsKey(cacheModule))
            {
                return CacheModules[cacheModule];
            }
            return CacheModules[ModuleCacheKeys.Default];
        }

        /// <summary>
        /// 添加缓存，已存在不更新
        /// </summary>
        /// <typeparam name="T">添加缓存对象类型</typeparam>
        /// <param name="key">添加对象的key</param>
        /// <param name="obj">值</param>
        /// <param name="slidingExpiration">相对过期的TimeSpan</param>
        /// <param name="absoluteExpiration"> 绝对过期时间 </param>
        /// <param name="db"> 缓存分区名称 </param>
        /// <param name="moduleName"> 模块名称 </param>
        /// <returns>是否添加成功</returns>
        public static bool Add<T>(string key, T obj, TimeSpan slidingExpiration, DateTime? absoluteExpiration = null,
            int db = 0, string moduleName=ModuleCacheKeys.Default)
        {
            return GetCache(moduleName).Add(key, obj, slidingExpiration, absoluteExpiration, db);
        }

        /// <summary>
        /// 添加缓存,如果存在则更新
        /// </summary>
        /// <typeparam name="T">添加缓存对象类型</typeparam>
        /// <param name="key">添加对象的key</param>
        /// <param name="obj">值</param>
        /// <param name="slidingExpiration">相对过期的TimeSpan</param>
        /// <param name="absoluteExpiration"> 绝对过期时间 </param>
        /// <param name="db"> 缓存分区db </param>
        /// <param name="moduleName">模块名称</param>
        /// <returns> 是否添加成功 </returns>
        public static bool AddOrUpdate<T>(string key, T obj, TimeSpan slidingExpiration, DateTime? absoluteExpiration = null,
            int db = 0, string moduleName=ModuleCacheKeys.Default)
        {
            return GetCache(moduleName).AddOrUpdate(key, obj, slidingExpiration, absoluteExpiration, db);
        }


        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">获取缓存对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="db">缓存分区db</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>获取指定key对应的值 </returns>
        public static T Get<T>(string key, int db=0, string moduleName=ModuleCacheKeys.Default)
        {
            return GetCache(moduleName).Get<T>(key, db);
        }


        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">获取缓存对象类型</typeparam>
        /// <param name="keys">  key列表   </param>
        /// <param name="db">缓存分区db</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns> 获取多个不同类型key对应的不同值 </returns>
        public static IDictionary<string, object> Get<T>(IEnumerable<String> keys, int db=0, string moduleName=ModuleCacheKeys.Default)
        {
            return GetCache(moduleName).Get<T>(keys, db);
        }

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>是否成功</returns>
        public static bool Remove(string key, int db=0, string moduleName=ModuleCacheKeys.Default)
        {
            return GetCache(moduleName).Remove(key, db);
        }


        /// <summary>
        ///   判断是否存在缓存对象
        /// </summary>
        /// <param name="key">  key值  </param>
        /// <param name="db"> 缓存分区db </param>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        public static bool Contains(string key, int db=0, string moduleName=ModuleCacheKeys.Default)
        {
            return GetCache(moduleName).Contains(key, db);
        }
    }
}
