namespace HareDu.Model;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information about a RabbitMQ channel.
/// </summary>
public record ChannelInfo
{
    /// <summary>
    /// Provides details about the reductions rate for the channel.
    /// </summary>
    [JsonPropertyName("reductions_details")]
    public Rate ReductionDetails { get; init; }

    /// <summary>
    /// Represents the total number of reductions performed by the channel.
    /// Reductions are a measure of the computational effort used by the channel to process messages.
    /// </summary>
    [JsonPropertyName("reductions")]
    public ulong TotalReductions { get; init; }

    /// <summary>
    /// Represents the virtual host associated with the channel.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the name of the node to which the channel is connected.
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; init; }

    /// <summary>
    /// Represents the username associated with the channel.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }

    /// <summary>
    /// Represents the user responsible for performing the associated action.
    /// </summary>
    [JsonPropertyName("user_who_performed_action")]
    public string UserWhoPerformedAction { get; init; }

    /// <summary>
    /// Represents the timestamp when the channel was connected.
    /// </summary>
    [JsonPropertyName("connected_at")]
    public long ConnectedAt { get; init; }

    /// <summary>
    /// Represents the maximum frame size, in bytes, that the channel can handle.
    /// </summary>
    [JsonPropertyName("frame_max")]
    public ulong FrameMax { get; init; }

    /// <summary>
    /// Represents the duration, in milliseconds, that the channel can remain idle before being closed.
    /// </summary>
    [JsonPropertyName("timeout")]
    public long Timeout { get; init; }

    /// <summary>
    /// Represents the unique identifier of the channel within the system.
    /// </summary>
    [JsonPropertyName("number")]
    public ulong Number { get; init; }

    /// <summary>
    /// Represents the name of the channel.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Represents the communication protocol used by the channel.
    /// </summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; }

    /// <summary>
    /// Represents the hash algorithm used in the SSL/TLS connection for the channel.
    /// </summary>
    [JsonPropertyName("ssl_hash")]
    public string SslHash { get; init; }

    /// <summary>
    /// Represents the SSL cipher used for encrypting the connection.
    /// </summary>
    [JsonPropertyName("ssl_cipher")]
    public string SslCipher { get; init; }

    /// <summary>
    /// Represents the SSL key exchange mechanism used in a connection.
    /// </summary>
    [JsonPropertyName("ssl_key_exchange")]
    public string SslKeyExchange { get; init; }

    /// <summary>
    /// Represents the SSL/TLS protocol that is used for the secure communication of the channel.
    /// </summary>
    [JsonPropertyName("ssl_protocol")]
    public string SslProtocol { get; init; }

    /// <summary>
    /// Represents the authentication mechanism used by the channel.
    /// </summary>
    [JsonPropertyName("auth_mechanism")]
    public string AuthenticationMechanism { get; init; }

    /// <summary>
    /// Represents the validity period of the peer's certificate.
    /// </summary>
    [JsonPropertyName("peer_cert_validity")]
    public string PeerCertificateValidity { get; init; }

    /// <summary>
    /// Represents the issuer of the peer certificate used for the communication channel.
    /// </summary>
    [JsonPropertyName("peer_cert_issuer")]
    public string PeerCertificateIssuer { get; init; }

    /// <summary>
    /// Represents the subject details of the peer certificate used in a connection.
    /// </summary>
    [JsonPropertyName("peer_cert_subject")]
    public string PeerCertificateSubject { get; init; }

    /// <summary>
    /// Indicates whether the channel is using an SSL (Secure Sockets Layer) connection.
    /// </summary>
    [JsonPropertyName("ssl")]
    public bool Ssl { get; init; }

    /// <summary>
    /// Represents the IP address or hostname of the peer connected to the channel.
    /// </summary>
    [JsonPropertyName("peer_host")]
    public string PeerHost { get; init; }

    /// <summary>
    /// Represents the name of the host where the RabbitMQ channel is running.
    /// </summary>
    [JsonPropertyName("host")]
    public string Host { get; init; }

    /// <summary>
    /// Represents the port number on the peer's side of the connection.
    /// </summary>
    [JsonPropertyName("peer_port")]
    public long PeerPort { get; init; }

    /// <summary>
    /// Represents the port used by the channel for communication.
    /// </summary>
    [JsonPropertyName("port")]
    public long Port { get; init; }

    /// <summary>
    /// Represents the type of the channel, which indicates its specific use or configuration.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// Represents details of the connection associated with the channel.
    /// </summary>
    [JsonPropertyName("connection_details")]
    public ConnectionDetails ConnectionDetails { get; init; }

    /// <summary>
    /// Contains information relevant to garbage collection for the channel.
    /// </summary>
    [JsonPropertyName("garbage_collection")]
    public GarbageCollectionDetails GarbageCollectionDetails { get; init; }

    /// <summary>
    /// Represents the current operational state of the channel, indicating whether it is active or idle.
    /// </summary>
    [JsonPropertyName("state")]
    public ChannelState State { get; init; }

    /// <summary>
    /// Represents the total number of channels present in the system.
    /// </summary>
    [JsonPropertyName("channels")]
    public long TotalChannels { get; init; }

    /// <summary>
    /// Represents the number of messages that are pending to be sent on the channel.
    /// </summary>
    [JsonPropertyName("send_pend")]
    public long SentPending { get; init; }

    /// <summary>
    /// Represents the total number of messages the client is allowed to receive across all channels on a connection before acknowledgment is required.
    /// </summary>
    [JsonPropertyName("global_prefetch_count")]
    public uint GlobalPrefetchCount { get; init; }

    /// <summary>
    /// Specifies the number of messages that can be pre-fetched and held in memory before being acknowledged.
    /// </summary>
    [JsonPropertyName("prefetch_count")]
    public uint PrefetchCount { get; init; }

    /// <summary>
    /// Represents the number of acknowledgements in the channel that have been sent by the consumer but not yet confirmed by the server.
    /// </summary>
    [JsonPropertyName("acks_uncommitted")]
    public ulong UncommittedAcknowledgements { get; init; }

    /// <summary>
    /// Represents the count of messages that have been published to the channel
    /// but not yet acknowledged or committed by the consumer.
    /// </summary>
    [JsonPropertyName("messages_uncommitted")]
    public ulong UncommittedMessages { get; init; }

    /// <summary>
    /// Represents the number of messages that have been published to the channel but not yet confirmed by the broker.
    /// </summary>
    [JsonPropertyName("messages_unconfirmed")]
    public ulong UnconfirmedMessages { get; init; }

    /// <summary>
    /// Represents the total number of messages that have been delivered to consumers but not yet acknowledged by them.
    /// </summary>
    [JsonPropertyName("messages_unacknowledged")]
    public ulong UnacknowledgedMessages { get; init; }

    /// <summary>
    /// Represents the total number of consumers associated with the channel.
    /// </summary>
    [JsonPropertyName("consumer_count")]
    public ulong TotalConsumers { get; init; }

    /// <summary>
    /// Indicates whether the channel operates in confirm mode, ensuring that published messages are confirmed by the server.
    /// </summary>
    [JsonPropertyName("confirm")]
    public bool Confirm { get; init; }

    /// <summary>
    /// Indicates whether the channel is operating in transactional mode.
    /// </summary>
    [JsonPropertyName("transactional")]
    public bool Transactional { get; init; }

    /// <summary>
    /// Represents the timestamp indicating when the channel became idle.
    /// </summary>
    [JsonPropertyName("idle_since")]
    public DateTimeOffset IdleSince { get; init; }

    /// <summary>
    /// Represents statistics related to various channel-level operations.
    /// </summary>
    [JsonPropertyName("message_stats")]
    public ChannelOperationStats OperationStats { get; init; }
}