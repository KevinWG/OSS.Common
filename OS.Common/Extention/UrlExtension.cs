using System;

namespace OS.Common.Extention
{
    /// <summary>
    /// 
    /// </summary>
    public static class UrlExtension
    {
        /// <summary>
        /// Url编码处理
        /// </summary>
        public static string UrlEncode(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            
            return Uri.EscapeDataString(input);
        }

        /// <summary>
        /// Url编码处理
        /// </summary>
        public static string UrlEncode(this object input)
        {
            if (input==null)
                return string.Empty;

            return Uri.EscapeDataString(input.ToString());
        }

        /// <summary>
        /// Url解码处理
        /// </summary>
        public static string UrlDecode(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return Uri.UnescapeDataString(input);
        }

    }
}
