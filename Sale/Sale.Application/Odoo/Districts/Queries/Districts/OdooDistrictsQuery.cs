using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.Districts;

namespace Sale.Application.Odoo.Districts.Queries.Districts;

public sealed class OdooDistrictsQuery : IPagingQuery, IQuery<Maybe<PagedList<OdooDistrictResponse>>>
{
    public OdooDistrictsQuery(int pageNumber, int pageSize, int? stateId, string? searchText)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        StateId = stateId;
        SearchText = searchText ?? "";
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int? StateId { get; }

    public string SearchText { get; }
}
