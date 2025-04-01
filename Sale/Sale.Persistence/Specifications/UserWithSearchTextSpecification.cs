using Microsoft.EntityFrameworkCore;
using Persistence.Specifications;
using Sale.Domain.Entities.Users;
using System.Linq.Expressions;

namespace Sale.Persistence.Specifications;

internal sealed class UserWithSearchTextSpecification(string searchText) : Specification<User>
{
    /// <inheritdoc />
    public override Expression<Func<User, bool>> ToExpression() =>
        user => EF.Functions.ILike(user.Name, $"%{searchText}%")
                || EF.Functions.ILike(user.Email.Value, $"%{searchText}%");
}