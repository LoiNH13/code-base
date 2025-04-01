using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories;

public interface IOdooCategoryRepository
{
    IQueryable<ProductCategory> Queryable();
}