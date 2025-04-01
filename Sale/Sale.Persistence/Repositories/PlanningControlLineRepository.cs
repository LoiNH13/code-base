using Application.Core.Abstractions.Data;
using Persistence.Repositories;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Persistence.Repositories;

internal sealed class PlanningControlLineRepository(IDbContext dbContext) : GenericRepository<PlanningControlLine>(dbContext), IPlanningControlLineRepository
{
}
