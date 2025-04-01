using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.PlanningControls;

namespace Sale.Application.PlanningControls.Queries.PlanningControls;

public sealed class PlanningControlsQuery : IPagingQuery, IQuery<Maybe<PagedList<PlanningControlResponse>>>
{
    public PlanningControlsQuery(int pageNumber, int pageSize, int? status)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Status = status ?? 0;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int Status { get; set; }
}
