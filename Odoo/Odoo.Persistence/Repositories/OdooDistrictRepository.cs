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

internal sealed class OdooDistrictRepository : IOdooDistrictRepository
{
    readonly OdooDbContext _context;
    readonly IDistributedCache _cache;

    public OdooDistrictRepository(OdooDbContext context, IDistributedCacheFactory cacheFactory)
    {
        _context = context;
        _cache = cacheFactory.CreateCache(Const.CacheInstanceName);
    }

    public async Task<Maybe<ResDistrict>> GetByIdAsync(int id) =>
        await _cache.GetOrCreateAsync(id.ToString(), async () =>
            await _context.ResDistricts.Include(x => x.ResWards).FirstOrDefaultAsync(x => x.Id == id) ?? default!
        , new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });


    public IQueryable<ResDistrict> Queryable(int? stateId, string searchText) =>
        _context.ResDistricts
        .WhereIf(stateId.HasValue, x => x.StateId == stateId)
        .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => EF.Functions.ILike(x.Name!, $"%{searchText}%"));
}
