namespace HareDu.Core.Security;

/// <summary>
/// Represents a provider that supplies credentials for connecting to a RabbitMQ server.
/// </summary>
public interface HareDuCredentialProvider
{
    /// <summary>
    /// Specifies the credentials to use when connecting to the RabbitMQ server.
    /// </summary>
    /// <param name="username">The username for authentication.</param>
    /// <param name="password">The password for authentication.</param>
    void UsingCredentials(string username, string password);
}