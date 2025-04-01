using Sale.Domain.Enumerations;

namespace Sale.Contract.Users;

/// <summary>
/// Represents a request to update user information.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Gets or sets the user's name. This property is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who manages this user. This property is optional.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the user's role.
    /// </summary>
    public ERole Role { get; set; }

    /// <summary>
    /// Gets or sets the user's business type.
    /// </summary>
    public EBusinessType BusinessType { get; set; }
}