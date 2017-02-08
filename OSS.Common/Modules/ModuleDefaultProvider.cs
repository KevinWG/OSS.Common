#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：全局模块提供者
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*    	修改日期：2017-2-8
*    	修改内容：标准库的缓存兼容
*       
*****************************************************************************/

#endregion
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


        /// <summary>
        ///   返回cache模块
        /// </summary>
        /// <param name="cacheModuleName"></param>
        /// <returns></returns>
        public virtual ICache GetCache(string cacheModuleName)
        {
#if NETFW
            return new Cache();
#else
            return null;
#endif

        }

#if NETFW
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
