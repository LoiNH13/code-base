namespace Contract.Common;


/// <summary>
/// Represents a query for paging data.
/// </summary>
public interface IPagingQuery
{
    /// <summary>
    /// Gets the page number for the query.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Gets the number of items to return per page.
    /// </summary>
    public int PageSize { get; }
}