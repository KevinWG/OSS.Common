#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：全局插件 -  配置插件辅助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Concurrent;
using OSS.Common.ComModels;

namespace OSS.Common.Plugs.DirConfigPlug
{
    /// <summary>
    /// 字典配置通用存储获取信息
    /// </summary>
    public static class DirConfigUtil
    {
        private static readonly ConcurrentDictionary<string, IDirConfigPlug> _dirConfigDirs = new ConcurrentDictionary<string, IDirConfigPlug>();

        /// <summary>
        /// 通过模块名称获取
        /// </summary>
        /// <param name="dirConfigModule"></param>
        /// <returns></returns>
        private static IDirConfigPlug GetDirConfig(string dirConfigModule)
        {
            if (string.IsNullOrEmpty(dirConfigModule))
                dirConfigModule = ModuleNames.Default;

            if (_dirConfigDirs.ContainsKey(dirConfigModule))
                return _dirConfigDirs[dirConfigModule];

            var dirConfig = OsConfig.DirConfigProvider?.Invoke(dirConfigModule) ?? new DefaultDirConfigPlug();

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
        public static ResultMo SetDirConfig<TConfig>(string key, TConfig dirConfig,
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
        public static ResultMo RemoveDirConfig( string key, string moduleName = null)
        {
            return GetDirConfig(moduleName).RemoveDirConfig(key);
        }
    }
}