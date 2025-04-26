namespace HareDu.Core.Configuration;

/// <summary>
/// Represents the credentials required to authenticate with a broker.
/// </summary>
public record HareDuCredentials
{
    /// <summary>
    /// Represents the username used for authentication to access the broker.
    /// </summary>
    public string Username { get; init; }

    /// <summary>
    /// Represents the password used for authentication to access the broker.
    /// </summary>
    public string Password { get; init; }
}