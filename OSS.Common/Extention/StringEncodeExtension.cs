#region Copyright (C) 2017  公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：字符串编码扩展功能
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using System.Text;

namespace OSS.Common.Extention
{
    /// <summary>
    /// url扩展
    /// </summary>
    public static class StringEncodeExtension
    {
        #region Url 编码处理

        
        /// <summary>
        /// Url编码处理
        /// </summary>
        public static string UrlEncode(this string input)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty : Uri.EscapeDataString(input);
        }

        /// <summary>
        /// Url解码处理
        /// </summary>
        public static string UrlDecode(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : Uri.UnescapeDataString(input);
        }

        #endregion


        #region Base64编码
        
        /// <summary>
        ///    转换字符串为Base64编码
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToBase64(this string source, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source), "转化Base64字符串不能为空");
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            var bytes = encoding.GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///    解码Base64编码到字符串
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string FromBase64(this string baseString, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(baseString))
            {
                throw new ArgumentNullException(nameof(baseString), "解码Base64字符串不能为空");
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            var bytes = Convert.FromBase64String(baseString);
            return encoding.GetString(bytes);
        }


        /// <summary>
        ///   转换字符串为Base64编码，同时处理（+，/,=）和url中冲突的字符
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToSafeUrlBase64(this string data, Encoding encoding = null)
        {
            return data.ToBase64(encoding).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }


        /// <summary>
        ///    解码Base64编码（特殊url冲突处理过的Base64编码）到字符串
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string FromSafeUrlBase64(this string baseString, Encoding encoding = null)
        {
            var bStr = baseString.Replace('-', '+').Replace('_', '/');
            var len = bStr.Length % 4;
            if (len > 0)
            {
                bStr += "====".Substring(len);
            }
            return bStr.FromBase64(encoding);
        }

        /// <summary>
        ///   替换base64位字符串中的特殊符号 Url友好
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("直接使用ToSafeUrlBase64")]
        public static string Base64UrlSafeEncode(this string data)
        {
            return data.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        /// <summary>
        ///   还原 base64 字符串中的特殊字符  Url不友好
        /// </summary>
        /// <param name="baseString"></param>
        /// <returns></returns>
        [Obsolete("直接使用FromSafeUrlBase64")]
        public static string Base64UrlSafeDecode(this string baseString)
        {
            var bStr= baseString.Replace('-', '+').Replace('_', '/');
            var len = bStr.Length % 4;
            if (len > 0)
            {
                bStr += "====".Substring(len);
            }
            return bStr;
        }
        
        #endregion

    }
}
