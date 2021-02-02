namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ServerOptions
    {
        [JsonPropertyName("sendfile")]
        public bool SendFile { get; init; }
    }
}