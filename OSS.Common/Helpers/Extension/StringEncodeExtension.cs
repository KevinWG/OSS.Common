﻿#region Copyright (C) 2017  公众号：OSSCore

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

namespace OSS.Common.Extension
{
    /// <summary>
    /// url扩展
    /// </summary>
    public static class StringEncodeExtension
    {
        #region Url 编码处理

        /// <summary>
        /// Uri编码处理（协议部分字符不会被编码）
        /// </summary>
        public static string SafeEscapeUriString(this string input)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty
                : Uri.EscapeUriString(input);
        }

        /// <summary>
        /// Uri数据编码处理(协议部分字符将被编码，如 http:// 会被转化)
        /// </summary>
        public static string SafeEscapeUriDataString(this string input)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty
                : Uri.EscapeDataString(input);
        }

        /// <summary>
        /// Uri解码处理
        /// </summary>
        public static string SafeUnescapeDataString(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : Uri.UnescapeDataString(input);
        }


        [Obsolete]
        public static string SafeUnescapeUriString(this string input)
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
        ///   转换字符串为Base64编码，同时处理和url中冲突的字符（+，/,=）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToSafeUrlBase64(this string data, Encoding encoding = null)
        {
            return data.ToBase64(encoding).ReplaceBase64ToUrlSafe();
        }


        /// <summary>
        ///    解码Base64编码（特殊url冲突处理过的Base64编码）到字符串
        /// </summary>
        /// <param name="safeBaseString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string FromSafeUrlBase64(this string safeBaseString, Encoding encoding = null)
        {
            return safeBaseString.ReplaceBase64UrlSafeBack().FromBase64(encoding);
        }

        /// <summary>
        ///   处理Base64字符串和url中冲突的字符（+，/,=）
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static string ReplaceBase64ToUrlSafe(this string base64String)
        {
            return base64String.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        /// <summary>
        ///   将url冲突处理过的Base64编码替换回正常Base64
        /// </summary>
        /// <param name="safeBase64String"></param>
        /// <returns></returns>
        public static string ReplaceBase64UrlSafeBack(this string safeBase64String)
        {
            var bStr = safeBase64String.Replace('-', '+').Replace('_', '/');
            var len  = bStr.Length % 4;
            if (len > 0)
            {
                bStr += "====".Substring(len);
            }
            return bStr;
        }

        #endregion

    }
}
