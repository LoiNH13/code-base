namespace Contract.Common;

/// <summary>
/// Represents the paged result.
/// </summary>
public class Paged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Paged"/> class.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    public Paged(int page, int pageSize, int totalCount)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Gets the current page.
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// Gets the page size. The maximum page size is 100.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets the total number of items.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Gets the flag indicating whether the next page exists.
    /// </summary>
    public bool HasNextPage => Page * PageSize < TotalCount;

    /// <summary>
    /// Gets the flag indicating whether the previous page exists.
    /// </summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>
    /// Gets the flag indicating whether the list exists.
    /// </summary>
    public bool ExistsItems => TotalCount > 0 && (Page - 1) * PageSize < TotalCount;

    /// <summary>
    /// Gets the flag indicating whether the not list exists.
    /// </summary>
    public bool NotExists() => !ExistsItems;
}
