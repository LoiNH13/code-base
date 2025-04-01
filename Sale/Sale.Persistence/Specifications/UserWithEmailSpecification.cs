using Domain.ValueObjects;
using Persistence.Specifications;
using Sale.Domain.Entities.Users;
using System.Linq.Expressions;

namespace Sale.Persistence.Specifications;

/// <summary>
/// Represents the specification for determining the user with email.
/// </summary>
internal sealed class UserWithEmailSpecification : Specification<User>
{
    private readonly Email _email;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserWithEmailSpecification"/> class.
    /// </summary>
    /// <param name="email">The email.</param>
    internal UserWithEmailSpecification(Email email) => _email = email;

    /// <inheritdoc />
    public override Expression<Func<User, bool>> ToExpression() => user => user.Email.Value == _email;
}
