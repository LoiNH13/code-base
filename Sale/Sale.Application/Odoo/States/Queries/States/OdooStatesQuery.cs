using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.States;

namespace Sale.Application.Odoo.States.Queries.States;

public sealed class OdooStatesQuery : IPagingQuery, IQuery<Maybe<PagedList<OdooStateResponse>>>
{
    public OdooStatesQuery(int pageNumber, int pageSize, string? searchText)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchText = searchText ?? "";
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public string SearchText { get; }
}
