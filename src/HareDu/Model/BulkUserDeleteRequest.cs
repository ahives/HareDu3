namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record BulkUserDeleteRequest
    {
        [JsonPropertyName("users")]
        public IList<string> Users { get; init; }
    }
}