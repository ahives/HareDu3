namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed information about a specific channel within a messaging system.
/// </summary>
public record ChannelDetails
{
    /// <summary>
    /// Represents the host information of the peer connected to the channel.
    /// </summary>
    [JsonPropertyName("peer_host")]
    public string PeerHost { get; init; }

    /// <summary>
    /// Represents the port number of the peer connected to the channel.
    /// </summary>
    [JsonPropertyName("peer_port")]
    public long PeerPort { get; init; }

    /// <summary>
    /// Represents the unique identifier assigned to a channel within the RabbitMQ broker.
    /// </summary>
    [JsonPropertyName("number")]
    public long Number { get; init; }

    /// <summary>
    /// Represents the name of the channel in the context of RabbitMQ, providing identification details for the channel.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Represents the RabbitMQ node hosting the channel.
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; init; }

    /// <summary>
    /// Represents the name of the connection associated with the channel.
    /// </summary>
    [JsonPropertyName("connection_name")]
    public string ConnectionName { get; init; }

    /// <summary>
    /// Represents the username associated with the channel.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }
}