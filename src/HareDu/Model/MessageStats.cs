namespace HareDu.Model;

using System.Text.Json.Serialization;

public record MessageStats
{
    [JsonPropertyName("publish")]
    public ulong TotalMessagesPublished { get; init; }

    [JsonPropertyName("publish_details")]
    public Rate MessagesPublishedDetails { get; init; }
        
    [JsonPropertyName("confirm")]
    public ulong TotalMessagesConfirmed { get; init; }

    [JsonPropertyName("confirm_details")]
    public Rate MessagesConfirmedDetails { get; init; }
        
    [JsonPropertyName("return_unroutable")]
    public ulong TotalUnroutableMessages { get; init; }

    [JsonPropertyName("return_unroutable_details")]
    public Rate UnroutableMessagesDetails { get; init; }
        
    [JsonPropertyName("disk_reads")]
    public ulong TotalDiskReads { get; init; }

    [JsonPropertyName("disk_reads_details")]
    public Rate DiskReadDetails { get; init; }
        
    [JsonPropertyName("disk_writes")]
    public ulong TotalDiskWrites { get; init; }

    [JsonPropertyName("disk_writes_details")]
    public Rate DiskWriteDetails { get; init; }
        
    [JsonPropertyName("get")]
    public ulong TotalMessageGets { get; init; }

    [JsonPropertyName("get_details")]
    public Rate MessageGetDetails { get; init; }
        
    [JsonPropertyName("get_no_ack")]
    public ulong TotalMessageGetsWithoutAck { get; init; }

    [JsonPropertyName("get_no_ack_details")]
    public Rate MessageGetsWithoutAckDetails { get; init; }
        
    [JsonPropertyName("deliver")]
    public ulong TotalMessagesDelivered { get; init; }

    [JsonPropertyName("deliver_details")]
    public Rate MessageDeliveryDetails { get; init; }
        
    [JsonPropertyName("deliver_no_ack")]
    public ulong TotalMessageDeliveredWithoutAck { get; init; }

    [JsonPropertyName("deliver_no_ack_details")]
    public Rate MessagesDeliveredWithoutAckDetails { get; init; }
        
    [JsonPropertyName("redeliver")]
    public ulong TotalMessagesRedelivered { get; init; }

    [JsonPropertyName("redeliver_details")]
    public Rate MessagesRedeliveredDetails { get; init; }
        
    [JsonPropertyName("ack")]
    public ulong TotalMessagesAcknowledged { get; init; }

    [JsonPropertyName("ack_details")]
    public Rate MessagesAcknowledgedDetails { get; init; }
        
    [JsonPropertyName("deliver_get")]
    public ulong TotalMessageDeliveryGets { get; init; }

    [JsonPropertyName("deliver_get_details")]
    public Rate MessageDeliveryGetDetails { get; init; }
}