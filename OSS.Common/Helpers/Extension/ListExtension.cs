namespace System.Linq;

/// <summary>
///  列表扩展方法
/// </summary>
public static class ListExtension
{
    /// <summary>
    /// List合并
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="target">目标对象</param>
    /// <param name="source">来源对象</param>
    /// <param name="checkFunc">重复项依据方法 true-没有重复项，可以合并，fale-有重复项，不能合并</param>
    /// <returns>返回合并后的目标对象 - 排除重复项</returns>
    public static IList<T> Merged<T>(this IList<T> target, IList<T> source, Func<IList<T>, T, bool> checkFunc)
    {
        foreach (var t in source)
        {
            if (checkFunc(target, t))
            {
                target.Add(t);
            }
        }

        return target;
    }

    /// <summary>
    /// 数组是否是空
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IList<T>? target)
    {
        return target == null || !target.Any();
    }
}