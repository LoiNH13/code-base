namespace Contract.Common;

/// <summary>
/// Represents a paginated response with metadata and items.
/// </summary>
/// <typeparam name="T">The type of items in the paginated response.</typeparam>
public class PagingJson<T> where T : class
{
    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of items.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the items for the current page.
    /// </summary>
    public T[] Items { get; set; } = Array.Empty<T>();

    /// <summary>
    /// Gets the flag indicating whether the next page exists.
    /// </summary>
    public bool HasNextPage => Page * PageSize < TotalCount;

    /// <summary>
    /// Gets the flag indicating whether the previous page exists.
    /// </summary>
    public bool HasPreviousPage => Page > 1;
}