using Application.Core.Abstractions.Data;
using Persistence.Repositories;
using Sale.Domain.Entities;
using Sale.Domain.Repositories;

namespace Sale.Persistence.Repositories;

internal sealed class TimeFrameRepository(IDbContext dbContext)
    : GenericRepository<TimeFrame>(dbContext), ITimeFrameRepository;