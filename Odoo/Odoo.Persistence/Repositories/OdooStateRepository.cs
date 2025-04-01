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

public sealed class OdooStateRepository : IOdooStateRepository
{
    readonly OdooDbContext _context;
    readonly IDistributedCache _cache;

    public OdooStateRepository(OdooDbContext context, IDistributedCacheFactory cacheFactory)
    {
        _context = context;
        _cache = cacheFactory.CreateCache(Const.CacheInstanceName);
    }

    public async Task<Maybe<ResCountryState>> GetByIdAndIncludeAllAsync(int id) => await
        _cache.GetOrCreateAsync($"{nameof(GetByIdAndIncludeAllAsync)}:{id}", async () =>
            await _context.ResCountryStates.Include(x => x.ResDistricts).ThenInclude(x => x.ResWards).FirstOrDefaultAsync(x => x.Id == id) ?? default!
        , new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });

    public async Task<Maybe<ResCountryState>> GetByIdAsync(int id) =>
        await _cache.GetOrCreateAsync(id.ToString(), async () =>
            await _context.ResCountryStates.Include(x => x.ResDistricts).FirstOrDefaultAsync(x => x.Id == id) ?? default!
        , new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });

    public IQueryable<ResCountryState> Queryable(string searchText) =>
        _context.ResCountryStates.Where(x => x.CountryId == 241 && x.Id != 743)
        .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => EF.Functions.ILike(x.Name!, $"%{searchText}%"));
}
