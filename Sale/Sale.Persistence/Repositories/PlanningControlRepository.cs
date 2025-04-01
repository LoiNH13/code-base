using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Persistence.Repositories;

internal sealed class PlanningControlRepository(IDbContext dbContext) : GenericRepository<PlanningControl>(dbContext), IPlanningControlRepository
{
    public override async Task<Maybe<PlanningControl>> GetByIdAsync(Guid id) =>
        await DbContext.Set<PlanningControl>().Where(x => x.Id == id)
        .Include(x => x.PlanningControlLines).ThenInclude(x => x.TimeFrame).FirstOrDefaultAsync() ?? default!;
}
