
using OSS.Common.Modules.AsynModule;
using OSS.Common.Modules.CacheModule;
using OSS.Common.Modules.DirConfigModule;
using OSS.Common.Modules.LogModule;

namespace OSS.Common.Modules
{
    /// <summary>
    /// 模块默认初始化提供方式
    /// </summary>
    public class ModuleBaseProvider
    {
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asynModuleName"></param>
        /// <returns></returns>
        public virtual IAsynBlock GetAsynBlock(string asynModuleName)
        {
            return new AsynBlock();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logModuleName"></param>
        /// <returns></returns>
        public virtual ILogWriter GetLogWrite(string logModuleName)
        {
            return new LogWriter();
        }

#if NETFW
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheModuleName"></param>
        /// <returns></returns>
        public virtual ICache GetCache(string cacheModuleName)
        {
            return new Cache();
        }


        /// <summary>
        /// 获取字典设置实现实体
        ///  todo
        /// </summary>
        /// <param name="dirConfigModuleName"></param>
        /// <returns></returns>
        public virtual IDirConfig GetDirConfig(string dirConfigModuleName)
        {
            return new DirConfig();
        }
#endif
    }
}
