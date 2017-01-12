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
using System.Collections.Generic;
using System.Text;

namespace OS.Common.Extention
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConvertExtention
    {
        #region DataReader转化部分
        ///// <summary>
        ///// 获取布尔类型
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A boolean value</returns>
        //public static bool GetBoolean(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return false;
        //    }
        //    return Convert.ToBoolean(rdr[index]);
        //}

        ///// <summary>
        ///// 获取字节流类型
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A byte array</returns>
        //public static byte[] GetBytes(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return null;
        //    }
        //    return (byte[])rdr[index];
        //}

        ///// <summary>
        ///// 获取时间格式对象，如果不存在返回最小时间
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A date time</returns>
        //public static DateTime GetDateTime(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return DateTime.MinValue;
        //    }
        //    return Convert.ToDateTime(rdr[index]);
        //}


        ///// <summary>
        ///// 获取UTC时间，如果不存在则返回最小时间
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A date time</returns>
        //public static DateTime GetUtcDateTime(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return DateTime.MinValue;
        //    }
        //    return DateTime.SpecifyKind((DateTime)rdr[index], DateTimeKind.Utc);
        //}

        ///// <summary>
        ///// 获取时间格式对象（可空）
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A date time if exists; otherwise, null</returns>
        //public static DateTime? GetNullableDateTime(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return  default(DateTime?);
        //    }
        //    return Convert.ToDateTime(rdr[index]);
        //}

        ///// <summary>
        ///// 获取UTC时间，可空
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A date time if exists; otherwise, null</returns>
        //public static DateTime? GetNullableUtcDateTime(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return default(DateTime?);
        //    }
        //    return DateTime.SpecifyKind((DateTime)rdr[index], DateTimeKind.Utc);
        //}

        ///// <summary>
        ///// 返回十进制类型
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A decimal value</returns>
        //public static decimal GetDecimal(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return decimal.Zero;
        //    }
        //    return Convert.ToDecimal(rdr[index]);
        //}

        ///// <summary>
        ///// 返回双精度类型
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A double value</returns>
        //public static double GetDouble(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return 0.0;
        //    }
        //    return Convert.ToDouble(rdr[index]);
        //}

        ///// <summary>
        ///// 获取GUID对象
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A GUID value</returns>
        //public static Guid GetGuid(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return Guid.Empty;
        //    }
        //    return (Guid)rdr[index];
        //}

        ///// <summary>
        ///// 获取int对象
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>An integer value</returns>
        //public static int GetInt32(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return 0;
        //    }
        //    return rdr[index].ToInt32(  );
        //}

        ///// <summary>
        ///// 得到可以为null的int型数据 ?号表示可以为空值类型，这是C#的特性，表示Nullable类型结构
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A nullable integer value</returns>
        //public static int? GetNullableInt(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return default(int?);
        //    }
        //    return rdr[index].ToInt32(  );
        //}

        ///// <summary>
        ///// 获取string类型对象
        ///// </summary>
        ///// <param name="rdr">Data reader</param>
        ///// <param name="columnName">Column name</param>
        ///// <returns>A string value</returns>
        //public static string GetString(this IDataReader rdr, string columnName)
        //{
        //    int index = rdr.GetOrdinal(columnName);
        //    if (rdr.IsDBNull(index))
        //    {
        //        return null;
        //    }
        //    return Convert.ToString(rdr[index]);
        //}
        #endregion

        #region  字符串数字转化部分



        /// <summary>
        /// 字符串转化成无符号整形
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static UInt32 ToUInt32(this string obj, UInt32 defaultValue = 0)
        {
            try
            {
                UInt32 returnValue = 0;
                if (UInt32.TryParse(obj, out returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {

            }
            return defaultValue;
        }


        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static int ToInt32( this string obj ,int defaultValue=0)
        {
            try
            {
                int returnValue = 0;
                if (int.TryParse(obj, out returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {
               
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static int ToInt32(this object obj, int defaultValue=0)
        {
            if (obj==null)
            {
                return defaultValue;
            }
            return obj.ToString().ToInt32(  );
        }




        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static long ToInt64(this string obj, int defaultValue=0)
        {
            try
            {
                long returnValue = 0;
                if (long.TryParse(obj, out returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {

            }
            return defaultValue;
        }



        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string obj, decimal defaultValue=0)
        {
            try
            {
                decimal returnValue = 0;
                if (decimal.TryParse(obj, out returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {

            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue=0)
        {
            if (obj==null)
            {
                return defaultValue;
            }
            return obj.ToString().ToInt32();
        }





        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this string obj, double defaultValue = 0)
        {
            try
            {
                double returnValue = 0;
                if (double.TryParse(obj, out returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {

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
            if (obj == null)
            {
                return defaultValue;
            }
            return obj.ToString().ToDouble();
        }


        /// <summary>
        /// 字符串转化成数字
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static float ToFloat(this string obj, float defaultValue = 0)
        {
            try
            {
                float returnValue = 0;
                if (float.TryParse(obj, out returnValue))
                {
                    return returnValue;
                }
            }
            catch (Exception)
            {

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
            if (obj == null)
            {
                return defaultValue;
            }
            return obj.ToString().ToFloat();
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
            DateTime date;
            return DateTime.TryParse(obj, out date) ? date : default(DateTime?);
        }

        /// <summary>
        /// 转化成布尔类型
        /// </summary>
        /// <returns></returns>
        public static bool ToBoolean(this string str)
        {
            bool isOkay = false;
            Boolean.TryParse(str, out isOkay);
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

        

       /// <summary>
       /// 获取字典的值
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dirs"></param>
       /// <param name="key"></param>
       /// <returns></returns>
        public static T GetValue<T>(this IDictionary<string, T> dirs, string key)
        { 
            T t;
            dirs.TryGetValue(key, out t);
            return t;
        }
    }
}
