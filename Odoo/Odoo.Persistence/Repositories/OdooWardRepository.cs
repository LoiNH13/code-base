using Application.Core.Abstractions.Factories;
using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Odoo.Persistence.Infrastructure;
using Persistence.Extensions;

namespace Odoo.Persistence.Repositories;

internal sealed class OdooWardRepository : IOdooWardRepository
{
    readonly OdooDbContext _context;
    readonly IDistributedCache _cache;

    public OdooWardRepository(OdooDbContext context, IDistributedCacheFactory cacheFactory)
    {
        _context = context;
        _cache = cacheFactory.CreateCache(Const.CacheInstanceName);
    }

    public async Task<Maybe<ResWard>> GetByIdAsync(int id) =>
        await _cache.GetOrCreateAsync(id.ToString(), async () =>
            await _context.ResWards.FirstOrDefaultAsync(x => x.Id == id) ?? default!
        , new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });

    public IQueryable<ResWard> Queryable(int? districtId, string searchText) =>
         _context.ResWards.WhereIf(districtId.HasValue, x => x.DistrictId == districtId)
        .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => EF.Functions.ILike(x.Name!, $"%{searchText}%"));
}
