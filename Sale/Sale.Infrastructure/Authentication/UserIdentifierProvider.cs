using Microsoft.AspNetCore.Http;
using Sale.Application.Core.Authentication;
using Sale.Domain.Enumerations;
using System.Security.Claims;

namespace Sale.Infrastructure.Authentication;

/// <summary>
/// Represents the user identifier provider.
/// </summary>
internal sealed class UserIdentifierProvider : IUserIdentifierProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserIdentifierProvider"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
    {
        string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("userId")
                             ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

        string emailClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)
                            ?? throw new ArgumentException("The email claim is required.", nameof(httpContextAccessor));

        var roleClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

        UserId = new Guid(userIdClaim);
        Email = emailClaim;
        Role = roleClaim is null ? ERole.Sale : Enum.Parse<ERole>(roleClaim);
    }

    /// <inheritdoc />
    public Guid UserId { get; }

    public string Email { get; }

    public ERole Role { get; }
}
