namespace HareDu.Model;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public record QueueDetailInfo
{
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }

    [JsonPropertyName("auto_delete")]
    public bool AutoDelete { get; init; }

    [JsonPropertyName("consumer_capacity")]
    public int ConsumerCapacity { get; init; }

    [JsonPropertyName("storage_version")]
    public int StorageVersion { get; init; }

    [JsonPropertyName("messages_details")]
    public Rate MessageDetails { get; init; }

    [JsonPropertyName("messages")]
    public ulong TotalMessages { get; init; }

    [JsonPropertyName("messages_unacknowledged_details")]
    public Rate UnacknowledgedMessageDetails { get; init; }

    [JsonPropertyName("messages_unacknowledged")]
    public ulong UnacknowledgedMessages { get; init; }

    [JsonPropertyName("messages_ready_details")]
    public Rate ReadyMessageDetails { get; init; }

    [JsonPropertyName("messages_ready")]
    public ulong ReadyMessages { get; init; }

    [JsonPropertyName("reductions_details")]
    public Rate ReductionDetails { get; init; }

    [JsonPropertyName("reductions")]
    public long TotalReductions { get; init; }

    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; init; }

    [JsonPropertyName("durable")]
    public bool Durable { get; init; }

    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("node")]
    public string Node { get; init; }

    [JsonPropertyName("message_bytes_paged_out")]
    public ulong MessageBytesPagedOut { get; init; }

    [JsonPropertyName("messages_paged_out")]
    public ulong TotalMessagesPagedOut { get; init; }

    [JsonPropertyName("backing_queue_status")]
    public BackingQueueStatus BackingQueueStatus { get; init; }

    [JsonPropertyName("head_message_timestamp")]
    public DateTimeOffset HeadMessageTimestamp { get; init; }

    [JsonPropertyName("message_bytes_persistent")]
    public ulong MessageBytesPersisted { get; init; }

    [JsonPropertyName("message_bytes_ram")]
    public ulong MessageBytesInRAM { get; init; }

    [JsonPropertyName("message_bytes_unacknowledged")]
    public ulong TotalBytesOfMessagesDeliveredButUnacknowledged { get; init; }

    [JsonPropertyName("message_bytes_ready")]
    public ulong TotalBytesOfMessagesReadyForDelivery { get; init; }

    [JsonPropertyName("message_bytes")]
    public ulong TotalBytesOfAllMessages { get; init; }

    [JsonPropertyName("messages_persistent")]
    public ulong MessagesPersisted { get; init; }

    [JsonPropertyName("messages_unacknowledged_ram")]
    public ulong UnacknowledgedMessagesInRAM { get; init; }

    [JsonPropertyName("messages_ready_ram")]
    public ulong MessagesReadyForDeliveryInRAM { get; init; }

    [JsonPropertyName("messages_ram")]
    public ulong MessagesInRAM { get; init; }

    [JsonPropertyName("garbage_collection")]
    public GarbageCollectionDetails GC { get; init; }

    [JsonPropertyName("state")]
    public QueueState State { get; init; }

    [JsonPropertyName("type")]
    public QueueType Type { get; init; }

    [JsonPropertyName("recoverable_slaves")]
    public IList<string> RecoverableSlaves { get; init; }

    [JsonPropertyName("consumers")]
    public ulong Consumers { get; init; }

    [JsonPropertyName("exclusive_consumer_tag")]
    public string ExclusiveConsumerTag { get; init; }

    [JsonPropertyName("operator_policy")]
    public string OperatorPolicy { get; init; }

    [JsonPropertyName("policy")]
    public string Policy { get; init; }

    [JsonPropertyName("consumer_utilisation")]
    public decimal ConsumerUtilization { get; init; }

    [JsonPropertyName("idle_since")]
    public DateTimeOffset IdleSince { get; init; }

    [JsonPropertyName("memory")]
    public ulong Memory { get; init; }

    [JsonPropertyName("message_stats")]
    public QueueMessageStats MessageStats { get; init; }
}