﻿using Domain.Core.Primitives;
using System.Linq.Expressions;

namespace Persistence.Specifications;

/// <summary>
/// Represents the abstract base class for specifications.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class Specification<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// Converts the specification to an expression predicate.
    /// </summary>
    /// <returns>The expression predicate.</returns>
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    /// <summary>
    /// Checks if the specified entity satisfies this specification.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>True if the entity satisfies the specification, otherwise false.</returns>
    internal bool IsSatisfiedBy(TEntity entity) => ToExpression().Compile()(entity);

    public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification) =>
        specification.ToExpression();
}
