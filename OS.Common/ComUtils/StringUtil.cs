using System;
using System.Text;

namespace OS.Common.ComUtils
{
    public static class StringUtil
    {
        #region Filter

        /// <summary>
        /// 过滤 Sql 语句字符串中的注入脚本
        /// </summary>
        /// <param name="source">传入的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string SqlFilter(string source)
        {
            source = source.Replace("\"", "");
            source = source.Replace("&", "&amp");
            source = source.Replace("<", "&lt");
            source = source.Replace(">", "&gt");
            source = source.Replace("delete", "");
            source = source.Replace("update", "");
            source = source.Replace("insert", "");
            source = source.Replace("'", "''");
            source = source.Replace(";", "；");
            source = source.Replace("(", "（");
            source = source.Replace(")", "）");
            source = source.Replace("Exec", "");
            source = source.Replace("Execute", "");
            source = source.Replace("xp_", "x p_");
            source = source.Replace("sp_", "s p_");
            source = source.Replace("0x", "0 x");
            return source;
        }

        #endregion

        #region 字符串转码

        /// <summary>
        ///    根据指定编码转化成对应的64位编码
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToBase64(this string source, Encoding encoding)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException("source", "转化Base64字符串不能为空");
            }
            byte[] bytes = encoding.GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///    从base64编码解码出正常的值
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string FromBase64(this string baseString, Encoding encoding)
        {
            if (string.IsNullOrEmpty(baseString))
            {
                throw new ArgumentNullException("baseString", "解码Base64字符串不能为空");
            }
            byte[] bytes = Convert.FromBase64String(baseString);
            return encoding.GetString(bytes);
        }

        /// <summary>
        ///   替换base64位字符串中的特殊符号 Url友好
        /// </summary>
        /// <param name="baseString"></param>
        /// <returns></returns>
        public static string Base64UrlEncode(this string baseString)
        {
            return baseString.Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        ///   还原 base64 字符串中的特殊字符  Url不友好
        /// </summary>
        /// <param name="baseString"></param>
        /// <returns></returns>
        public static string Base64UrlDecode(this string baseString)
        {
            return baseString.Replace('-', '+').Replace( '_','/');
        }

        #endregion
    }
}
