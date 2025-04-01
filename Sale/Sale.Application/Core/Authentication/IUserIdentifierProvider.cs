using Sale.Domain.Enumerations;

namespace Sale.Application.Core.Authentication;

/// <summary>
/// Represents the user identifier provider interface.
/// </summary>
public interface IUserIdentifierProvider
{
    /// <summary>
    /// Gets the authenticated user identifier.
    /// </summary>
    Guid UserId { get; }

    /// <summary>
    /// Gets the email of the authenticated user.
    /// </summary>
    string Email { get; }

    ERole Role { get; }
}
