using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OSS.Common.Extention
{
    /// <summary>
    /// 枚举扩展方法类
    /// </summary>
    public static class EnumExtention
    {

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="en">枚举对象</param>
        /// <param name="isFlag">是否二进制取和</param>
        /// <param name="operate">多个描述分隔符</param>
        /// <returns></returns>
        public static string GetDesp(this Enum en, bool isFlag = false, string operate = ",")
        {
            var values = en.GetType().ToEnumDirs();
            var current = (int) Enum.Parse(en.GetType(), en.ToString());
            
            if (isFlag)
            {
                var strResult = new StringBuilder();

                foreach (var value in values)
                {
                    var tempKey = value.Key.ToInt32();
                    if ((tempKey & current) != tempKey) continue;

                    if (strResult.Length != 0)
                    {
                        strResult.Append(operate);
                    }
                    strResult.Append(value.Value);
                }
                return strResult.ToString();
            }

            var keypair = values.FirstOrDefault(e => e.Key == current.ToString());
            return keypair.Key == null ? "不存在的枚举值" : keypair.Value;
        }
        
        private static ConcurrentDictionary<string, Dictionary<string, string>> enumDirs
           =new ConcurrentDictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 获取枚举字典列表
        /// </summary>
        /// <param name="enType">枚举类型</param>
        /// <param name="isIntValue">返回枚举值是否是int类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToEnumDirs(this Type enType, bool isIntValue = true)
        {
#if NETFW
            if (!enType.IsEnum)
#else
            if (!enType.GetTypeInfo().IsEnum)
#endif
                throw new ArgumentException("获取枚举字典，参数必须是枚举类型！");
            
            var key = string.Concat(enType.FullName, isIntValue);
            enumDirs.TryGetValue(key, out var dirs);

            if (dirs != null)
                return dirs.Copy();

            dirs = new Dictionary<string, string>();
            var values = Enum.GetValues(enType);

            foreach (var value in values)
            {
                var name = Enum.GetName(enType, value);
                var resultValue = isIntValue ? ((int) value).ToString() : value.ToString();
#if NETFW
                var attr = enType.GetField(name)?.GetCustomAttribute<OSDescriptAttribute>();
#else
                var attr = enType.GetTypeInfo().GetDeclaredField(name)?.GetCustomAttribute<OSDescriptAttribute>();
#endif
                dirs.Add(resultValue, attr == null ? name : attr.Description);
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
            return source.ToDictionary(sou => sou.Key, sou => sou.Value);
        }
    }

    /// <summary>
    ///   自定义描述属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class OSDescriptAttribute : Attribute
    {
        public OSDescriptAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        ///   描述信息
        /// </summary>
        public string Description { get; set; }
    }


}
