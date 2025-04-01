using Domain.Core.Primitives.Maybe;
using Sale.Contract.PlanningControls;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControls.Queries.PlanningControlById;

internal sealed class PlanningControlByIdQueryHandler(IPlanningControlRepository planningControlRepository)
    : IQueryHandler<PlanningControlByIdQuery, Maybe<PlanningControlResponse>>
{
    public async Task<Maybe<PlanningControlResponse>> Handle(PlanningControlByIdQuery request,
        CancellationToken cancellationToken) =>
        await planningControlRepository.GetByIdAsync(request.PlanningControlId)
            .Match(x => new PlanningControlResponse(x), () => default!);
}