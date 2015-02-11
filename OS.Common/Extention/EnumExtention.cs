using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OS.Common.Extention
{
    public static class EnumExtention
    {

        ///// <summary>
        ///// 获取描述
        ///// </summary>
        ///// <param name="en"></param>
        ///// <returns></returns>
        //public static string GetDesp(this Enum en,bool isFlag=false)
        //{
        //    Type enType = en.GetType();

        //    var field = enType.GetField(en.ToString());
        //    if (field==null)
        //    {
        //        return "不存在的枚举值";
        //    }
        //    var attrList =field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        //    if (attrList.Length > 0)
        //    {
        //        return attrList[0].Description;
        //    }
        //    return en.ToString();
        //}


        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDesp(this Enum en, bool isFlag=false,string operate=",")
        {
            var values = en.GetType().ToEnumDirs();
            int current = (int)Enum.Parse(en.GetType(),en.ToString());


            if (isFlag)
            {
                StringBuilder strResult=new StringBuilder();

                foreach (var value in values)
                {
                    int tempKey = value.Key.ToInt32();
                    if ((tempKey&current)==tempKey)
                    {
                        if (strResult.Length!=0)
                        {
                            strResult.Append(operate);
                        }
                        strResult.Append(value.Value);
                    }
                }
                return strResult.ToString();
            }
            else
            {
                KeyValuePair<string, string> keypair = values.FirstOrDefault(e => e.Key == current.ToString());
                return keypair.Key==null ? "不存在的枚举值" : keypair.Value;
            }
        }



        private static ConcurrentDictionary<string, Dictionary<string, string>> enumDirs
           =new ConcurrentDictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 获取枚举字典列表
        /// </summary>
        /// <param name="enType">枚举类型</param>
        /// <param name="isIntValue">返回枚举值是否是int类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToEnumDirs(this Type enType,bool isIntValue=true)
        {
            if (!enType.IsEnum)
            {
               throw new ArgumentException("获取枚举字典，参数必须是枚举类型！");
            }
            string key = string.Concat(enType.FullName, isIntValue);
             Dictionary<string, string> dirs;
             enumDirs.TryGetValue(key, out dirs);

             if (dirs != null)
                 return dirs.Copy() ;

            dirs=new Dictionary<string, string>();

            var values = Enum.GetValues(enType);
    
            foreach (var value in values)
            {
                var name = Enum.GetName(enType, value);
                string resultValue = isIntValue ? ((int) value).ToString() : value.ToString();
                var attrList = enType.GetField(name).GetCustomAttributes(typeof (DescriptionAttribute), false) as DescriptionAttribute[];
                if (attrList.Length > 0)
                {
                    dirs.Add(resultValue, attrList[0].Description);
                    continue;
                }
                dirs.Add(resultValue, name);
            }

            enumDirs.TryAdd(key, dirs);
            return dirs.Copy();
        }


        /// <summary>
        ///   拷贝字典
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static Dictionary<string, string> Copy(this Dictionary<string, string> source)
        {
            Dictionary<string, string> results=new Dictionary<string, string>();
            foreach (var sou in source)
            {
                results.Add(sou.Key,sou.Value);
            }

            return results;
        }
    }
}
