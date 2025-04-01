using Domain.Core.Primitives.Maybe;
using Sale.Domain.Entities.Products;

namespace Sale.Domain.Repositories;

public interface ICategoryRepository
{
    IQueryable<Category> Queryable();

    IQueryable<Category> Queryable(string searchText);

    void Insert(Category category);

    Task<Maybe<Category>> GetByIdAsync(Guid id);
}
