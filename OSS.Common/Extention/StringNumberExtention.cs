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

namespace OSS.Common.Extention
{
    /// <summary>
    /// 字符串数字转化扩展
    /// </summary>
    public static class StringNumberExtention
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

        #endregion

        #region Filter

        /// <summary>
        /// 过滤 Sql 语句字符串中的注入脚本
        /// </summary>
        /// <param name="source">传入的字符串</param>
        /// <returns>过滤后的字符串</returns>
        [Obsolete]
        public static string SqlFilter(this string source)
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
