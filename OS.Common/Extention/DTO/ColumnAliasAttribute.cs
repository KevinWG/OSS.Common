using System;

namespace OS.Common.Extention.DTO
{




    /// <summary>
     /// 别名属性
     /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field
                     | AttributeTargets.Property | AttributeTargets.GenericParameter,
        AllowMultiple = false, Inherited = true)]
    public class ColumnAliasAttribute :Attribute
    {
        private readonly string alias;
        protected bool isMap;
        public ColumnAliasAttribute(string _alias)
        {
            alias = _alias;
            isMap = true;
        }

        public String Alias
        {
            get { return alias; }
        }
        public bool IsMap {
            get { return isMap; }
        }
    }


     /// <summary>
     /// 别名属性
     /// </summary>
    public class IgnoreMap : ColumnAliasAttribute
     {
         
         public IgnoreMap():base(string.Empty)
         {
             isMap = false;
         }

     }

}
