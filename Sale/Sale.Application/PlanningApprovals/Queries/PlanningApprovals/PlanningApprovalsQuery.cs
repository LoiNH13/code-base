using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.PlanningApprovals;

namespace Sale.Application.PlanningApprovals.Queries.PlanningApprovals;

public sealed class PlanningApprovalsQuery : IPagingQuery, IQuery<Maybe<PagedList<PlanningApprovalResponse>>>
{
    public PlanningApprovalsQuery(int pageNumber, int pageSize, Guid? customerId, int? planningControlStatus, int? planningApprovalStatus, Guid? planningControlId)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        CustomerId = customerId;
        PlanningControlStatus = planningControlStatus ?? 0;
        PlanningApprovalStatus = planningApprovalStatus ?? 0;
        PlanningControlId = planningControlId;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public Guid? CustomerId { get; }

    public Guid? PlanningControlId { get; }

    public int PlanningControlStatus { get; }

    public int PlanningApprovalStatus { get; }

}
