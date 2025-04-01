using Domain.Core.Primitives.Maybe;
using Sale.Contract.PlanningControls;

namespace Sale.Application.PlanningControls.Queries.PlanningControlById;

public sealed class PlanningControlByIdQuery : IQuery<Maybe<PlanningControlResponse>>
{
    public Guid PlanningControlId { get; }

    public PlanningControlByIdQuery(Guid planningControlId) => PlanningControlId = planningControlId;
}
