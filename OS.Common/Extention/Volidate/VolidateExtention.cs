using System;
using System.Collections.Generic;
using OS.Common.Helper;

namespace OS.Common.Volidate
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

                foreach (var attr in attrs)
                {
                    var requireAttr = attr as BaseValidateAttribute;
                    if (requireAttr != null && !requireAttr.Validate( fd.Name, fd.GetValue( t, null ) ))
                    {
                        return false;
                    }
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

            Type type = typeof(T);

            var files = TypeHelper.GetProperties(type);

            for (int i = 0; i < files.Length; i++)
            {
                var fd = files[i];

                var attrs = TypeHelper.GetPropertiAttributes(type.FullName, fd, typeof(BaseValidateAttribute));

                foreach (var attr in attrs)
                {
                    var requireAttr = attr as BaseValidateAttribute;
                    if (requireAttr!=null)
                    {
                        bool result= requireAttr.Validate(fd.Name, fd.GetValue(t,null));
                        if (!result)
                        {
                            resultList.Add(requireAttr.ErrorMessage);
                            break;
                        }
                    }
                }
            }
            return resultList;
        }

    }
}
