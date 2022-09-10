namespace HareDu.Model;

using System.Text.Json.Serialization;

public record GlobalParameterRequest
{
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Name { get; init; }
            
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public object Value { get; init; }
}