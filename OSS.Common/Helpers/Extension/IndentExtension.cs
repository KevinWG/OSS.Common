namespace System.Linq;

/// <summary>
/// 
/// </summary>
public static class IndentExtension
{
   
    /// <summary>
    ///  平级转化为递进结构
    /// </summary>
    /// <typeparam name="TFlat"></typeparam>
    /// <typeparam name="TIndent"></typeparam>
    /// <typeparam name="TKeyValue"></typeparam>
    /// <param name="sourceList"></param>
    /// <param name="convert">参数：TFlat实体，子级TIndentList-可能为空，输出：TIndent实体</param>
    /// <param name="defaultParentKeyValue">默认父级键值，如:0</param>
    /// <returns></returns>
    public static IList<TIndent> ToIndent<TFlat, TIndent, TKeyValue>(this IList<TFlat> sourceList,
                                                                     Func<TFlat, IList<TIndent>, TIndent> convert,
                                                                     TKeyValue defaultParentKeyValue)
    where TFlat : IFlatWithParentId<TKeyValue>
    {
        return sourceList.ToIndent<TFlat, TIndent, TKeyValue>(convert,c=>c.parent_id, c=>c.id,
            defaultParentKeyValue);
    }

    /// <summary>
    /// 平级转化为递进结构
    /// </summary>
    /// <typeparam name="TFlat"></typeparam>
    /// <typeparam name="TIndent"></typeparam>
    /// <typeparam name="TKeyValue"></typeparam>
    /// <param name="sourceList"></param>
    /// <param name="convert">参数：TFlat实体，子级TIndentList-可能为空，输出：TIndent实体</param>
    /// <param name="getParentKeyValue">获取对象父键值，如：c=>c.parent_id</param>
    /// <param name="getKeyValue">获取当前的键值，如： c=>c.id </param>
    /// <param name="defaultParentKeyValue">默认父级键值，如:0</param>
    /// <returns></returns>
    public static IList<TIndent> ToIndent<TFlat, TIndent, TKeyValue>(this IList<TFlat> sourceList,
                                                                      Func<TFlat, IList<TIndent>, TIndent> convert,
                                                                      Func<TFlat, TKeyValue> getParentKeyValue,
                                                                      Func<TFlat, TKeyValue> getKeyValue,
                                                                      TKeyValue defaultParentKeyValue)
    {
        var indentList = new List<TIndent>();
        foreach (var item in sourceList)
        {
            if (getParentKeyValue(item).Equals(defaultParentKeyValue))
            {
                var children = sourceList.ToIndent(convert, getParentKeyValue, getKeyValue,
                    getKeyValue(item));

                var indentItem = convert(item, children);
                indentList.Add(indentItem);
            }
        }

        return indentList;
    }





    /// <summary>
    /// 平级转化为递进结构
    /// </summary>
    /// <typeparam name="TFlat"></typeparam>
    /// <typeparam name="TIndent"></typeparam>
    /// <param name="sourceList"></param>
    /// <param name="convert">参数：TFlat实体，子级TIndentList-可能为空，输出：TIndent实体</param>
    /// <param name="getParentKeyValue">获取对象父键值，如：c=>c.parent_id</param>
    /// <param name="getKeyValue">获取当前的键值，如： c=>c.id </param>
    /// <param name="defaultParentKeyValue">默认父级键值，如:0</param>
    /// <returns></returns>
    public static IList<TIndent> ToIndent<TFlat, TIndent>(this IList<TFlat> sourceList,
                                                          Func<TFlat, IList<TIndent>, TIndent> convert,
                                                          Func<TFlat, string> getParentKeyValue,
                                                          Func<TFlat, string> getKeyValue,
                                                          string defaultParentKeyValue)
    {
        return sourceList.ToIndent<TFlat, TIndent, string>(convert, getParentKeyValue, getKeyValue,
            defaultParentKeyValue);
    }







    /// <summary>
    /// 递进结构转化为平级结构
    /// </summary>
    /// <typeparam name="TIndent"></typeparam>
    /// <typeparam name="TFlat"></typeparam>
    /// <param name="sourceList"></param>
    /// <param name="convert"></param>
    /// <param name="getChildren"></param>
    /// <returns></returns>
    public static IList<TFlat> ToFlat<TIndent, TFlat>(this IList<TIndent> sourceList,
                                                      Func<TIndent, TFlat> convert,
                                                      Func<TIndent, IList<TIndent>> getChildren)
    {
        var flatList = new List<TFlat>();
        foreach (var item in sourceList)
        {
            var flatItem = convert(item);
            flatList.Add(flatItem);

            var children = getChildren(item);
            if (children?.Count > 0)
            {
                var childFlatList = children.ToFlat(convert, getChildren);
                flatList.AddRange(childFlatList);
            }
        }

        return flatList;
    }


    /// <summary>
    /// 递进结构转化为平级结构
    /// </summary>
    /// <typeparam name="TIndent"></typeparam>
    /// <typeparam name="TFlat"></typeparam>
    /// <param name="sourceList"></param>
    /// <param name="convert"></param>
    /// <returns></returns>
    public static IList<TFlat> ToFlat<TIndent, TFlat>(this IList<TIndent> sourceList,
                                                      Func<TIndent, TFlat> convert)
        where TIndent : IIndentWithChildren<TIndent>
    {
        var flatList = new List<TFlat>();
        foreach (var item in sourceList)
        {
            var flatItem = convert(item);
            flatList.Add(flatItem);

            var children = item.children;
            if (children?.Count > 0)
            {
                var childFlatList = children.ToFlat(convert);
                flatList.AddRange(childFlatList);
            }
        }

        return flatList;
    }
}

/// <summary>
///  平级附带父级Id
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IFlatWithParentId<TValue>
{
    /// <summary>
    /// id
    /// </summary>
    public TValue id { get; set; }

     /// <summary>
     /// 父级id
     /// </summary>
     public TValue parent_id { get; set; }
}


/// <summary>
///  附带子类缩进实体
/// </summary>
/// <typeparam name="TIndent"></typeparam>
public interface IIndentWithChildren<TIndent>
{
    /// <summary>
    /// 子级
    /// </summary>
    public List<TIndent> children { get; set; }
}