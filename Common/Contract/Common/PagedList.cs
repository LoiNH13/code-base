namespace Contract.Common;

/// <summary>
/// Represents the generic paged list.
/// </summary>
/// <typeparam name="T">The type of list.</typeparam>
public sealed class PagedList<T> : Paged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    public PagedList(IEnumerable<T> items, int page, int pageSize, int totalCount) : base(page, pageSize, totalCount)
    {
        Items = items.ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="paged"></param>
    public PagedList(Paged paged, IEnumerable<T> items = default!) : base(paged.Page, paged.PageSize, paged.TotalCount)
    {
        Items = items?.ToList() ?? new List<T>();
    }

    /// <summary>
    /// Gets the items.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; }
}