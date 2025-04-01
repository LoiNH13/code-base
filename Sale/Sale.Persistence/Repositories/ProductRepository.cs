using Application.Core.Abstractions.Data;
using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;
using Sale.Persistence.Specifications;

namespace Sale.Persistence.Repositories;

internal sealed class ProductRepository(IDbContext dbContext)
    : GenericRepository<Product>(dbContext), IProductRepository
{
    public IQueryable<Product> Queryable(string searchText) =>
        DbContext.Set<Product>().WhereIf(!string.IsNullOrWhiteSpace(searchText),
            new ProductWithSearchTextSpecification(searchText));

    public override async Task<Maybe<Product>> GetByIdAsync(Guid id) =>
        await DbContext.Set<Product>().Where(x => x.Id == id)
            .Include(x => x.ProductTimeFramePrices).ThenInclude(x => x.TimeFrame).FirstOrDefaultAsync() ?? default!;
}