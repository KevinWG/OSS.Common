using System;

namespace OSS.Common.Extention
{
    /// <summary>
    /// url扩展
    /// </summary>
    public static class UrlExtension
    {
        /// <summary>
        /// Url编码处理
        /// </summary>
        public static string UrlEncode(this string input)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty : Uri.EscapeDataString(input);
        }

        /// <summary>
        /// Url编码处理
        /// </summary>
        public static string UrlEncode(this object input)
        {
            return input==null ? string.Empty 
                : Uri.EscapeDataString(input.ToString());
        }

        /// <summary>
        /// Url解码处理
        /// </summary>
        public static string UrlDecode(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : Uri.UnescapeDataString(input);
        }

    }
}
