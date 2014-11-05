using System;

namespace OS.Common.Extention
{
    public static class ConvertExtention
    {

        #region  int类型转换

        /// <summary>
        /// object转换Int扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(this object obj, Int32 defaultValue=0)
        {
            return obj == null ? defaultValue : obj.ToString().ToInt32();
        }

        /// <summary>
        /// string转Int扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(this string obj, Int32 defaultValue=0)
        {
            return obj.ConvertToType((o, d) =>
            {
                int returnValue;
                return int.TryParse(obj, out returnValue) ? returnValue : defaultValue;
            }, defaultValue);
        }


        #endregion

        #region  long值转换

        /// <summary>
        /// object转换Int64扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Int64 ToInt64( this object obj, Int64 defaultValue=0 )
        {
            return obj == null ? defaultValue : obj.ToString().ToInt64();
        }

        /// <summary>
        /// string转Int64扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int64 ToInt64( this string obj, Int64 defaultValue = 0 )
        {
            return obj.ConvertToType( ( o, d ) =>
            {
                long returnValue;
                return long.TryParse( obj, out returnValue ) ? returnValue : defaultValue;
            }, defaultValue );
        }

        #endregion

        #region  double值转换

        /// <summary>
        /// object转换Int64扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Double ToDouble(this object obj, double defaultValue=0)
        {
            return obj == null ?defaultValue : obj.ToString().ToDouble();
        }

        /// <summary>
        /// string转Int64扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Double ToDouble( this string obj,double defaultValue=0 )
        {
            return obj.ConvertToType((o, d) =>
            {
                Double returnValue;
                return Double.TryParse(obj, out returnValue) ? returnValue : defaultValue;
            },defaultValue);
        }

        #endregion

        #region  DateTime值转换

        /// <summary>
        /// string转DateTime扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this string obj)
        {
            return obj.ConvertToType<DateTime?>((o, d) =>
            {
                DateTime returnValue;
                return DateTime.TryParse(obj, out returnValue) ? returnValue : d;
            });
        }

        /// <summary>
        /// string转DateTime扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime( this string obj, DateTime defaultTime = default(DateTime) )
        {
            return obj.ConvertToType( ( o, d ) =>
            {
                DateTime returnValue;
                return DateTime.TryParse( o, out returnValue ) ? returnValue : d;
            }, defaultTime);
        }
        #endregion

        /// <summary>
        /// 类型转换基本方法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <param name="funcCommand"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static TResult ConvertToType<TResult>(this string obj, Func<string, TResult, TResult> funcCommand, TResult defaultValue=default(TResult))
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            return funcCommand( obj, defaultValue );
        }




        /// <summary>
        /// 转换到指定类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object ChangeToType( this String obj, Type type )
        {
            if (obj == null)
            {
                return default(Type);
            }
          
            return ((object)obj).ChangeToType( type );
        }


        /// <summary>
        /// 转换到指定类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object ChangeToType( this Object obj, Type type )
        {
            if (obj == null)
            {
                return default(Type);
            }

            object result;
            try
            {
                if (type.IsEnum)
                {
                    return Enum.ToObject(type, obj.ToInt64());
                }
                return Convert.ChangeType( obj, type );
            }
            catch (Exception)
            {
                result = default(Type);
            }
            return default(Type);
        }
    }
}
