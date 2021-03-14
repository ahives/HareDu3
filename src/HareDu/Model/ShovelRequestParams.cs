namespace HareDu.Model
{
    using System.Text.Json.Serialization;
    using Serialization;

    public record ShovelRequestParams
    {
        [JsonPropertyName("src-protocol")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ShovelProtocolType SourceProtocol { get; init; }
        
        [JsonPropertyName("src-uri")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string SourceUri { get; init; }
        
        [JsonPropertyName("src-queue")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string SourceQueue { get; init; }

        [JsonPropertyName("dest-protocol")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ShovelProtocolType DestinationProtocol { get; init; }
        
        [JsonPropertyName("dest-uri")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DestinationUri { get; init; }
        
        [JsonPropertyName("dest-queue")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DestinationQueue { get; init; }
        
        [JsonPropertyName("reconnect-delay")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ReconnectDelay { get; init; }
        
        [JsonPropertyName("ack-mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonConverter(typeof(AckModeEnumConverter))]
        public AckMode AcknowledgeMode { get; init; }
        
        [JsonPropertyName("src-delete-after")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object SourceDeleteAfter { get; init; }
        
        [JsonPropertyName("src-prefetch-count")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ulong SourcePrefetchCount { get; init; }
        
        [JsonPropertyName("src-exchange")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string SourceExchange { get; init; }
        
        [JsonPropertyName("src-exchange-key")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string SourceExchangeRoutingKey { get; init; }
        
        [JsonPropertyName("dest-exchange")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DestinationExchange { get; init; }
        
        [JsonPropertyName("dest-exchange-key")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DestinationExchangeKey { get; init; }
        
        [JsonPropertyName("dest-publish-properties")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DestinationPublishProperties { get; init; }
        
        [JsonPropertyName("dest-add-forward-headers")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool DestinationAddForwardHeaders { get; init; }
        
        [JsonPropertyName("dest-add-timestamp-header")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool DestinationAddTimestampHeader { get; init; }
    }
}