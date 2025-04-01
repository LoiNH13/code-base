namespace Contract.Authentication;

/// <summary>
/// Represents the response from the Vietmap SSO service.
/// </summary>
public class VietmapSsoResponse
{
    /// <summary>
    /// Gets or sets the data returned from the Vietmap SSO service.
    /// </summary>
    public Data? Data { get; set; }

    /// <summary>
    /// Gets or sets the code returned from the Vietmap SSO service.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the message returned from the Vietmap SSO service.
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// Represents the data returned from the Vietmap SSO API.
/// </summary>
public class Data
{
    /// <summary>
    /// Gets or sets the profile information of the user.
    /// </summary>
    public Profile? Profile { get; set; }
}

/// <summary>
/// Represents a user profile.
/// </summary>
public class Profile
{
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string? Email { get; set; }
}