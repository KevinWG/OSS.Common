using System.Collections.Concurrent;
using OS.Common.ComModels;

namespace OS.Common.Modules.DirConfigModule
{
    /// <summary>
    /// 字典配置通用存储获取信息
    /// </summary>
    public static class DirConfigUtil
    {
        private static readonly ConcurrentDictionary<string, IDirConfig> _dirConfigDirs = new ConcurrentDictionary<string, IDirConfig>();

        /// <summary>
        /// 通过模块名称获取
        /// </summary>
        /// <param name="dirConfigModule"></param>
        /// <returns></returns>
        private static IDirConfig GetDirConfig(string dirConfigModule)
        {
            if (string.IsNullOrEmpty(dirConfigModule))
                dirConfigModule = ModuleNames.Default;

            if (_dirConfigDirs.ContainsKey(dirConfigModule))
                return _dirConfigDirs[dirConfigModule];

            var dirConfig = OsConfig.Provider.GetDirConfig(dirConfigModule) ?? new DirConfig();
            _dirConfigDirs.TryAdd(dirConfigModule, dirConfig);

            return dirConfig;
        }


        /// <summary>
        /// 设置字典配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dirConfig"></param>
        /// <param name="moduleName">模块名称</param>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        public static ResultModel SetDirConfig<TConfig>(string key, TConfig dirConfig,
            string moduleName = null) where TConfig : class, new()
        {
            return GetDirConfig(moduleName).SetDirConfig(key, dirConfig);
        }


        /// <summary>
        ///   获取字典配置
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="key"></param>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        public static TConfig GetDirConfig<TConfig>(string key,  string moduleName = null) where TConfig : class ,new()
        {
            return GetDirConfig(moduleName).GetDirConfig<TConfig>(key);
        }

        /// <summary>
        ///  移除配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        public static ResultModel RemoveDirConfig( string key, string moduleName = null)
        {
            return GetDirConfig(moduleName).RemoveDirConfig(key);
        }
    }
}
