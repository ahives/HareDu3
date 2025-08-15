namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the OAuth2 authentication details retrieved from the broker.
/// </summary>
public record AuthenticationDetails
{
    /// <summary>
    /// Represents whether OAuth authentication is enabled or disabled for the authentication details.
    /// </summary>
    [JsonPropertyName("oauth_enabled")]
    public bool Enabled { get; init; }

    /// <summary>
    /// Represents the Client ID used for OAuth authentication in the authentication details.
    /// </summary>
    [JsonPropertyName("oauth_client_id")]
    public string ClientId { get; init; }

    /// <summary>
    /// Specifies the URL of the OAuth provider associated with the authentication details.
    /// </summary>
    [JsonPropertyName("oauth_provider_url")]
    public string ProviderUrl { get; init; }
}