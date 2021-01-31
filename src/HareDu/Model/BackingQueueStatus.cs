namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record BackingQueueStatus
    {
        [JsonPropertyName("mode")]
        public string Mode { get; init; }
        
        [JsonPropertyName("q1")]
        public long Q1 { get; init; }
        
        [JsonPropertyName("q2")]
        public long Q2 { get; init; }
        
        [JsonPropertyName("delta")]
        public IList<object> Delta { get; init; }
        
        [JsonPropertyName("q3")]
        public long Q3 { get; init; }
        
        [JsonPropertyName("q4")]
        public long Q4 { get; init; }
        
        [JsonPropertyName("len")]
        public long Length { get; init; }
        
        [JsonPropertyName("target_ram_count")]
        public string TargetTotalMessagesInRAM { get; init; }
        
        [JsonPropertyName("next_seq_id")]
        public long NextSequenceId { get; init; }
        
        [JsonPropertyName("avg_ingress_rate")]
        public decimal AvgIngressRate { get; init; }
        
        [JsonPropertyName("avg_egress_rate")]
        public decimal AvgEgressRate { get; init; }
        
        [JsonPropertyName("avg_ack_ingress_rate")]
        public decimal AvgAcknowledgementIngressRate { get; init; }
        
        [JsonPropertyName("avg_ack_egress_rate")]
        public decimal AvgAcknowledgementEgressRate { get; init; }
    }
}