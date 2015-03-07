using System;
using System.Collections.Generic;
using OS.Common.LogModule;
using OS.Common.Modules;
using OS.Common.Modules.AsynModule;
using OS.Common.Modules.CacheModule;

namespace OS.Common
{
    /// <summary>
    /// 基础配置模块
    /// </summary>
    public static class OSConfig
    {
        #region  日志模块基本配置模块

        /// <summary>
        ///    注册 日志 事件
        /// </summary>
        /// <param name="logModules"></param>
        public static void RegLogModule(IDictionary<string, ILogWriter> logModules)
        {
            RegisterModule<ILogWriter, LogWriter>(LogUtil.LogModules, logModules, ModuleLogKeys.Default);
        }

        /// <summary>
        ///   日志记录时使用的异步模块名称
        /// </summary>
        /// <param name="asynModuleName"></param>
        public static void RegLogAsynModuleName(string asynModuleName)
        {
            if (string.IsNullOrEmpty(asynModuleName))
                throw new ArgumentNullException("asynModuleName","传入日志异步模块名称不能为空");

            LogUtil.LogAsynModuleName = asynModuleName;
        }

        #endregion

        #region  异步模块  

        /// <summary>
        ///    注册外部异步事件
        /// </summary>
        /// <param name="asynBlockModules"></param>
        public static void RegAsynModule(IDictionary<string, IAsynBlock> asynBlockModules)
        {
            RegisterModule<IAsynBlock, AsynBlock>(AsynUtil.AsynModules, asynBlockModules, ModuleAsynKeys.Default);
        }

        #endregion

        #region  缓存模块

        /// <summary>
        /// 注册缓存模块
        /// </summary>
        /// <param name="moduleCaches"></param>
        public static void RegCacheModule(IDictionary<string, ICache> moduleCaches)
        {
            RegisterModule<ICache, Cache>(CacheUtil.CacheModules, moduleCaches, ModuleCacheKeys.Default);
        }


        #endregion


        /// <summary>
        ///     注册缓存，异步，日志等模块
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TM"></typeparam>
        /// <param name="moduleCaches"></param>
        /// <param name="addModuleCaches"></param>
        /// <param name="defaultModuleName"></param>
        private static void RegisterModule<TI, TM>(IDictionary<string, TI> moduleCaches,
            IDictionary<string, TI> addModuleCaches, string defaultModuleName)
            where TM : TI, new()
        {
            foreach (var moc in addModuleCaches)
            {
                if (moduleCaches.ContainsKey(moc.Key))
                {
                    if (moc.Key == ModuleCacheKeys.Default)
                    {
                        moduleCaches[defaultModuleName] = moc.Value;
                        continue;
                    }
                    throw new ArgumentException(string.Concat(moc.Key, "缓存模块已存在"), "moduleName");
                }
                moduleCaches.Add(moc.Key, moc.Value);
            }
            if (!moduleCaches.ContainsKey(defaultModuleName))
            {
                moduleCaches.Add(defaultModuleName, new TM());
            }
        }

    }
}
