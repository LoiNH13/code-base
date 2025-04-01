using Domain.Core.Primitives.Maybe;
using Sale.Domain.Entities.Planning;

namespace Sale.Domain.Repositories;

public interface IPlanningApprovalRepository
{
    IQueryable<PlanningApproval> Queryable();

    void Insert(PlanningApproval planningApproval);

    Task<Maybe<PlanningApproval>> GetByIdAsync(Guid id);

    Task<Maybe<PlanningApproval>> GetByCustomerAndPlanning(Guid customerId, Guid planningControlId);
}
