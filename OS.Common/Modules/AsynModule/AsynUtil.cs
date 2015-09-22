using System;
using System.Collections.Concurrent;

namespace OS.Common.Modules.AsynModule
{
    /// <summary>
    /// 异步
    /// </summary>
    public static class AsynUtil
    {
        private static readonly ConcurrentDictionary<string, IAsynBlock> _asynDirs = new ConcurrentDictionary<string, IAsynBlock>();//Dictionary<string, IAsynBlock>();
        /// <summary>
        /// 通过模块名称获取异步处理模块实例
        /// </summary>
        /// <param name="asynModule"></param>
        /// <returns></returns>
        public static IAsynBlock GetAsynBlock(string asynModule)
        {
            if (string.IsNullOrEmpty(asynModule))
                asynModule = ModuleNames.Default;
            
            if (_asynDirs.ContainsKey(asynModule))
                return _asynDirs[asynModule];

            var asyn = OsConfig.Provider.GetAsynBlock(asynModule) ?? new AsynBlock();
            _asynDirs.TryAdd(asynModule,asyn);

            return asyn;
        }


        /// <summary>
        /// 调用异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asynAction"></param>
        /// <param name="t"></param>
        /// <param name="moduleName"> 异步模块名称 </param>
        /// <returns></returns>
        public static bool Asyn<T>(Action<T> asynAction, T t,string moduleName=ModuleNames.Default)
        {
            return GetAsynBlock(moduleName).Asyn(asynAction, t);
        }


    }
}
