#region Copyright (C) 2014 OS系列开源项目

/*       
　　	文件功能描述：验证属性attribute

　　	创建人：王超
        创建人Email：1985088337@qq.com
    	创建日期：2014.08.25

　　	修改描述：
*/

#endregion
using System;
using System.Text;

namespace OSS.Common.Extention
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConvertExtention
    {
        #region  字符串数字转化部分
        
        /// <summary>
        /// 字符串转化成无符号整形
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static uint ToUInt32(this string obj, uint defaultValue = 0)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            try
            {
                if (uint.TryParse(obj, out var returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static int ToInt32(this string obj, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            try
            {
                if (int.TryParse(obj, out var returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static int ToInt32(this object obj, int defaultValue = 0)
        {
            return obj?.ToString().ToInt32() ?? defaultValue;
        }




        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static long ToInt64(this string obj, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            try
            {
                if (long.TryParse(obj, out var returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return defaultValue;
        }



        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string obj, decimal defaultValue = 0)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            try
            {
                if (decimal.TryParse(obj, out var returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue = 0)
        {
            return obj?.ToString().ToInt32() ?? defaultValue;
        }
        
        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this string obj, double defaultValue = 0)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            try
            {
                if (double.TryParse(obj, out var returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double defaultValue = 0)
        {
            return obj?.ToString().ToDouble() ?? defaultValue;
        }


        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static float ToFloat(this string obj, float defaultValue = 0)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            try
            {
                if (float.TryParse(obj, out var returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float defaultValue = 0)
        {
            return obj?.ToString().ToFloat() ?? defaultValue;
        }

        /// <summary>
        /// 字符串转化成时间
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return null;
            }
            return DateTime.TryParse(obj, out var date) ? date : default(DateTime?);
        }

        /// <summary>
        /// 转化成布尔类型
        /// </summary>
        /// <returns></returns>
        public static bool ToBoolean(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            bool.TryParse(str, out var isOkay);
            return isOkay;
        }



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
                throw new ArgumentNullException(nameof(source), "转化Base64字符串不能为空");
            }
            var bytes = encoding.GetBytes(source);
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
                throw new ArgumentNullException(nameof(baseString), "解码Base64字符串不能为空");
            }
            var bytes = Convert.FromBase64String(baseString);
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
            return baseString.Replace('-', '+').Replace('_', '/');
        }
        
        #endregion

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
    }
}
