#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局插件 -  配置插件默认实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System;
using System.IO;
using System.Xml.Serialization;
using OSS.Common.ComModels;

namespace OSS.Common.Plugs.DirConfigPlug
{

    /// <summary>
    /// 默认配置处理
    /// </summary>

    [Obsolete("请使用 OSS.Tools.Log 命名空间下 DefaultDirConfigPlug ")]
    public class DefaultDirConfigPlug : IDirConfigPlug
    {
        private static readonly string _defaultPath;

        static DefaultDirConfigPlug()
        {
#if NETFW
            _defaultPath = AppDomain.CurrentDomain.BaseDirectory;
#else
            _defaultPath = AppContext.BaseDirectory;  // totest
#endif
        }


        /// <summary>
        /// 设置字典配置信息
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="key"></param>
        /// <param name="dirConfig"></param>
        /// <returns></returns>
        public ResultMo SetDirConfig<TConfig>(string key, TConfig dirConfig) where TConfig : class, new()
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "配置键值不能为空！");

            if (dirConfig == null)
                throw new ArgumentNullException("dirConfig", "配置信息不能为空！");

            string path = string.Concat(_defaultPath, "\\", "Config");

            ResultMo result;
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fs = new FileStream(string.Concat(path, "\\", key, ".config"), FileMode.Create, FileAccess.Write);

                var type = typeof(TConfig);

                var xmlSer = new XmlSerializer(type);
                xmlSer.Serialize(fs, dirConfig);

                result = new ResultMo();
            }
            finally
            {
                fs?.Dispose();
            }
            return result;
        }


        /// <summary>
        ///   获取字典配置
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TConfig GetDirConfig<TConfig>(string key) where TConfig : class, new()
        {

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "配置键值不能为空！");

            var path = string.Concat(_defaultPath, "\\", "Config");

            var t = default(TConfig);
            FileStream fs = null;
            try
            {
                var fileFullName = string.Concat(path, "\\", key, ".config");

                if (!File.Exists(fileFullName))
                    return t;

                fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read);

                var type = typeof(TConfig);

                var xmlSer = new XmlSerializer(type);
                t = (TConfig)xmlSer.Deserialize(fs);
            }
            finally
            {
                fs?.Dispose();
            }
            return t;
        }

        /// <summary>
        ///  移除配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ResultMo RemoveDirConfig(string key)
        {
            var path = string.Concat(_defaultPath, "\\", "Config");
            var fileName = string.Concat(path, "\\", key, ".config");
            
            if (!File.Exists(fileName))
                return new ResultMo(ResultTypes.InnerError, "移除字典配置时出错");

            File.Delete(fileName);
            return new ResultMo();
        }
    }
}
