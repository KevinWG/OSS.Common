using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

#region Copyright (C) 2014 北京金色世纪商旅网络科技股份有限公司

/*
　　	文件功能描述：实体映射赋值

　　	创建人：王超
　　	创建人Email：wangchao@jsj.com.cn
    	创建日期：2014.08.25

　　	修改描述：
	*/

#endregion

namespace OS.Common.Extention
{
   public  static class DTOExtetion
    {

       private static ConcurrentDictionary<string, IDictionary<string,PropertyColumnInfo>> propertyDirs
           =new ConcurrentDictionary<string, IDictionary<string, PropertyColumnInfo>>();

       /// <summary>
       /// 从Rquest等NameValueCollection列表对象中获取实体信息
       /// </summary>
       /// <typeparam name="TModel"></typeparam>
       /// <param name="valueCollection"></param>
       public static TModel SetToModel<TModel>(this NameValueCollection valueCollection) where TModel : class, new()
       {
           TModel tModel = new TModel();
           var properties = GetColumnAttributes( typeof (TModel) );
           foreach (var propertyColumnInfo in properties)
           {
               var propertyInfo = propertyColumnInfo.Value.PropertyInfo;
               var value = valueCollection[propertyColumnInfo.Key];

               if (!propertyColumnInfo.Value.IsIgnore
                   &&!string.IsNullOrEmpty( value ))
               {
                   propertyInfo.SetValue(tModel, value.ChangeToType(propertyInfo.PropertyType), null);
               }
           }
           return tModel;
       }

       ///// <summary>
       ///// 从DataReader中获取实体信息
       ///// </summary>
       ///// <typeparam name="TModel"></typeparam>
       ///// <param name="dr"></param>
       //public static TModel SetToModel<TModel>(this IDataReader dr) where TModel : class ,new()
       //{
       //    TModel tModel=new TModel();

       //    var properties = GetColumnAttributes( typeof (TModel) );

       //    for (int i = 0; i < dr.FieldCount; i++)
       //    {
       //        string drFieldName = dr.GetName( i );

       //        if (properties.ContainsKey( drFieldName ) && !dr.IsDBNull( i ))
       //        {
       //            var propertyColumnInfo = properties[drFieldName];
       //            if (!propertyColumnInfo.IsIgnore)
       //            {
       //                var propertyInfo = propertyColumnInfo.PropertyInfo;
       //                propertyInfo.SetValue(tModel, dr[i].ChangeToType(propertyInfo.PropertyType), null);
       //            }
       //        }
       //    }
       //    return tModel;
       //}

       #region   属性处理

       /// <summary>
       /// 获取列附加属性列表
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
       private static IDictionary<string, PropertyColumnInfo> GetColumnAttributes(Type type)
       {
           IDictionary<string, PropertyColumnInfo> propertyList;

           propertyDirs.TryGetValue(type.FullName, out propertyList);
           if (propertyList != null)
               return propertyList;

           var properties = TypeHelper.GetProperties(type);
           propertyList = new Dictionary<string, PropertyColumnInfo>(properties.Length);

           foreach (PropertyInfo fd in properties)
           {
               var properColumnInfo = new PropertyColumnInfo();

               properColumnInfo.Name = fd.Name;
               properColumnInfo.PropertyInfo = fd;

               var attrs = TypeHelper.GetPropertiAttributes(type.FullName, fd, typeof(BaseClumnAttribute))
                   .Select(attr => attr as BaseClumnAttribute)
                   .ToList();

               attrs.ForEach(attr =>
               {
                   properColumnInfo.IsAuto = attr.IsAuto || properColumnInfo.IsAuto;
                   properColumnInfo.IsIgnore = attr.IsIgnore || properColumnInfo.IsIgnore;

                   if (!string.IsNullOrEmpty(attr.Alias))
                       properColumnInfo.Alias = attr.Alias;
               });

               properColumnInfo.Alias = properColumnInfo.Alias ?? fd.Name;
               if (!propertyList.ContainsKey(properColumnInfo.Alias))
               {
                   propertyList.Add(properColumnInfo.Alias, properColumnInfo);
               }
            
           }
           propertyDirs.TryAdd(type.FullName, propertyList);
           return propertyList;
       }
       #endregion

       #region  数据转化部分
       /// <summary>
       /// 转换到指定类型
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public static Object ChangeToType(this String obj, Type type)
       {
           if (obj == null)
           {
               return default(Type);
           }
           return ((object)obj).ChangeToType(type);
       }

       /// <summary>
       /// 转换到指定类型
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public static Object ChangeToType(this Object obj, Type type)
       {
           if (obj == null)
           {
               return default(Type);
           }
           object result;
           try
           {
               if (type == typeof (DateTime?))
               {
                   DateTime date;
                   result = DateTime.TryParse(obj.ToString(), out date) ? date : default(DateTime?);
               }
               else if (type == typeof(int?))
               {
                   int i;
                   result = int.TryParse(obj.ToString(), out i) ? i : default(int?);
               }
               else
               {
                   result = type.IsEnum ? Enum.ToObject(type, Convert.ToInt64(obj)) : Convert.ChangeType(obj, type);
               }
           }
           catch (Exception)
           {
               result = default(Type);
           }
           return result;
       }

       #endregion

       #region  创建数据参数部分
      
       ///// <summary>
       /////   从实体中获取  所有数据库参数和值
       ///// </summary>
       ///// <param name="tModel"></param>
       ///// <param name="type"></param>
       //public static DbParameters GetDbParameteres<TModel>(this TModel tModel, DataBaseType type=DataBaseType.SqlServer) where TModel : class 
       //{
       //    DbParameters parameters=new DbParameters(type);
       //    var properties = GetColumnAttributes(typeof(TModel));
       //    foreach (var pro in properties)
       //    {
       //        var pcInfo = pro.Value;
       //        if (!pcInfo.IsIgnore && !pcInfo.IsAuto)
       //        {
       //            parameters.AddInParameter(pcInfo.Alias,
       //                 GetPropertityValue(pcInfo.PropertyInfo, tModel));
       //        }
       //        else if (pcInfo.IsAuto)
       //        {
       //            parameters.AddOutParameter(pcInfo.Alias,DbType.Int64);
       //        }
       //    }
       //    return parameters;
       //}


       ///// <summary>
       /////   设置SqlServer数据库插入参数和语句
       ///// </summary>
       ///// <param name="tModel"></param>
       ///// <param name="tableName"></param>
       //public static void SetSqlServerInsertPara<TModel>(this DbCommand com, TModel tModel, string tableName)
       //{
       //    DbParameters parameters = new DbParameters(DataBaseType.SqlServer);
       //    var properties = GetColumnAttributes(typeof (TModel));

       //    StringBuilder strColumns = new StringBuilder();
       //    StringBuilder strValues = new StringBuilder();
       //    string strAuto = string.Empty;

       //    foreach (var pro in properties)
       //    {
       //        var pcInfo = pro.Value;
       //        if (!pcInfo.IsIgnore && !pcInfo.IsAuto)
       //        {
       //            var para = parameters.AddInParameter(pcInfo.Alias,
       //                DbParameters.ConvertToDbType(pcInfo.PropertyInfo.PropertyType),
       //                GetPropertityValue(pcInfo.PropertyInfo, tModel));

       //            if (strColumns.Length > 0)
       //                strColumns.Append(",");
       //            if (strValues.Length > 0)
       //                strValues.Append(",");

       //            strColumns.Append("[").Append(pcInfo.Alias).Append("]");
       //            strValues.Append(para.ParameterName);
       //        }
       //        else if (pcInfo.IsAuto)
       //        {
       //            var para = parameters.AddOutParameter(pcInfo.Alias, DbType.Int64);
       //            strAuto = string.Format(" set {0}=@@identity", para.ParameterName);
       //        }
       //    }

       //    com.CommandType = CommandType.Text;
       //    com.CommandText = string.Format("insert into {0}({1}) values ({2}) {3}", tableName, strColumns, strValues,
       //        strAuto);
       //    com.Parameters.AddRange(parameters.ToArray());
       //}

       private static object GetPropertityValue<TModel>(PropertyInfo proper, TModel tModel)
       {
           object obj = proper.GetValue(tModel, null);
           if (proper.PropertyType.IsEnum)
           {
               obj = (int) obj;
           }
           return obj;
       }
       #endregion
    }


   #region   数据库参数定义

   //public enum DataBaseType
   //{
   //    SqlServer,
   //    Oracle
   //}

   //public static class DataBase
   //{
   //    public static DbProviderFactory GetDbProvider(DataBaseType type=DataBaseType.SqlServer)
   //    {
   //        DbProviderFactory factory = null;
   //        switch (type)
   //        {
   //            case DataBaseType.SqlServer:
   //                factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
   //                break;
   //            case DataBaseType.Oracle:
   //                factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
   //                break;
   //        }
   //        if (factory == null)
   //        {
   //            throw new ArgumentNullException("type", "DbProviderFactory do not support!");
   //        }
   //        return factory;
   //    }
   //}

   ///// <summary>
   ///// 数据库参数封装
   ///// </summary>
   //public class DbParameters : Collection<DbParameter>
   //{
   //    private readonly DataBaseType _dataBaseType;
   //   /// <summary>
   //   /// 
   //   /// </summary>
   //   /// <param name="type"></param>
   //    public DbParameters(DataBaseType type)
   //    {
   //        _dataBaseType = type;
   //    }

   //    /// <summary>
   //    /// 添加输入参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    /// <returns></returns>
   //    public DbParameter AddInParameter(string name, DbType dbType)
   //    {
   //       return AddParameter(name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, null);
   //    }

   //    /// <summary>
   //    /// 添加输入参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="value"></param>
   //    /// <returns></returns>
   //    public DbParameter AddInParameter(string name, object value)
   //    {
   //        return AddParameter(name, ConvertToDbType(value==null?typeof(object):value.GetType()), ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
   //    }


   //    /// <summary>
   //    /// 添加输入参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    /// <param name="value"></param>
   //    /// <returns></returns>
   //    public DbParameter AddInParameter(string name, DbType dbType, object value)
   //    {
   //       return AddParameter(name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
   //    }

   //    /// <summary>
   //    /// 添加一个新的参数
   //    /// </summary>
   //    /// <param name="name">参数名称，不带前缀的，例如直接   “name” </param>
   //    /// <param name="dbType">参数类型</param>
   //    /// <param name="size">参数大小</param>
   //    /// <param name="value">参数值</param>
   //    public DbParameter AddInParameter(string name, DbType dbType, int size, object value)
   //    {
   //       return AddParameter(name, dbType, size, ParameterDirection.Input, true, 0, 0, String.Empty, DataRowVersion.Default,
   //            value);
   //    }

   //    /// <summary>
   //    ///    添加输入参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    /// <param name="sourceColumn"></param>
   //    /// <param name="sourceVersion"></param>
   //    /// <returns></returns>
   //    public DbParameter AddInParameter(string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion)
   //    {
   //       return AddParameter(name, dbType, 0, ParameterDirection.Input, true, 0, 0, sourceColumn, sourceVersion, null);
   //    }

   //    /// <summary>
   //    /// 添加输出参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    public DbParameter AddOutParameter(string name, DbType dbType)
   //    {
   //       return AddParameter(name, dbType, 0, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Default,
   //            DBNull.Value);
   //    }

   //    /// <summary>
   //    /// 添加输出参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    /// <param name="size"></param>
   //    public DbParameter AddOutParameter(string name, DbType dbType, int size)
   //    {
   //       return AddParameter(name, dbType, size, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Default,
   //            DBNull.Value);
   //    }

   //    /// <summary>
   //    /// 
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    /// <param name="direction"></param>
   //    /// <param name="sourceColumn"></param>
   //    /// <param name="sourceVersion"></param>
   //    /// <param name="value"></param>
   //    public DbParameter AddParameter(string name, DbType dbType, ParameterDirection direction, string sourceColumn,
   //        DataRowVersion sourceVersion, object value)
   //    {
   //       return AddParameter(name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);

   //    }

   //    /// <summary>
   //    /// 添加参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    /// <param name="size"></param>
   //    /// <param name="direction"></param>
   //    /// <param name="nullable"></param>
   //    /// <param name="precision"></param>
   //    /// <param name="scale"></param>
   //    /// <param name="sourceColumn"></param>
   //    /// <param name="sourceVersion"></param>
   //    /// <param name="value"></param>
   //    protected DbParameter AddParameter(string name, DbType dbType, int size, ParameterDirection direction, bool nullable,
   //        byte precision,
   //        byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
   //    {
   //        DbParameter parameter = CreateParameter(name, dbType, size, direction, nullable, precision, scale,
   //            sourceColumn, sourceVersion, value);
   //        Add(parameter);
   //        return parameter;
   //    }

   //    /// <summary>
   //    /// 创建参数
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <param name="dbType"></param>
   //    /// <param name="size"></param>
   //    /// <param name="direction"></param>
   //    /// <param name="nullable"></param>
   //    /// <param name="precision"></param>
   //    /// <param name="scale"></param>
   //    /// <param name="sourceColumn"></param>
   //    /// <param name="sourceVersion"></param>
   //    /// <param name="value"></param>
   //    /// <returns></returns>
   //    protected internal DbParameter CreateParameter(string name, DbType dbType, int size, ParameterDirection direction,
   //        bool nullable, byte precision,
   //        byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
   //    {
   //        DbParameter para = DataBase.GetDbProvider(_dataBaseType).CreateParameter();
   //        para.ParameterName = string.Concat(PreParaToken, name);
   //        ConfigureParameter(para, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion,
   //            value);
   //        return para;
   //    }

   //    /// <summary>
   //    /// 配置参数信息
   //    /// </summary>
   //    /// <param name="param"></param>
   //    /// <param name="dbType"></param>
   //    /// <param name="size"></param>
   //    /// <param name="direction"></param>
   //    /// <param name="nullable"></param>
   //    /// <param name="precision"></param>
   //    /// <param name="scale"></param>
   //    /// <param name="sourceColumn"></param>
   //    /// <param name="sourceVersion"></param>
   //    /// <param name="value"></param>
   //    protected virtual void ConfigureParameter(DbParameter param, DbType dbType, int size,
   //        ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn,
   //        DataRowVersion sourceVersion, object value)
   //    {
   //        param.DbType = dbType;
   //        param.Size = size;
   //        param.Value = value ?? DBNull.Value;
   //        param.Direction = direction;
   //        param.IsNullable = nullable;
   //        param.SourceColumn = sourceColumn;
   //        param.SourceVersion = sourceVersion;
   //    }

   //    /// <summary>
   //    ///   索引器
   //    /// </summary>
   //    /// <param name="name"></param>
   //    /// <returns></returns>
   //    public DbParameter this[string name]
   //    {
   //        get
   //        {
   //            if (string.IsNullOrEmpty(name))
   //            {
   //                throw new ArgumentNullException("ParameterName","查询的属性名称不能为空！");
   //            }
   //            return Items.FirstOrDefault(dbp => dbp.ParameterName ==string.Concat(PreParaToken, name));
   //        }
   //    }

   //    private string _preParaToken;

   //    /// <summary>
   //    /// 当前数据的库操作符号
   //    /// </summary>
   //    public String PreParaToken
   //    {
   //        get
   //        {
   //            if (string.IsNullOrEmpty(_preParaToken))
   //            {
   //                switch (_dataBaseType)
   //                {
   //                    case DataBaseType.SqlServer:
   //                        _preParaToken = "@";
   //                        break;
   //                    //case DataBaseType.Oracle:
   //                    //    _preParaToken = ":";
   //                    //    break;
   //                    default:
   //                        _preParaToken = string.Empty;
   //                        break;
   //                }
   //            }
   //            return _preParaToken;
   //        }
   //    }


   //    #region  数据库类型和.net类型映射
   //    /// <summary>
   //    ///    数据库类型和.net类型映射
   //    /// </summary>
   //    private static Dictionary<Type,DbType>  typeMap=new Dictionary<Type, DbType>();
   //    /// <summary>
   //    /// 
   //    /// </summary>
   //    static DbParameters()
   //    {
   //        typeMap[typeof(byte)] = DbType.Byte;
   //        typeMap[typeof(sbyte)] = DbType.SByte;
   //        typeMap[typeof(short)] = DbType.Int16;
   //        typeMap[typeof(ushort)] = DbType.UInt16;
   //        typeMap[typeof(int)] = DbType.Int32;
   //        typeMap[typeof(uint)] = DbType.UInt32;
   //        typeMap[typeof(long)] = DbType.Int64;
   //        typeMap[typeof(ulong)] = DbType.UInt64;
   //        typeMap[typeof(float)] = DbType.Single;
   //        typeMap[typeof(double)] = DbType.Double;
   //        typeMap[typeof(decimal)] = DbType.Decimal;
   //        typeMap[typeof(bool)] = DbType.Boolean;
   //        typeMap[typeof(string)] = DbType.String;
   //        typeMap[typeof(char)] = DbType.StringFixedLength;
   //        typeMap[typeof(Guid)] = DbType.Guid;
   //        typeMap[typeof(DateTime)] = DbType.DateTime;
   //        typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
   //        typeMap[typeof(TimeSpan)] = DbType.Time;
   //        typeMap[typeof(byte[])] = DbType.Binary;
   //        typeMap[typeof(byte?)] = DbType.Byte;
   //        typeMap[typeof(sbyte?)] = DbType.SByte;
   //        typeMap[typeof(short?)] = DbType.Int16;
   //        typeMap[typeof(ushort?)] = DbType.UInt16;
   //        typeMap[typeof(int?)] = DbType.Int32;
   //        typeMap[typeof(uint?)] = DbType.UInt32;
   //        typeMap[typeof(long?)] = DbType.Int64;
   //        typeMap[typeof(ulong?)] = DbType.UInt64;
   //        typeMap[typeof(float?)] = DbType.Single;
   //        typeMap[typeof(double?)] = DbType.Double;
   //        typeMap[typeof(decimal?)] = DbType.Decimal;
   //        typeMap[typeof(bool?)] = DbType.Boolean;
   //        typeMap[typeof(char?)] = DbType.StringFixedLength;
   //        typeMap[typeof(Guid?)] = DbType.Guid;
   //        typeMap[typeof(DateTime?)] = DbType.DateTime;
   //        typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
   //        typeMap[typeof(TimeSpan?)] = DbType.Time;
   //    }
   //    #endregion



   //    /// <summary>
   //    ///   将.net类型转成数据库类型
   //    /// </summary>
   //    /// <param name="type"></param>
   //    /// <returns></returns>
   //    internal static DbType ConvertToDbType(Type type)
   //    {
   //        if (type.IsEnum)
   //        {
   //            return DbType.Int32;
   //        }
   //        if (typeMap.ContainsKey(type))
   //        {
   //            return typeMap[type];
   //        }
   //        return DbType.String;
   //    }
   //}

   #endregion

}
