namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record UserDefinition
    {
        [JsonPropertyName("password_hash")]
        public string PasswordHash { get; init; }
        
        [JsonPropertyName("password")]
        public string Password { get; init; }
        
        [JsonPropertyName("tags")]
        public string Tags { get; init; }
    }
}