namespace Contract.Authentication;

/// <summary>
/// Represents a request to authenticate a user using Vietmap SSO.
/// </summary>
public class LoginVietmapSsoRequest
{
    /// <summary>
    /// Gets or sets the client ID used for authentication.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the access token obtained from Vietmap SSO.
    /// </summary>
    public required string AccessToken { get; set; }
}
