using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using OS.Common.Helper;

namespace OS.Common.Extention.DTO
{
   public  static class DTOExtetion
    {

       private static ConcurrentDictionary<string,IDictionary<string, PropertyInfo>> propertyDirs
           =new ConcurrentDictionary<string, IDictionary<string, PropertyInfo>>();

       public static void SetToModel<TModel>(this NameValueCollection valueCollection)  where TModel:class ,new()
       {
           TModel t=new TModel();
           var properties = GetColumnAlias(typeof(TModel));
           foreach (var propertyInfo in properties)
           {
               var value = valueCollection[propertyInfo.Key];
               if (!string.IsNullOrEmpty(value))
               {
                   propertyInfo.Value.SetValue(t, value.ChangeToType(propertyInfo.Value.PropertyType), null);
               }
           }
       }


       private static IDictionary<string, PropertyInfo> GetColumnAlias( Type type )
       {
           IDictionary<string, PropertyInfo> dirsDictionary;

           propertyDirs.TryGetValue( type.FullName, out dirsDictionary );
           if (dirsDictionary != null)
           {
               return dirsDictionary;
           }

           dirsDictionary = new Dictionary<string, PropertyInfo>();
           var properties = TypeHelper.GetProperties( type );
           foreach (var fd in properties)
           {
               var attrs = TypeHelper.GetPropertiAttributes( type.FullName, fd, typeof (ColumnAliasAttribute) );

               if (attrs != null && attrs.Length > 0)
               {
                   var aliasAttr = attrs[0] as ColumnAliasAttribute;
                   if (aliasAttr != null && aliasAttr.IsMap)
                   {
                       dirsDictionary.Add( aliasAttr.Alias, fd );
                   }
               }
               else
               {
                   dirsDictionary.Add( fd.Name, fd );
               }
           }
           propertyDirs.TryAdd( type.FullName, dirsDictionary );
           return dirsDictionary;
       }





    }
}
