using OS.Common.Modules.AsynModule;
using OS.Common.Modules.CacheModule;
using OS.Common.Modules.DirConfigModule;
using OS.Common.Modules.LogModule;

namespace OS.Common.Modules
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
        /// <param name="cacheModuleName"></param>
        /// <returns></returns>
        public virtual ICache GetCache(string cacheModuleName)
        {
            return new Cache();
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



        /// <summary>
        /// 获取字典设置实现实体
        /// </summary>
        /// <param name="dirConfigModuleName"></param>
        /// <returns></returns>
        public virtual IDirConfig GetDirConfig(string dirConfigModuleName)
        {
            return new DirConfig();
        }
    }
}
