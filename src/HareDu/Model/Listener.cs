namespace HareDu.Model;

using System.Text.Json.Serialization;
using Serialization.Converters;

/// <summary>
/// Represents information related to a listener used by a RabbitMQ node.
/// </summary>
public record Listener
{
    /// <summary>
    /// Gets the name of the RabbitMQ node associated with the listener.
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; init; }

    /// <summary>
    /// Gets the protocol used by the listener.
    /// </summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; }

    /// <summary>
    /// Gets the IP address associated with the listener.
    /// </summary>
    [JsonPropertyName("ip_address")]
    public string IPAddress { get; init; }

    /// <summary>
    /// Gets the port number associated with the listener.
    /// </summary>
    [JsonPropertyName("port")]
    public string Port { get; init; }

    /// <summary>
    /// Gets the socket configuration options associated with the listener.
    /// </summary>
    [JsonPropertyName("socket_opts")]
    [JsonConverter(typeof(InconsistentObjectDataConverter))]
    public SocketOptions SocketOptions { get; init; }
}