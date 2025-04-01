using Domain.Core.Primitives;
using Persistence.Specifications;
using Sale.Domain.Core.Abstractions;
using System.Linq.Expressions;

namespace Sale.Persistence.Specifications;

internal sealed class GenericFilterUserSpecification<T> : Specification<T>
    where T : Entity, IHaveManagedByUser
{
    readonly List<Guid> _userIds;
    readonly bool _default;

    internal GenericFilterUserSpecification(List<Guid> userIds, bool @default = false)
    {
        _userIds = userIds;
        _default = @default;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        if (_userIds.Count == 0) return t => _default;
        else if (_userIds.Count == 1) return t => _userIds.FirstOrDefault() == t.ManagedByUserId;
        else return t => _userIds.Contains(t.ManagedByUserId ?? Guid.Empty);
    }
}
