using Sale.Domain.Entities.Users;

namespace Sale.Contract.Core;

/// <summary>
/// Represents a filter for managing entities based on user IDs.
/// </summary>
public class ManagedByFilter
{
    /// <summary>
    /// Gets a list of user IDs to filter by.
    /// </summary>
    public List<Guid> ManagedByUserIds { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedByFilter"/> class with a single user ID.
    /// </summary>
    /// <param name="managedByUserId">The ID of the user to filter by. If null, no user ID is set.</param>
    /// <param name="includeSubordinateUsers">A flag indicating whether to include subordinate users in the filter. Default is false.</param>
    protected ManagedByFilter(Guid? managedByUserId, bool includeSubordinateUsers = false)
    {
        IncludeSubordinateUsers = includeSubordinateUsers;
        ManagedByUserIds = managedByUserId != null ? [managedByUserId.Value] : [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedByFilter"/> class with a list of user IDs.
    /// </summary>
    /// <param name="managedByUserIds">The list of user IDs to filter by. If null, an empty list is set.</param>
    /// <param name="includeSubordinateUsers">A flag indicating whether to include subordinate users in the filter. Default is false.</param>
    protected ManagedByFilter(List<Guid>? managedByUserIds, bool includeSubordinateUsers = false)
    {
        IncludeSubordinateUsers = includeSubordinateUsers;
        ManagedByUserIds = managedByUserIds ?? [];
    }

    /// <summary>
    /// Gets a flag indicating whether to include subordinate users in the filter.
    /// </summary>
    public bool IncludeSubordinateUsers { get; }

    /// <summary>
    /// Gets a list of users associated with the filter.
    /// </summary>
    public List<User> Users { get; } = [];

    /// <summary>
    /// Adds a user to the list of users associated with the filter.
    /// </summary>
    /// <param name="user">The user to add.</param>
    public void AddUser(User user) => Users.Add(user);

    /// <summary>
    /// Adds a list of users to the list of users associated with the filter.
    /// </summary>
    /// <param name="users">The list of users to add.</param>
    public void AddUsers(List<User> users) => Users.AddRange(users);
}