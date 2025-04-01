using Application.Core.Abstractions.Data;
using Application.Core.Extensions;
using Persistence.Repositories;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;
using Sale.Persistence.Specifications;

namespace Sale.Persistence.Repositories;

internal sealed class CategoryRepository(IDbContext dbContext) :
    GenericRepository<Category>(dbContext), ICategoryRepository
{
    public IQueryable<Category> Queryable(string searchText) =>
        DbContext.Set<Category>().WhereIf(!string.IsNullOrWhiteSpace(searchText), new CategoryWithSearchTextSpecification(searchText));
}
