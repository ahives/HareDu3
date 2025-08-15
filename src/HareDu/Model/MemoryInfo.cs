namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents memory usage information for different components and processes.
/// </summary>
public record MemoryInfo
{
    /// <summary>
    /// Represents the memory allocated for connection readers in the system.
    /// </summary>
    [JsonPropertyName("connection_readers")]
    public long ConnectionReaders { get; init; }

    /// <summary>
    /// Represents the memory allocated for connection writers in the system.
    /// </summary>
    [JsonPropertyName("connection_writers")]
    public long ConnectionWriters { get; init; }

    /// <summary>
    /// Represents the memory allocated for connection channels in the system.
    /// </summary>
    [JsonPropertyName("connection_channels")]
    public long ConnectionChannels { get; init; }

    /// <summary>
    /// Represents the memory allocated for other connection-related processes in the system.
    /// </summary>
    [JsonPropertyName("connection_other")]
    public long ConnectionOther { get; init; }

    /// <summary>
    /// Represents the memory allocated for queue processes in the system.
    /// </summary>
    [JsonPropertyName("queue_procs")]
    public long QueueProcesses { get; init; }

    /// <summary>
    /// Represents the memory allocated for queue slave processes in the system.
    /// </summary>
    [JsonPropertyName("queue_slave_procs")]
    public long QueueSlaveProcesses { get; init; }

    /// <summary>
    /// Represents the memory allocated for plugins in the system.
    /// </summary>
    [JsonPropertyName("plugins")]
    public long Plugins { get; init; }

    /// <summary>
    /// Represents the memory consumed by other processes within the system that are not specifically categorized.
    /// </summary>
    [JsonPropertyName("other_proc")]
    public long OtherProcesses { get; init; }

    /// <summary>
    /// Represents the memory allocated for metrics in the system.
    /// </summary>
    [JsonPropertyName("metrics")]
    public long Metrics { get; init; }

    /// <summary>
    /// Represents the memory allocated for the management database within the system.
    /// </summary>
    [JsonPropertyName("mgmt_db")]
    public long ManagementDatabase { get; init; }

    /// <summary>
    /// Represents the memory used by the Mnesia database within the system.
    /// </summary>
    [JsonPropertyName("mnesia")]
    public long Mnesia { get; init; }

    /// <summary>
    /// Represents memory allocated for miscellaneous in-memory storage mechanisms in the system.
    /// </summary>
    [JsonPropertyName("other_ets")]
    public long OtherInMemoryStorage { get; init; }

    /// <summary>
    /// Represents the amount of memory allocated for binary data within the system.
    /// </summary>
    [JsonPropertyName("binary")]
    public long Binary { get; init; }

    /// <summary>
    /// Represents the memory used for the message index in the system.
    /// </summary>
    [JsonPropertyName("msg_index")]
    public long MessageIndex { get; init; }

    /// <summary>
    /// Represents the memory allocated for bytecode within the system.
    /// </summary>
    [JsonPropertyName("code")]
    public long ByteCode { get; init; }

    /// <summary>
    /// Represents the memory allocated for atom structures in the system.
    /// </summary>
    [JsonPropertyName("atom")]
    public long Atom { get; init; }

    /// <summary>
    /// Represents the memory allocated for other system-related processes within the system.
    /// </summary>
    [JsonPropertyName("other_system")]
    public long OtherSystem { get; init; }

    /// <summary>
    /// Represents the amount of memory that has been allocated but remains unused.
    /// </summary>
    [JsonPropertyName("allocated_unused")]
    public long AllocatedUnused { get; init; }

    /// <summary>
    /// Represents the amount of memory that has been reserved but not allocated for use.
    /// </summary>
    [JsonPropertyName("reserved_unallocated")]
    public long ReservedUnallocated { get; init; }

    /// <summary>
    /// Represents the memory allocation strategy used by the system.
    /// </summary>
    [JsonPropertyName("strategy")]
    public string Strategy { get; init; }

    /// <summary>
    /// Represents the aggregate memory usage data in the system.
    /// </summary>
    [JsonPropertyName("total")]
    public TotalMemoryInfo Total { get; init; }

    /// <summary>
    /// Represents the memory allocated for quorum queue processes in the system.
    /// </summary>
    [JsonPropertyName("quorum_queue_procs")]
    public long QuorumQueueProcesses { get; init; }

    /// <summary>
    /// Represents the memory allocated for storing in-memory quorum queue data.
    /// </summary>
    [JsonPropertyName("quorum_ets")]
    public long QuorumInMemoryStorage { get; init; }
}