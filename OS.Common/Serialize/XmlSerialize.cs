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
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Get<T>(string path)
        {
            T t = default(T);
            FileStream fs = null;
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(T));
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
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
            if (t == null) t = (T)Activator.CreateInstance(typeof(T));
            return t;
        }

        /// <summary>
        /// 保存对象到文件,
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="t">对象必须可以被序列化</param>
        /// <returns>true-成功   false-失败（对象是否能被序列化，文件是否有权限）</returns>
        public static bool Save<T>(string path, T t)
        {
            bool isOk = true;
            FileStream fs = null;
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(T));
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);
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
