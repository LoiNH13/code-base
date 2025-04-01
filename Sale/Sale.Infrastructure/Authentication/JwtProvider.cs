using Application.Core.Abstractions.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sale.Application.Core.Authentication;
using Sale.Domain.Enumerations;
using Sale.Infrastructure.Authentication.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sale.Infrastructure.Authentication;

/// <summary>
/// Represents the JWT provider.
/// </summary>
internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTime _dateTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtProvider"/> class.
    /// </summary>
    /// <param name="jwtOptions">The JWT options.</param>
    /// <param name="dateTime">The current date and time.</param>
    public JwtProvider(
        IOptions<JwtSettings> jwtOptions,
        IDateTime dateTime)
    {
        _jwtSettings = jwtOptions.Value;
        _dateTime = dateTime;
    }

    /// <inheritdoc />
    public string Create(string userId, string name, string email, ERole eRole)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new Claim("userId", userId),
            new Claim("email", email),
            new Claim("name", name),
            new Claim(ClaimTypes.Role, eRole.ToString())
        ];

        DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}