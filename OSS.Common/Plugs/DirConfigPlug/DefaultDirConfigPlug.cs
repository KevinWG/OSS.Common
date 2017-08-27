#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

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
using OSS.Common.ComModels.Enums;
using OSS.Common.Plugs.LogPlug;

namespace OSS.Common.Plugs.DirConfigPlug
{

//    /// <summary>
//    /// 默认配置处理
//    /// </summary>
//    public class DefaultDirConfigPlug : IDirConfigPlug
//    {
//        private static readonly string _defaultPath;

//        static DefaultDirConfigPlug()
//        {
//#if NETFW
//            _defaultPath = AppDomain.CurrentDomain.BaseDirectory;
//#else
//            _defaultPath = AppContext.BaseDirectory;  // totest
//#endif
//        }


//        /// <summary>
//        /// 设置字典配置信息
//        /// </summary>
//        /// <typeparam name="TConfig"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="dirConfig"></param>
//        /// <returns></returns>
//        public ResultMo SetDirConfig<TConfig>(string key, TConfig dirConfig) where TConfig : class, new()
//        {
//            if (string.IsNullOrEmpty(key))
//                throw new ArgumentNullException("key", "配置键值不能为空！");

//            if (dirConfig == null)
//                throw new ArgumentNullException("dirConfig", "配置信息不能为空！");

//            string path = string.Concat(_defaultPath, "\\", "Config");

//            ResultMo result;
//            FileStream fs = null;
//            try
//            {
//                if (!Directory.Exists(path))
//                {
//                    Directory.CreateDirectory(path);
//                }
//                fs = new FileStream(string.Concat(path, "\\", key, ".config"), FileMode.Create, FileAccess.Write);

//                var type = typeof(TConfig);

//                XmlSerializer xmlSer = new XmlSerializer(type);
//                xmlSer.Serialize(fs, dirConfig);

//                result = new ResultMo();
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                throw ex;
//#endif
//                LogUtil.Error(string.Format("错误描述：{0}    详情：{1}", ex.Message, ex.StackTrace));
//                result = new ResultMo(ResultTypes.InnerError, "设置字典配置信息出错");
//            }
//            finally
//            {
//                if (fs != null)
//                    fs.Dispose();
//            }
//            return result;
//        }


//        /// <summary>
//        ///   获取字典配置
//        /// </summary>
//        /// <typeparam name="TConfig"></typeparam>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public TConfig GetDirConfig<TConfig>(string key) where TConfig : class, new()
//        {

//            if (string.IsNullOrEmpty(key))
//                throw new ArgumentNullException("key", "配置键值不能为空！");

//            string path = string.Concat(_defaultPath, "\\", "Config");

//            TConfig t = default(TConfig);
//            FileStream fs = null;
//            try
//            {
//                string fileFullName = string.Concat(path, "\\", key, ".config");

//                if (!File.Exists(fileFullName))
//                    return t;

//                fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read);

//                var type = typeof(TConfig);

//                XmlSerializer xmlSer = new XmlSerializer(type);
//                t = (TConfig)xmlSer.Deserialize(fs);
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                throw ex;
//#endif
//                LogUtil.Error(string.Format("错误描述：{0}    详情：{1}", ex.Message, ex.StackTrace));
//            }
//            finally
//            {
//                if (fs != null)
//                    fs.Dispose();
//            }

//            return t;
//        }

//        /// <summary>
//        ///  移除配置信息
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public ResultMo RemoveDirConfig(string key)
//        {
//            string path = string.Concat(_defaultPath, "\\", "Config");
//            string fileName = string.Concat(path, "\\", key, ".config");

//            try
//            {
//                if (File.Exists(fileName))
//                {
//                    File.Delete(fileName);
//                }
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                  throw ex;
//#endif
//                LogUtil.Error(string.Format("错误描述：{0}    详情：{1}", ex.Message, ex.StackTrace));
//            }
//            return new ResultMo(ResultTypes.InnerError, "移除字典配置时出错");
//        }
//    }
}
