using System;
using System.Collections.Generic;
using OS.Common.Modules.CacheModule;

namespace OS.Common.Modules.AsynModule
{
    /// <summary>
    /// 异步
    /// </summary>
    public static class AsynUtil
    {
        internal static Dictionary<string, IAsynBlock> AsynModules=
            new Dictionary<string, IAsynBlock>();

        static AsynUtil()
        {
            if (!AsynModules.ContainsKey(ModuleAsynKeys.Default))
            {
                AsynModules.Add(ModuleAsynKeys.Default,new AsynBlock());
            }
        }

        /// <summary>
        /// 通过模块名称获取异步处理模块实例
        /// </summary>
        /// <param name="asynModule"></param>
        /// <returns></returns>
        public static IAsynBlock GetAsynBlock(string asynModule)
        {
            if (!string.IsNullOrEmpty(asynModule)&& AsynModules.ContainsKey(asynModule))
            {
                return AsynModules[asynModule];
            }
            return AsynModules[ModuleAsynKeys.Default];
        }


        /// <summary>
        /// 调用异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asynAction"></param>
        /// <param name="t"></param>
        /// <param name="moduleName"> 异步模块名称 </param>
        /// <returns></returns>
        public static bool Asyn<T>(Action<T> asynAction, T t,string moduleName=ModuleAsynKeys.Default)
        {
            return GetAsynBlock(moduleName).Asyn(asynAction, t);
        }


    }
}
