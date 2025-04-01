using Domain.Core.Primitives.Result;
using Sale.Domain.Enumerations;

namespace Sale.Application.Users.Commands.Update;

public sealed class UpdateUserCommand : ICommand<Result>
{
    public UpdateUserCommand(Guid id, string name, Guid? managedByUserId, ERole role, EBusinessType businessType)
    {
        Id = id;
        Name = name;
        ManagedByUserId = managedByUserId;
        Role = role;
        BusinessType = businessType;
    }

    public Guid Id { get; }

    public string Name { get; }

    public Guid? ManagedByUserId { get; }

    public EBusinessType BusinessType { get; }

    public ERole Role { get; }
}
