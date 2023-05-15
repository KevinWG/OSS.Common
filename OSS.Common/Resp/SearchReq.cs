using System.Collections.Generic;
using System.ComponentModel;

namespace OSS.Common;

/// <summary>
/// 排序类型
/// </summary>
public enum SortType
{
    /// <summary>
    /// 倒序  由大到小
    /// </summary>
    desc = 0,

    /// <summary>
    /// 顺序  由小到大
    /// </summary>
    asc = 1,
}

/// <summary>
///  搜索请求基类
/// </summary>
public class SearchReq
{
    /// <summary>
    /// 搜索请求
    /// </summary>
    public SearchReq() : this(20, 1)
    {
    }

    /// <summary>
    ///  搜索请求
    /// </summary>
    /// <param name="size"></param>
    /// <param name="currentPage"></param>
    public SearchReq(int size, int currentPage)
    {
        this.size = size;
        this.page = currentPage;
    }

    private int _currentPage = 1;

    /// <summary>
    /// 当前页数（默认是1
    /// </summary>
    public int page
    {
        get => _currentPage <= 0 ? 1 : _currentPage;
        set => _currentPage = value;
    }

    /// <summary>
    ///   每页的数量
    /// </summary>
    public int size { get; set; }

    /// <summary>
    /// 是否请求获取数量
    /// ( 如果是 true , 会查询总数。
    /// ( 如果是 false , 不查询总数，仅返回当前页列表，性能较好
    /// </summary>
    [DefaultValue(true)]
    public bool req_count { get; set; } = true;

    /// <summary>
    ///    获取起始行
    /// </summary>
    public int GetStartRow() => (page - 1) * size;

    private Dictionary<string, SortType> _orders;

    /// <summary>
    /// 排序集合      适用于多个查询条件
    /// </summary>
    public Dictionary<string, SortType> orders => _orders ??= new Dictionary<string, SortType>();
}

/// <summary>
/// 搜索实体( filter属性为FType类型 
/// </summary>
public class SearchReq<FType> : SearchReq
{
    /// <summary>
    ///   构造函数
    /// </summary>
    public SearchReq()
    {
    }

    /// <summary>
    /// 过滤器
    /// </summary>
    public FType filter { get; set; }
}

/// <summary>
///  搜索实体（filter 类型为 Dictionary&lt;string, string&gt; ）
/// </summary>
public class DicFilterSearchReq : SearchReq<Dictionary<string, string>>
{
    /// <inheritdoc />
    public DicFilterSearchReq()
    {
        filter = new Dictionary<string, string>();
    }
}