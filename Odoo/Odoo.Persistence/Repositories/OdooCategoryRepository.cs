using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Odoo.Persistence.Infrastructure;

namespace Odoo.Persistence.Repositories;

public sealed class OdooCategoryRepository(OdooDbContext context) : IOdooCategoryRepository
{
    public IQueryable<ProductCategory> Queryable()
    {
        return context.ProductCategories.AsQueryable();
    }
}