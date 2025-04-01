using Microsoft.EntityFrameworkCore;
using Persistence.Specifications;
using Sale.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Sale.Persistence.Specifications;

internal sealed class ProductWithSearchTextSpecification(string searchText) : Specification<Product>
{
    /// <inheritdoc />
    public override Expression<Func<Product, bool>> ToExpression() =>
        product => EF.Functions.ILike(product.Name, $"%{searchText}%");
}