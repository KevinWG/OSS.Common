using System;

namespace OS.Common.Extention
{
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
        #endregion
    }
}
