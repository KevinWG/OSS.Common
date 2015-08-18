using System;
using System.Reflection;

namespace OS.Common.Extention.DTO
{
    #region   类型属性的自定义属性处理

    /// <summary>
    /// 别名属性
    /// </summary>
    public class ColumnAliasAttribute : BaseClumnAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        public ColumnAliasAttribute(string alias)
        {
            _alias = alias;
        }
    }


    /// <summary>
    /// 别名属性
    /// </summary>
    public class IgnoreColumnAttribute : BaseClumnAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public IgnoreColumnAttribute()
        {
            _IsIgnore = false;
        }
    }


    /// <summary>
    /// 自增长属性
    /// </summary>
    public class AutoColumnAttribute : BaseClumnAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoColumnAttribute()
        {
            _IsAuto = true;
        }
    }

    /// <summary>
    /// 列属性封装基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field
                    | AttributeTargets.Property | AttributeTargets.GenericParameter,
        AllowMultiple = false, Inherited = true)]
    public class BaseClumnAttribute : Attribute
    {
        protected string _alias;

        /// <summary>
        /// 列简称
        /// </summary>
        public String Alias
        {
            get { return _alias; }
        }

        protected bool _IsAuto;

        /// <summary>
        /// 是否是自增长列
        /// </summary>
        public bool IsAuto
        {
            get { return _IsAuto; }
        }

        protected bool _IsIgnore;

        /// <summary>
        /// 是否忽略当前列
        /// </summary>
        public bool IsIgnore
        {
            get { return _IsIgnore; }
        }
    }



    /// <summary>
    ///   列属性信息
    /// </summary>
    public class PropertyColumnInfo
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 是否自增长
        /// </summary>
        public bool IsAuto { get; set; }

        /// <summary>
        ///   是否忽略当前列
        /// </summary>
        public bool IsIgnore { get; set; }

        /// <summary>
        /// 列属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
    }

    #endregion
}
