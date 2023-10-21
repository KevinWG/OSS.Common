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
    
    /// <summary>
    /// 排序字段集合
    /// </summary>
    public Dictionary<string, SortType>? orders { get; set; }

    #region 扩展项
    
    internal Dictionary<string, string>? _extProperty;
    /// <summary>
    ///  设置搜索项的值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetItemValue(string key, string value)
    {
        _extProperty ??= new Dictionary<string, string>();
        _extProperty[key] = value;
    }

    /// <summary>
    ///  获取索引项的值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetItemValue(string key)
    {
        if (_extProperty != null && _extProperty.TryGetValue(key, out var val))
        {
            return val;
        }
        return string.Empty;
    }

    /// <summary>
    ///  获取搜索项Key值列表
    /// </summary>
    /// <returns></returns>
    public string[] GetItemKeys()
    {
        return _extProperty == null ? Array.Empty<string>() : _extProperty.Keys.ToArray();
    }

    #endregion
}

/// <summary>
/// 搜索实体 ( 含有自定义类型(FType) filter 属性
/// </summary>
public abstract class SearchReq<FType> : SearchReq
{
    /// <summary>
    ///   构造函数
    /// </summary>
    protected SearchReq()
    {
    }

    /// <summary>
    /// 过滤器对象
    /// </summary>
    public FType filter { get; set; } = default!;
}

/// <summary>
///  搜索实体 ( 含有类型为Dictionary&lt;string, string&gt; 的 filter 属性
/// </summary>
public class DicFilterSearchReq : SearchReq
{
    /// <inheritdoc />
    public DicFilterSearchReq()
    {
        _extProperty = new Dictionary<string, string>();
    }

    /// <summary>
    /// 过滤器对象
    /// </summary>
    public Dictionary<string, string>? filter
    {
        get => _extProperty;
        set => _extProperty = value;
    }
}