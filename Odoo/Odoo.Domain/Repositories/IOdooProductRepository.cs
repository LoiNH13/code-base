using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories;

public interface IOdooProductRepository
{
    IQueryable<ProductProduct> Queryable(string searchText);

    Task<Maybe<ProductProduct>> GetProductById(int id);
}
