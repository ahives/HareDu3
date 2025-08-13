namespace HareDu;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the configuration options available for a server.
/// </summary>
public record ServerOptions
{
    /// <summary>
    /// Indicates whether the server supports the use of the sendfile system call to optimize file transfers.
    /// </summary>
    [JsonPropertyName("sendfile")]
    public bool SendFile { get; init; }
}