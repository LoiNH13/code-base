using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.PlanningControls;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControls.Queries.PlanningControls;

internal sealed class PlanningControlsQueryHandler(IPlanningControlRepository planningControlRepository)
    : IQueryHandler<PlanningControlsQuery, Maybe<PagedList<PlanningControlResponse>>>
{
    public async Task<Maybe<PagedList<PlanningControlResponse>>> Handle(PlanningControlsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<PlanningControl> query = planningControlRepository.Queryable()
            .WhereIf(request.Status > 0, x => x.Status == PlanningControlStatus.FromValue(request.Status))
            .Paginate(request.PageNumber, request.PageSize, out Paged paged);
        if (paged.NotExists()) return new PagedList<PlanningControlResponse>(paged);

        List<PlanningControlResponse> data =
            await query.Select(x => new PlanningControlResponse(x)).ToListAsync(cancellationToken);
        return new PagedList<PlanningControlResponse>(paged, data);
    }
}