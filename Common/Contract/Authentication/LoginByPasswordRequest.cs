namespace Contract.Authentication;

/// <summary>
/// Represents a request for logging in a user using their username and password.
/// </summary>
public class LoginByPasswordRequest
{
    /// <summary>
    /// The username of the user attempting to log in.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The password of the user attempting to log in.
    /// </summary>
    public required string Password { get; set; }
}
