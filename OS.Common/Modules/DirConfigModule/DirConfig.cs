using System;
using System.IO;
using System.Xml.Serialization;
using OS.Common.ComModels;
using OS.Common.ComModels.Enums;
using OS.Common.Modules.LogModule;

namespace OS.Common.Modules.DirConfigModule
{
    /// <summary>
    /// 默认配置处理
    /// </summary>
    public class DirConfig : IDirConfig
    {

        private static readonly string _defaultPath;

        static DirConfig()
        {

#if DNXCORE50
            _defaultPath = AppDomain.CurrentDomain.BaseDirectory;
#else
            _defaultPath = AppDomain.CurrentDomain.BaseDirectory;
#endif
        }


        /// <summary>
        /// 设置字典配置信息
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="key"></param>
        /// <param name="dirConfig"></param>
        /// <returns></returns>
        public ResultModel SetDirConfig<TConfig>( string key, TConfig dirConfig) where TConfig:class ,new()
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "配置键值不能为空！");

            if (dirConfig == null)
                throw new ArgumentNullException("dirConfig", "配置信息不能为空！");

            string path = string.Concat(_defaultPath, "\\", "Config");

            ResultModel result;
            FileStream fs = null;
            try
            {

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fs = new FileStream(string.Concat(path, "\\", key, ".config"), FileMode.Create, FileAccess.Write);

                var type = typeof (TConfig);
           
                    XmlSerializer xmlSer = new XmlSerializer(type);
                    xmlSer.Serialize(fs, dirConfig);
                
                fs.Close();
                result = new ResultModel();
            }
            catch (Exception ex)
            {
#if DEBUG
                  throw ex;
#endif
                LogUtil.Error(string.Format("错误描述：{0}    详情：{1}", ex.Message, ex.StackTrace));
                result = new ResultModel(ResultTypes.InnerError, "设置字典配置信息出错");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return result;
        }


        /// <summary>
        ///   获取字典配置
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TConfig GetDirConfig<TConfig>(string key) where TConfig : class ,new()
        {

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "配置键值不能为空！");

            string path = string.Concat(_defaultPath, "\\", "Config");

            TConfig t = default(TConfig);
            FileStream fs = null;
            try
            {
                string fileFullName=string.Concat(path, "\\", key, ".config");
                if (!File.Exists(fileFullName))                
                    return t;
                
                fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read,FileShare.Read);

                var type = typeof (TConfig);
              
                    XmlSerializer xmlSer = new XmlSerializer(type);
                    t = (TConfig) xmlSer.Deserialize(fs);
               
                fs.Close();

            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
                LogUtil.Error(string.Format("错误描述：{0}    详情：{1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return t;
        }

        /// <summary>
        ///  移除配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ResultModel RemoveDirConfig( string key)
        {
            string path = string.Concat(_defaultPath, "\\", "Config");
            string fileName = string.Concat(path, "\\", key, ".config");

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                  throw ex;
#endif
                LogUtil.Error(string.Format("错误描述：{0}    详情：{1}",ex.Message,ex.StackTrace));
            }
            return new ResultModel(ResultTypes.InnerError, "移除字典配置时出错");
        }
    }
}
