using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.PlanningApprovals;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningApprovals.Queries.PlanningApprovals;

internal sealed class PlanningApprovalsQueryHandler(IPlanningApprovalRepository planningApprovalRepository)
    : IQueryHandler<PlanningApprovalsQuery, Maybe<PagedList<PlanningApprovalResponse>>>
{
    public async Task<Maybe<PagedList<PlanningApprovalResponse>>> Handle(PlanningApprovalsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<PlanningApproval> query = planningApprovalRepository.Queryable()
            .WhereIf(request.CustomerId.HasValue, x => x.CustomerId == request.CustomerId)
            .WhereIf(request.PlanningControlId.HasValue, x => x.PlanningControlId == request.PlanningControlId)
            .WhereIf(request.PlanningApprovalStatus > 0,
                x => x.Status == PlanningApprovalStatus.FromValue(request.PlanningApprovalStatus))
            .WhereIf(request.PlanningControlStatus > 0,
                x => x.PlanningControl.Status == PlanningControlStatus.FromValue(request.PlanningControlStatus))
            .Paginate(request.PageNumber, request.PageSize, out Paged paged);

        if (paged.NotExists()) return new PagedList<PlanningApprovalResponse>(paged);

        List<PlanningApprovalResponse> data =
            await query.Select(x => new PlanningApprovalResponse(x)).ToListAsync(cancellationToken);

        return new PagedList<PlanningApprovalResponse>(paged, data);
    }
}