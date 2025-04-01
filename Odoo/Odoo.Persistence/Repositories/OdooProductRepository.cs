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

internal sealed class OdooProductRepository : IOdooProductRepository
{
    readonly OdooDbContext _context;
    readonly IDistributedCache _cache;

    public OdooProductRepository(OdooDbContext context, IDistributedCacheFactory cacheFactory)
    {
        _context = context;
        _cache = cacheFactory.CreateCache(Const.CacheInstanceName);
    }

    public async Task<Maybe<ProductProduct>> GetProductById(int id) =>
        await _cache.GetOrCreateAsync(id.ToString(), async () => await _context.ProductProducts.FindAsync(id) ?? default!
        , new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });

    public IQueryable<ProductProduct> Queryable(string searchText) =>
         _context.ProductProducts.Where(x => x.Active ?? false)
        .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => EF.Functions.ILike(x.DisplayName!, $"%{searchText}%"));
}
