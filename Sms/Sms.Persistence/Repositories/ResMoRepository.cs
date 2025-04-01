using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Factories;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Persistence.Extensions;
using Persistence.Repositories;
using Sms.Application.Core;
using Sms.Domain.Entities;
using Sms.Domain.Repositories;

namespace Sms.Persistence.Repositories;

internal sealed class ResMoRepository(IDbContext dbContext, IDistributedCacheFactory cacheFactory)
    : GenericRepository<ResMo>(dbContext), IResMoRepository
{
    private readonly IDistributedCache _cache = cacheFactory.CreateCache(Const.CacheInstanceName);

    public async Task<Maybe<ResMo>> GetByServiceNum(string serviceNum) =>
        await _cache.GetOrCreateAsync($"ByServicePhone:{serviceNum}",
            async () => await DbContext.Set<ResMo>().FirstOrDefaultAsync(rm => rm.ServicePhone == serviceNum)
            ?? default!,
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24) });
}