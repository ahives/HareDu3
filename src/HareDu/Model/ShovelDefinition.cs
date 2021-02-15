namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ShovelDefinition
    {
        [JsonPropertyName("value")]
        public ShovelDefinitionParams Value { get; init; }
    }
}