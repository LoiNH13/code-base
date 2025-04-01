using Domain.Core.Primitives.Maybe;
using Sale.Domain.Entities.Planning;

namespace Sale.Domain.Repositories;

public interface IPlanningControlRepository
{
    IQueryable<PlanningControl> Queryable();

    void Insert(PlanningControl planningControl);

    Task<Maybe<PlanningControl>> GetByIdAsync(Guid id);
}
