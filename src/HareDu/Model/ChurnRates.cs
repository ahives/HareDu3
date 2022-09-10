namespace HareDu.Model;

using System.Text.Json.Serialization;

public record ChurnRates
{
    [JsonPropertyName("channel_closed")]
    public ulong TotalChannelsClosed { get; init; }
        
    [JsonPropertyName("channel_closed_details")]
    public Rate ClosedChannelDetails { get; init; }

    [JsonPropertyName("channel_created")]
    public ulong TotalChannelsCreated { get; init; }
        
    [JsonPropertyName("channel_created_details")]
    public Rate CreatedChannelDetails { get; init; }

    [JsonPropertyName("connection_closed")]
    public ulong TotalConnectionsClosed { get; init; }
        
    [JsonPropertyName("connection_closed_details")]
    public Rate ClosedConnectionDetails { get; init; }

    [JsonPropertyName("connection_created")]
    public ulong TotalConnectionsCreated { get; init; }
        
    [JsonPropertyName("connection_created_details")]
    public Rate CreatedConnectionDetails { get; init; }

    [JsonPropertyName("queue_created")]
    public ulong TotalQueuesCreated { get; init; }
        
    [JsonPropertyName("queue_created_details")]
    public Rate CreatedQueueDetails { get; init; }

    [JsonPropertyName("queue_declared")]
    public ulong TotalQueuesDeclared { get; init; }
        
    [JsonPropertyName("queue_declared_details")]
    public Rate DeclaredQueueDetails { get; init; }

    [JsonPropertyName("queue_deleted")]
    public ulong TotalQueuesDeleted { get; init; }
        
    [JsonPropertyName("queue_deleted_details")]
    public Rate DeletedQueueDetails { get; init; }
}