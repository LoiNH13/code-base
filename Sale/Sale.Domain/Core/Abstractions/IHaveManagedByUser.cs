namespace Sale.Domain.Core.Abstractions;

public interface IHaveManagedByUser
{
    Guid? ManagedByUserId { get; }
}
