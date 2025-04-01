using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.Wards;

namespace Sale.Application.Odoo.Wards.Queries.Wards;

public sealed class OdooWardsQuery : IPagingQuery, IQuery<Maybe<PagedList<OdooWardResponse>>>
{
    public OdooWardsQuery(int pageNumber, int pageSize, int? districtId, string? searchText)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        DistrictId = districtId;
        SearchText = searchText ?? string.Empty;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int? DistrictId { get; }

    public string SearchText { get; }
}
