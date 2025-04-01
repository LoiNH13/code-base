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

internal sealed class OdooCustomerRepository : IOdooCustomerRepository
{
    readonly OdooDbContext _context;
    readonly IDistributedCache _cache;

    public OdooCustomerRepository(OdooDbContext context, IDistributedCacheFactory cacheFactory)
    {
        _context = context;
        _cache = cacheFactory.CreateCache(Const.CacheInstanceName);
    }

    public async Task<Maybe<ResPartner>> GetCustomerById(int id) =>
       await _cache.GetOrCreateAsync(id.ToString(), async () => await _context.ResPartners.FindAsync(id) ?? default!
       , new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });

    public IQueryable<ResPartner> Queryable(string searchText)
    {
        IQueryable<ResPartner> query = _context.ResPartners
        .Where(x => (x.Active ?? false) && (x.Type == "contact") && (x.IsCustomer ?? false))
        .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => EF.Functions.ILike(x.Name!, $"%{searchText}%")
        || EF.Functions.ILike(x.Ref!, $"%{searchText}%"));

        IQueryable<ResourceResource> resources = _context.ResourceResources;

        return from q in query
               join r in resources on q.UserId equals r.UserId into rgroup
               from r in rgroup.DefaultIfEmpty()
               where r.Active ?? true
               select q;
    }

    public async Task<Maybe<ResPartner>> GetCustomerByOdooRef(string odooRef) =>
        await _context.ResPartners.FirstOrDefaultAsync(x => x.Ref == odooRef) ?? default!;

    public IQueryable<VietmapCustomerView> ViewQueryable() => _context.VietmapCustomerViews;
}
