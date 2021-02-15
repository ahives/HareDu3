namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record UserDefinition
    {
        [JsonPropertyName("password_hash")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string PasswordHash { get; init; }
        
        [JsonPropertyName("password")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Password { get; init; }
        
        [JsonPropertyName("tags")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Tags { get; init; }
    }
}