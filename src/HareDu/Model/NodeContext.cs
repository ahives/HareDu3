namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a context in which a node operates. It provides information about the specific configuration
/// and settings associated with a RabbitMQ node context.
/// </summary>
public record NodeContext
{
    /// <summary>
    /// Provides a descriptive text associated with the node context.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Represents the path associated with the node context.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; init; }

    /// <summary>
    /// Represents the port associated with the node context.
    /// </summary>
    [JsonPropertyName("port")]
    public string Port { get; init; }

    /// <summary>
    /// Represents the server options associated with the node context.
    /// </summary>
    [JsonPropertyName("cowboy_opts")]
    public string ServerOptions { get; init; }

    /// <summary>
    /// Represents the list of SSL options associated with the node context.
    /// </summary>
    [JsonPropertyName("ssl_opts")]
    public IList<string> SslOptions { get; init; }

    /// <summary>
    /// Represents the name of the node in the context.
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; init; }
}