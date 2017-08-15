using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#region Copyright (C) 2014 OS系列开源项目

/*       
　　	文件功能描述：验证属性attribute

　　	创建人：王超
        创建人Email：1985088337@qq.com
    	创建日期：2014.08.25

　　	修改描述：
*/

#endregion


namespace OSS.Common.Extention.Volidate
{
    /// <summary>
    /// 
    /// </summary>
    public static class VolidateExtention
    {
        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsOsValidate<T>(this T t) where T : class, new()
        {
            if (t == null)
                return false;


            var type = typeof (T);
            var files = TypeHelper.GetProperties(type);

            foreach (var fd in files)
            {
                var attrs = TypeHelper.GetPropertiAttributes(type.FullName, fd, typeof (BaseValidateAttribute));

                var fd1 = fd;
                if (
                    attrs.OfType<BaseValidateAttribute>()
                        .Any(requireAttr => !requireAttr.Validate(fd1.Name, fd1.GetValue(t, null))))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 返回的验证错误信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<string> ValidateOsMessage<T>(this T t) where T : class, new()
        {
            List<string> resultList = new List<string>();
            if (t == null)
            {
                resultList.Add("对象不能为空！");
                return resultList;
            }

            var type = typeof (T);

            var files = TypeHelper.GetProperties(type);

            foreach (var fd in files)
            {
                var attrs = TypeHelper.GetPropertiAttributes(type.FullName, fd, typeof (BaseValidateAttribute));

                var fd1 = fd;
                foreach (var requireAttr in from requireAttr in attrs.OfType<BaseValidateAttribute>()
                    let result = requireAttr.Validate(fd1.Name, fd1.GetValue(t, null))
                    where !result
                    select requireAttr)
                {
                    resultList.Add(requireAttr.ErrorMessage);
                    break;
                }
            }
            return resultList;
        }

    }


    /// <summary>
    /// 
    /// </summary>
    public static class TypeHelper
    {
        private static ConcurrentDictionary<string, object[]> attrDirs = new ConcurrentDictionary<string, object[]>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="fd"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static object[] GetPropertiAttributes(string typeName, PropertyInfo fd, Type attributeType)
        {
            string key = string.Concat(typeName, fd.Name);

            object[] attrs;

            attrDirs.TryGetValue(key, out attrs);
            if (attrs != null)
            {
                return attrs;
            }
            attrs = fd.GetCustomAttributes(attributeType, true).ToArray();
            attrDirs.TryAdd(key, attrs);
            return attrs;
        }

        /// <summary>
        /// 
        /// </summary>
        private static ConcurrentDictionary<string, PropertyInfo[]> proDictionaries =
            new ConcurrentDictionary<string, PropertyInfo[]>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            PropertyInfo[] properties;
            proDictionaries.TryGetValue(type.FullName, out properties);
            if (properties != null)
            {
                return properties;
            }
            properties = type.GetProperties();

            proDictionaries.TryAdd(type.FullName, properties);
            return properties;
        }

    }
}
