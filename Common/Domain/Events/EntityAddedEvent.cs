using Domain.Core.Events;

namespace Domain.Events;

public sealed class EntityAddedEvent<T> : IDomainEvent
{
    public T Entity { get; }

    public EntityAddedEvent(T entity) => Entity = entity;
}
