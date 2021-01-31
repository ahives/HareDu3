namespace HareDu.Core.Configuration
{
    using System.Text.Json.Serialization;

    public record BrokerCredentials
    {
        [JsonPropertyName("username")]
        public string Username { get; init; }
        
        [JsonPropertyName("password")]
        public string Password { get; init; }
    }
}