using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Domain.Services;
using Domain.ValueObjects;
using Sale.Domain.Core.Abstractions;
using Sale.Domain.Core.Errors;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Sale.Domain.Entities.Users;

public sealed class User : Entity, IAuditableEntity, ISoftDeletableEntity, IHaveManagedByUser
{
    private string _passwordHash;

    public User(string name, Email email, string passwordHash)
    {
        Name = name;
        Email = email;
        _passwordHash = passwordHash;
    }

    public string Name { get; private set; } = string.Empty;

    public Email Email { get; init; }

    public Guid? ManagedByUserId { get; private set; }

    public EBusinessType BusinessType { get; set; } = EBusinessType.Dealer;

    public ERole Role { get; private set; } = ERole.User;

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public User? ManagedByUser { get; init; }

    public List<User> SubordinateUsers { get; init; } = new();

    private User() { }

    public async Task<Result> Update(string name,
                                     Guid? managedByUserId,
                                     ERole role,
                                     EBusinessType businessType,
                                     IUserRepository userRepository)
    {
        if (managedByUserId is not null && ManagedByUserId != managedByUserId)
        {
            Result mustNotLoop = await userRepository.UserMustNotDependencyLoop(Id, managedByUserId.Value);
            if (mustNotLoop.IsFailure) return mustNotLoop;
        }

        ManagedByUserId = managedByUserId;
        Name = name;
        Role = role;
        BusinessType = businessType;

        return Result.Success();
    }

    /// <summary>
    /// Verifies that the provided password hash matches the password hash.
    /// </summary>
    /// <param name="password">The password to be checked against the user password hash.</param>
    /// <param name="passwordHashChecker">The password hash checker.</param>
    /// <returns>True if the password hashes match, otherwise false.</returns>
    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
        => !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(_passwordHash, password);

    /// <summary>
    /// Changes the users password.
    /// </summary>
    /// <param name="passwordHash">The password hash of the new password.</param>
    /// <returns>The success result or an error.</returns>
    public Result ChangePassword(string passwordHash)
    {
        if (passwordHash == _passwordHash)
        {
            return Result.Failure(SaleDomainErrors.User.CannotChangePassword);
        }

        _passwordHash = passwordHash;

        return Result.Success();
    }


    public List<User> GetUsersIncludeSubordinates()
    {
        List<User> users = [this];

        users.AddRange(SubordinateUsers.SelectMany(x => x.GetUsersIncludeSubordinates()));

        return users;
    }

    public List<Guid> GetUserIdsIncludeSubordinates() =>
        GetUsersIncludeSubordinates().Select(x => x.Id).Distinct().ToList();
}
