#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：xml序列化辅助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com       
*    	创建日期：2017-2-19
*       
*****************************************************************************/

#endregion
using System;
using System.IO;
using System.Xml.Serialization;

namespace OSS.Common.Extension
{
    /// <summary>
    /// xml  转化扩展
    /// </summary>
    public static class XmlConvertExtension
    {
        /// <summary>
        /// 对象序列化成xml字符串
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string SerializeToXml(this object t)
        {
            string result;
            MemoryStream stream = null;
            StreamReader streamReader = null;
            try
            {
                var serializer = new XmlSerializer(t.GetType());

                stream = new MemoryStream();
                serializer.Serialize(stream, t);

                stream.Position = 0;
                streamReader = new StreamReader(stream);
                result = streamReader.ReadToEnd();
            }
            finally
            {
                streamReader?.Dispose();
                stream?.Dispose();
            }
            return result;
        }

        /// <summary>
        /// xml字符串转化到对象
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static TT DeserializeXml<TT>(this string xml)
            where TT : class
        {
            return DeserializeXml(xml, typeof(TT)) as TT;
        }

        /// <summary>
        /// xml字符串转化到对象
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeXml(this string xml, Type type)
        {
            object result;
            MemoryStream stream = null;
            StreamWriter writer = null;
            try
            {
                stream = new MemoryStream();

                writer = new StreamWriter(stream);
                writer.Write(xml);
                writer.Flush();

                stream.Position = 0;
                var deSer = new XmlSerializer(type);
                result = deSer.Deserialize(stream);
            }
            finally
            {
                writer?.Dispose();
                stream?.Dispose();
            }
            return result;
        }
    }
}
