namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents connection details of a communication entity, providing
/// information about the host, port, and name of the connection.
/// </summary>
public record ConnectionDetails
{
    /// <summary>
    /// Gets the hostname or IP address of the peer connected to the server.
    /// </summary>
    [JsonPropertyName("peer_host")]
    public string PeerHost { get; init; }

    /// <summary>
    /// Gets the port number of the peer connected to the server.
    /// </summary>
    [JsonPropertyName("peer_port")]
    public long PeerPort { get; init; }

    /// <summary>
    /// Gets the name of the connection as specified in the system.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
}