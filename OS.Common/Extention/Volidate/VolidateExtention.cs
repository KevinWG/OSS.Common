using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


#region Copyright (C) 2014 北京金色世纪商旅网络科技股份有限公司

/*
　　	文件功能描述：验证属性扩展方法

　　	创建人：王超
　　	创建人Email：wangchao@jsj.com.cn
    	创建日期：2014.08.25

　　	修改描述：
	*/

#endregion


namespace OS.Common.Extention
{
    public static class VolidateExtention
    {
        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsValidate<T>(this T t) where T : class ,new()
        {
            Type type = typeof(T);
            var files =TypeHelper.GetProperties(type);

            for (int i = 0; i < files.Length; i++)
            {
                var fd = files[i];

                object[] attrs = TypeHelper.GetPropertiAttributes(type.FullName, fd, typeof(BaseValidateAttribute));

                if (attrs.OfType<BaseValidateAttribute>().Any(requireAttr => !requireAttr.Validate(fd.Name, fd.GetValue(t,null))))
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
        public static List<string> ValidateMessage<T>(this T t) where T : class ,new()
        {
            List<string> resultList=new List<string>();

            var type = typeof(T);

            var files = TypeHelper.GetProperties(type);

            foreach (var fd in files)
            {
                var attrs = TypeHelper.GetPropertiAttributes(type.FullName, fd, typeof(BaseValidateAttribute));

                var fd1 = fd;
                foreach (var requireAttr in from requireAttr in attrs.OfType<BaseValidateAttribute>() 
                    let result = requireAttr.Validate(fd1.Name, fd1.GetValue(t,null)) 
                    where !result select requireAttr)
                {
                    resultList.Add(requireAttr.ErrorMessage);
                    break;
                }
            }
            return resultList;
        }

    }



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
            attrs = fd.GetCustomAttributes(attributeType, true);
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
