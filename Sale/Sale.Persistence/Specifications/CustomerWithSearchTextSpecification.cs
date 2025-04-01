using Microsoft.EntityFrameworkCore;
using Persistence.Specifications;
using Sale.Domain.Entities.Customers;
using System.Linq.Expressions;

namespace Sale.Persistence.Specifications;

internal sealed class CustomerWithSearchTextSpecification(string searchText) : Specification<Customer>
{
    /// <inheritdoc />
    public override Expression<Func<Customer, bool>> ToExpression() =>
        customer => EF.Functions.ILike(customer.Name, $"%{searchText}%");
}