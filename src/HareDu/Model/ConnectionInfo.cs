namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed information about a connection in the broker.
/// </summary>
public record ConnectionInfo
{
    /// <summary>
    /// Represents the rate details for reductions performed on the connection.
    /// </summary>
    [JsonPropertyName("reductions_details")]
    public Rate ReductionDetails { get; init; }

    /// <summary>
    /// Denotes the protocol used for the connection.
    /// </summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; }

    /// <summary>
    /// Represents the total number of reductions performed by the connection.
    /// </summary>
    [JsonPropertyName("reductions")]
    public ulong TotalReductions { get; init; }

    /// <summary>
    /// Represents the total number of packets received for the connection.
    /// </summary>
    [JsonPropertyName("recv_cnt")]
    public ulong PacketsReceived { get; init; }

    /// <summary>
    /// Represents the total number of bytes received over the connection.
    /// </summary>
    [JsonPropertyName("recv_oct")]
    public ulong PacketBytesReceived { get; init; }

    /// <summary>
    /// Represents the rate details for the total number of packet bytes received on the connection.
    /// </summary>
    [JsonPropertyName("recv_oct_details")]
    public Rate PacketBytesReceivedDetails { get; init; }

    /// <summary>
    /// Represents the total number of packets sent over the connection.
    /// </summary>
    [JsonPropertyName("send_cnt")]
    public ulong PacketsSent { get; init; }

    /// <summary>
    /// Represents the total number of bytes sent in packets over the connection.
    /// </summary>
    [JsonPropertyName("send_oct")]
    public ulong PacketBytesSent { get; init; }

    /// <summary>
    /// Provides details about the rate of bytes sent in packets over the connection.
    /// </summary>
    [JsonPropertyName("send_oct_details")]
    public Rate PacketBytesSentDetails { get; init; }

    /// <summary>
    /// Indicates the timestamp, in milliseconds since the Unix epoch, at which the connection was established.
    /// </summary>
    [JsonPropertyName("connected_at")]
    public long ConnectedAt { get; init; }

    /// <summary>
    /// Specifies the maximum number of channels that can be open on a connection.
    /// </summary>
    [JsonPropertyName("channel_max")]
    public ulong OpenChannelsLimit { get; init; }

    /// <summary>
    /// Specifies the maximum size, in bytes, of a frame that can be transmitted over the connection.
    /// </summary>
    [JsonPropertyName("frame_max")]
    public ulong MaxFrameSizeInBytes { get; init; }

    /// <summary>
    /// Specifies the timeout duration, in milliseconds, for the connection before it is terminated.
    /// </summary>
    [JsonPropertyName("timeout")]
    public long ConnectionTimeout { get; init; }

    /// <summary>
    /// Represents the virtual host associated with the connection.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the name of the connection.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Represents the total number of channels currently associated with the connection.
    /// </summary>
    [JsonPropertyName("channels")]
    public ulong Channels { get; init; }

    /// <summary>
    /// Represents the number of bytes queued to be sent for the connection.
    /// </summary>
    [JsonPropertyName("send_pend")]
    public ulong SendPending { get; init; }

    /// <summary>
    /// Represents the type of the connection.
    /// </summary>
    [JsonPropertyName("type")]
    public ConnectionType Type { get; init; }

    /// <summary>
    /// Represents details about the garbage collection statistics and configuration settings for a connection.
    /// </summary>
    [JsonPropertyName("garbage_collection")]
    public GarbageCollectionDetails GarbageCollectionDetails { get; init; }

    /// <summary>
    /// Represents the current state of the broker connection.
    /// </summary>
    [JsonPropertyName("state")]
    public BrokerConnectionState State { get; init; }

    /// <summary>
    /// Represents the hash function used in the SSL connection.
    /// </summary>
    [JsonPropertyName("ssl_hash")]
    public string SslHashFunction { get; init; }

    /// <summary>
    /// Specifies the SSL cipher algorithm used for securing the connection.
    /// </summary>
    [JsonPropertyName("ssl_cipher")]
    public string SslCipherAlgorithm { get; init; }

    /// <summary>
    /// Specifies the SSL key exchange algorithm used in the connection.
    /// </summary>
    [JsonPropertyName("ssl_key_exchange")]
    public string SslKeyExchangeAlgorithm { get; init; }

    /// <summary>
    /// Specifies the SSL protocol used for the connection.
    /// </summary>
    [JsonPropertyName("ssl_protocol")]
    public string SslProtocol { get; init; }

    /// <summary>
    /// Specifies the authentication mechanism used during the connection.
    /// </summary>
    [JsonPropertyName("auth_mechanism")]
    public string AuthenticationMechanism { get; init; }

    /// <summary>
    /// Represents the validity duration of the peer certificate within the connection.
    /// </summary>
    [JsonPropertyName("peer_cert_validity")]
    public string TimePeriodPeerCertificateValid { get; init; }

    /// <summary>
    /// Represents the issuer of the peer's certificate as part of the connection information.
    /// </summary>
    [JsonPropertyName("peer_cert_issuer")]
    public string PeerCertificateIssuer { get; init; }

    /// <summary>
    /// Represents the subject field of the peer's certificate in the connection.
    /// </summary>
    [JsonPropertyName("peer_cert_subject")]
    public string PeerCertificateSubject { get; init; }

    /// <summary>
    /// Indicates whether the connection is secured using SSL/TLS.
    /// </summary>
    [JsonPropertyName("ssl")]
    public bool IsSsl { get; init; }

    /// <summary>
    /// Represents the hostname of the peer connected to the server in a network connection.
    /// </summary>
    [JsonPropertyName("peer_host")]
    public string PeerHost { get; init; }

    /// <summary>
    /// Represents the name of the host associated with the connection.
    /// </summary>
    [JsonPropertyName("host")]
    public string Host { get; init; }

    /// <summary>
    /// Represents the port number associated with the peer in the connection.
    /// </summary>
    [JsonPropertyName("peer_port")]
    public long PeerPort { get; init; }

    /// <summary>
    /// Specifies the port used for the connection to the RabbitMQ broker.
    /// </summary>
    [JsonPropertyName("port")]
    public long Port { get; init; }

    /// <summary>
    /// Represents the RabbitMQ broker node associated with the connection.
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; init; }

    /// <summary>
    /// Represents the username associated with the connection.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }

    /// <summary>
    /// Represents the username of the user who performed the action associated with the connection.
    /// </summary>
    [JsonPropertyName("user_who_performed_action")]
    public string UserWhoPerformedAction { get; init; }

    /// <summary>
    /// Represents the properties of a client in a connection.
    /// </summary>
    [JsonPropertyName("client_properties")]
    public ConnectionClientProperties ConnectionClientProperties { get; init; }
}