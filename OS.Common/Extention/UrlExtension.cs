using System;

namespace OS.Http.Extention
{
    public static class UrlExtension
    {
        /// <summary>
        /// Url编码处理
        /// </summary>
        public static string UrlEncode(this string input)
        {
            return Uri.EscapeDataString(input);
        }
        /// <summary>
        /// Url编码处理
        /// </summary>
        public static string UrlEncode(this object input)
        {
            if (input != null)
            {
                return Uri.EscapeDataString(input.ToString());
            }
            return string.Empty;
        }

    }
}
