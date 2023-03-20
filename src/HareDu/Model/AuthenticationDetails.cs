namespace HareDu.Model;

using System.Text.Json.Serialization;

public record AuthenticationDetails
{
    [JsonPropertyName("oauth_enabled")]
    public bool Enabled { get; init; }

    [JsonPropertyName("oauth_client_id")]
    public string ClientId { get; init; }

    [JsonPropertyName("oauth_provider_url")]
    public string ProviderUrl { get; init; }
}