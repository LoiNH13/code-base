using Domain.Core.Events;

namespace Domain.Events;

public sealed class EntityModifiedEvent<T> : IDomainEvent
{
    public T Entity { get; }

    public EntityModifiedEvent(T entity) => Entity = entity;
}