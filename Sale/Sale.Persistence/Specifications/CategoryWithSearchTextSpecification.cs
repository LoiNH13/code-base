using Microsoft.EntityFrameworkCore;
using Persistence.Specifications;
using Sale.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Sale.Persistence.Specifications;

internal sealed class CategoryWithSearchTextSpecification(string searchText) : Specification<Category>
{
    /// <inheritdoc />
    public override Expression<Func<Category, bool>> ToExpression() =>
        category => EF.Functions.ILike(category.Name, $"%{searchText}%");
}