namespace HareDu.Model
{
    using System;
    using System.Text.Json.Serialization;

    public record ChannelInfo
    {
        [JsonPropertyName("reductions_details")]
        public Rate ReductionDetails { get; init; }

        [JsonPropertyName("reductions")]
        public ulong TotalReductions { get; init; }

        [JsonPropertyName("vhost")]
        public string VirtualHost { get; init; }

        [JsonPropertyName("node")]
        public string Node { get; init; }

        [JsonPropertyName("user")]
        public string User { get; init; }

        [JsonPropertyName("user_who_performed_action")]
        public string UserWhoPerformedAction { get; init; }

        [JsonPropertyName("connected_at")]
        public long ConnectedAt { get; init; }

        [JsonPropertyName("frame_max")]
        public ulong FrameMax { get; init; }

        [JsonPropertyName("timeout")]
        public long Timeout { get; init; }

        [JsonPropertyName("number")]
        public ulong Number { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("protocol")]
        public string Protocol { get; init; }

        [JsonPropertyName("ssl_hash")]
        public string SslHash { get; init; }

        [JsonPropertyName("ssl_cipher")]
        public string SslCipher { get; init; }

        [JsonPropertyName("ssl_key_exchange")]
        public string SslKeyExchange { get; init; }

        [JsonPropertyName("ssl_protocol")]
        public string SslProtocol { get; init; }

        [JsonPropertyName("auth_mechanism")]
        public string AuthenticationMechanism { get; init; }

        [JsonPropertyName("peer_cert_validity")]
        public string PeerCertificateValidity { get; init; }

        [JsonPropertyName("peer_cert_issuer")]
        public string PeerCertificateIssuer { get; init; }

        [JsonPropertyName("peer_cert_subject")]
        public string PeerCertificateSubject { get; init; }

        [JsonPropertyName("ssl")]
        public bool Ssl { get; init; }

        [JsonPropertyName("peer_host")]
        public string PeerHost { get; init; }

        [JsonPropertyName("host")]
        public string Host { get; init; }

        [JsonPropertyName("peer_port")]
        public long PeerPort { get; init; }

        [JsonPropertyName("port")]
        public long Port { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("connection_details")]
        public ConnectionDetails ConnectionDetails { get; init; }

        [JsonPropertyName("garbage_collection")]
        public GarbageCollectionDetails GarbageCollectionDetails { get; init; }

        [JsonPropertyName("state")]
        public ChannelState State { get; init; }

        [JsonPropertyName("channels")]
        public long TotalChannels { get; init; }

        [JsonPropertyName("send_pend")]
        public long SentPending { get; init; }

        [JsonPropertyName("global_prefetch_count")]
        public uint GlobalPrefetchCount { get; init; }

        [JsonPropertyName("prefetch_count")]
        public uint PrefetchCount { get; init; }

        [JsonPropertyName("acks_uncommitted")]
        public ulong UncommittedAcknowledgements { get; init; }

        [JsonPropertyName("messages_uncommitted")]
        public ulong UncommittedMessages { get; init; }

        [JsonPropertyName("messages_unconfirmed")]
        public ulong UnconfirmedMessages { get; init; }

        [JsonPropertyName("messages_unacknowledged")]
        public ulong UnacknowledgedMessages { get; init; }

        [JsonPropertyName("consumer_count")]
        public ulong TotalConsumers { get; init; }

        [JsonPropertyName("confirm")]
        public bool Confirm { get; init; }

        [JsonPropertyName("transactional")]
        public bool Transactional { get; init; }

        [JsonPropertyName("idle_since")]
        public DateTimeOffset IdleSince { get; init; }
        
        [JsonPropertyName("message_stats")]
        public ChannelOperationStats OperationStats { get; init; }
    }
}