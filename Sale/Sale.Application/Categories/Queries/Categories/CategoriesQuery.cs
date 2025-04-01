using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Categories;

namespace Sale.Application.Categories.Queries.Categories;

public sealed class CategoriesQuery : IPagingQuery, ICommand<Maybe<PagedList<CategoryResponse>>>
{
    public CategoriesQuery(int pageNumber, int pageSize, string? searchText, EShowInApp? showInApp)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchText = searchText ?? string.Empty;
        ShowInApp = showInApp;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public string SearchText { get; }

    public EShowInApp? ShowInApp { get; }
}
