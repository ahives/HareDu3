namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record UserInfo
    {
        [JsonPropertyName("name")]
        public string Username { get; init; }
        
        [JsonPropertyName("password_hash")]
        public string PasswordHash { get; init; }
        
        [JsonPropertyName("hashing_algorithm")]
        public string HashingAlgorithm { get; init; }
        
        [JsonPropertyName("tags")]
        public string Tags { get; init; }
    }
}