namespace HareDu.Model;

using System.Text.Json.Serialization;

public record UserRequest
{
    [JsonPropertyName("password_hash")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string PasswordHash { get; init; }
        
    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Password { get; init; }
        
    [JsonPropertyName("tags")]
    public string Tags { get; init; }
}