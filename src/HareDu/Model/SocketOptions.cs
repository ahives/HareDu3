namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the configuration options available for a socket connection.
/// </summary>
public record SocketOptions
{
    /// <summary>
    /// Gets the maximum number of pending connection requests that can be queued for processing by the server.
    /// </summary>
    [JsonPropertyName("backlog")]
    public long Backlog { get; init; }

    /// <summary>
    /// Indicates whether the Nagle algorithm is disabled for the socket, which determines whether
    /// small packets of data are sent immediately or buffered for more efficient transmissions.
    /// </summary>
    [JsonPropertyName("nodelay")]
    public bool NoDelay { get; init; }

    /// <summary>
    /// Gets the configurations related to the linger settings of the socket,
    /// which may influence socket behavior during connection termination.
    /// </summary>
    [JsonPropertyName("linger")]
    public IList<object> Linger { get; init; }

    /// <summary>
    /// Indicates whether the socket connection should automatically terminate when the client closes its connection.
    /// </summary>
    [JsonPropertyName("exit_on_close")]
    public bool ExitOnClose { get; init; }

    /// <summary>
    /// Represents additional server-specific configuration options for the socket.
    /// </summary>
    [JsonPropertyName("cowboy_opts")]
    public ServerOptions ServerOptions { get; init; }
}