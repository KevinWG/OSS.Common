#region Copyright (C) 2014 OS系列开源项目

/*       
　　	文件功能描述：验证属性attribute

　　	创建人：王超
        创建人Email：1985088337@qq.com
    	创建日期：2014.08.25

　　	修改描述：
*/

#endregion


namespace OSS.Common.Extension
{
    /// <summary>
    /// 字符串数字转化扩展
    /// </summary>
    public static class StringNumberExtension
    {
        #region  字符串数字转化部分

        /// <summary>
        /// 字符串转化成无符号整形
        /// </summary>
        /// <param name="obj">要转化的值</param>
        /// <param name="defaultValue">如果转化失败，返回的默认值</param>
        /// <returns></returns>
        public static uint ToUInt32(this string? obj, uint defaultValue = 0)
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
        public static int ToInt32(this string? obj, int defaultValue = 0)
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
        public static long ToInt64(this string? obj, int defaultValue = 0)
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
        public static decimal ToDecimal(this string? obj, decimal defaultValue = 0)
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
        public static double ToDouble(this string? obj, double defaultValue = 0)
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
        public static float ToFloat(this string? obj, float defaultValue = 0)
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
        
        #endregion
        
    }
}
