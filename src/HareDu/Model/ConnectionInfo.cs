namespace HareDu.Model;

using System.Text.Json.Serialization;

public record ConnectionInfo
{
    [JsonPropertyName("reductions_details")]
    public Rate ReductionDetails { get; init; }
        
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; }

    [JsonPropertyName("reductions")]
    public ulong TotalReductions { get; init; }

    [JsonPropertyName("recv_cnt")]
    public ulong PacketsReceived { get; init; }

    [JsonPropertyName("recv_oct")]
    public ulong PacketBytesReceived { get; init; }

    [JsonPropertyName("recv_oct_details")]
    public Rate PacketBytesReceivedDetails { get; init; }

    [JsonPropertyName("send_cnt")]
    public ulong PacketsSent { get; init; }

    [JsonPropertyName("send_oct")]
    public ulong PacketBytesSent { get; init; }

    [JsonPropertyName("send_oct_details")]
    public Rate PacketBytesSentDetails { get; init; }

    [JsonPropertyName("connected_at")]
    public long ConnectedAt { get; init; }

    [JsonPropertyName("channel_max")]
    public ulong OpenChannelsLimit { get; init; }

    [JsonPropertyName("frame_max")]
    public ulong MaxFrameSizeInBytes { get; init; }

    [JsonPropertyName("timeout")]
    public long ConnectionTimeout { get; init; }

    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("channels")]
    public ulong Channels { get; init; }

    [JsonPropertyName("send_pend")]
    public ulong SendPending { get; init; }

    [JsonPropertyName("type")]
    public ConnectionType Type { get; init; }

    [JsonPropertyName("garbage_collection")]
    public GarbageCollectionDetails GarbageCollectionDetails { get; init; }

    [JsonPropertyName("state")]
    public BrokerConnectionState State { get; init; }

    [JsonPropertyName("ssl_hash")]
    public string SslHashFunction { get; init; }

    [JsonPropertyName("ssl_cipher")]
    public string SslCipherAlgorithm { get; init; }

    [JsonPropertyName("ssl_key_exchange")]
    public string SslKeyExchangeAlgorithm { get; init; }

    [JsonPropertyName("ssl_protocol")]
    public string SslProtocol { get; init; }

    [JsonPropertyName("auth_mechanism")]
    public string AuthenticationMechanism { get; init; }

    [JsonPropertyName("peer_cert_validity")]
    public string TimePeriodPeerCertificateValid { get; init; }

    [JsonPropertyName("peer_cert_issuer")]
    public string PeerCertificateIssuer { get; init; }

    [JsonPropertyName("peer_cert_subject")]
    public string PeerCertificateSubject { get; init; }

    [JsonPropertyName("ssl")]
    public bool IsSsl { get; init; }

    [JsonPropertyName("peer_host")]
    public string PeerHost { get; init; }

    [JsonPropertyName("host")]
    public string Host { get; init; }

    [JsonPropertyName("peer_port")]
    public long PeerPort { get; init; }

    [JsonPropertyName("port")]
    public long Port { get; init; }

    [JsonPropertyName("node")]
    public string Node { get; init; }

    [JsonPropertyName("user")]
    public string User { get; init; }

    [JsonPropertyName("user_who_performed_action")]
    public string UserWhoPerformedAction { get; init; }

    [JsonPropertyName("client_properties")]
    public ConnectionClientProperties ConnectionClientProperties { get; init; }
}