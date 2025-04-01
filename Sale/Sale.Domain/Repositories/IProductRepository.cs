using Domain.Core.Primitives.Maybe;
using Sale.Domain.Entities.Products;

namespace Sale.Domain.Repositories;

public interface IProductRepository
{
    IQueryable<Product> Queryable();

    IQueryable<Product> Queryable(string searchText);

    void Insert(Product timeFrame);

    Task<Maybe<Product>> GetByIdAsync(Guid id);
}
