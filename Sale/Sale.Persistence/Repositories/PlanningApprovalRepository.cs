using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Persistence.Repositories;

internal sealed class PlanningApprovalRepository(IDbContext dbContext)
    : GenericRepository<PlanningApproval>(dbContext), IPlanningApprovalRepository
{
    public async Task<Maybe<PlanningApproval>> GetByCustomerAndPlanning(Guid customerId, Guid planningControlId) =>
        await DbContext.Set<PlanningApproval>()
            .Where(x => x.CustomerId == customerId && x.PlanningControlId == planningControlId)
            .Include(x => x.PlanningControl).ThenInclude(x => x!.PlanningControlLines)
            .FirstOrDefaultAsync() ?? default!;

    public override async Task<Maybe<PlanningApproval>> GetByIdAsync(Guid id) =>
        await DbContext.Set<PlanningApproval>().Where(x => x.Id == id)
            .Include(x => x.PlanningControl).ThenInclude(x => x!.PlanningControlLines)
            .FirstOrDefaultAsync() ?? default!;
}