using Sale.Domain.Enumerations;

namespace Sale.Application.Core.Authentication;

/// <summary>
/// Represents the JWT provider interface.
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Creates the JWT for the specified user.
    /// </summary>
    /// <param name="userId">The userId.</param>
    /// <param name="name">The name</param>
    /// <param name="email">The email</param>
    /// <param name="role">The role</param>
    /// <returns>The JWT for the specified user.</returns>
    string Create(string userId, string name, string email, ERole role);
}