using Domain.Core.Events;

namespace Domain.Events;

public sealed class EntityDeletedEvent<T> : IDomainEvent
{
    public T Entity { get; }

    public EntityDeletedEvent(T entity) => Entity = entity;
}
