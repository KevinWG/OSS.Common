using System;
using System.IO;
using System.Xml.Serialization;

namespace OS.Common.Serialize
{
    public static class XmlSerialize
    {
        /// <summary>
        /// 从文件中获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">文件夹路径 - 不要带文件名,不带后缀（\\）</param>
        /// <param name="fileName">等于空则为 类型名称  </param>
        /// <returns></returns>
        public static T Get<T>(string path=null, string fileName=null) where T : class ,new()
        {
            if (string.IsNullOrEmpty(path))
            {
                path =string.Concat(Environment.CurrentDirectory,"\\","Config");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = typeof(T).Name;
            }

            T t = default(T);
            FileStream fs = null;
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(T));
                fs = new FileStream(string.Concat(path, "\\", fileName, ".xml"), FileMode.Open, FileAccess.Read, FileShare.Read);
                t = (T)xmlSer.Deserialize(fs);
            }
            catch
            {
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            if (t == null) 
                t = new T();
            return t;
        }

        /// <summary>
        /// 保存对象到指定目录下 指定文件名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">对象必须可以被序列化</param>
        /// <param name="path">文件夹路径 - 不要带文件名,不带后缀（\\）</param>
        /// <param name="fileName">等于空则为 类型名称  </param>
        /// <returns>true-成功   false-失败（对象是否能被序列化，文件是否有权限）</returns>
        public static bool Save<T>(this  T t,string path=null,string fileName=null) where T : class ,new()
        {
            if (string.IsNullOrEmpty( path ))
            {
                path =string.Concat(Environment.CurrentDirectory, "\\", "Config");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = typeof(T).Name;
            }
            bool isOk = true;
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory( path );
                }

                XmlSerializer xmlSer = new XmlSerializer(typeof(T));
                fs = new FileStream(string.Concat(path,"\\", fileName, ".xml"), FileMode.Create, FileAccess.Write);
                xmlSer.Serialize(fs, t);
                fs.Close();
            }
            catch
            {
                isOk = false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return isOk;
        }
    }
}
