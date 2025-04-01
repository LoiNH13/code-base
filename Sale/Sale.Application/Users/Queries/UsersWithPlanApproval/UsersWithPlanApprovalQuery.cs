using Domain.Core.Primitives.Maybe;
using Sale.Contract.Core;
using Sale.Contract.Users;
using Sale.Domain.Enumerations;

namespace Sale.Application.Users.Queries.UsersWithPlanApproval;

public sealed class UsersWithPlanApprovalQuery : ManagedByFilter, IQuery<Maybe<List<UsersWithPlanApprovalResponse>>>
{
    public Guid PlanningControlId { get; }

    public Maybe<PlanningApprovalStatus> Status { get; }

    public bool? IsHaveOdoo { get; }

    public UsersWithPlanApprovalQuery(Guid planningControlId, int? status, bool? isHaveOdoo, List<Guid>? managedByUserIds, bool includeSubordinateUsers) : base(managedByUserIds, includeSubordinateUsers)
    {
        PlanningControlId = planningControlId;
        Status = PlanningApprovalStatus.FromValue(status ?? 0);
        IsHaveOdoo = isHaveOdoo;
    }
}
