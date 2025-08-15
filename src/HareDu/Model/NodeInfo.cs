namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information and statistics about a node in the system.
/// </summary>
public record NodeInfo
{
    /// <summary>
    /// Contains the list of partitions associated with the node.
    /// </summary>
    [JsonPropertyName("partitions")]
    public IList<string> Partitions { get; init; }

    /// <summary>
    /// Represents the operating system process identifier associated with the node.
    /// </summary>
    [JsonPropertyName("os_pid")]
    public string OperatingSystemProcessId { get; init; }

    /// <summary>
    /// Represents the total number of file descriptors available on the node.
    /// </summary>
    [JsonPropertyName("fd_total")]
    public ulong TotalFileDescriptors { get; init; }

    /// <summary>
    /// Represents the total number of sockets available on the node.
    /// </summary>
    [JsonPropertyName("sockets_total")]
    public ulong TotalSocketsAvailable { get; init; }

    /// <summary>
    /// Represents the memory limit configured for the node.
    /// </summary>
    [JsonPropertyName("mem_limit")]
    public ulong MemoryLimit { get; init; }

    /// <summary>
    /// Indicates whether a memory alarm is active on the node.
    /// </summary>
    [JsonPropertyName("mem_alarm")]
    public bool MemoryAlarm { get; init; }

    /// <summary>
    /// Represents the minimum amount of free disk space, in bytes, that must be maintained on the node.
    /// </summary>
    [JsonPropertyName("disk_free_limit")]
    public ulong FreeDiskLimit { get; init; }

    /// <summary>
    /// Indicates whether the free disk space alarm is active for the node.
    /// </summary>
    [JsonPropertyName("disk_free_alarm")]
    public bool FreeDiskAlarm { get; init; }

    /// <summary>
    /// Represents the total number of processes available in the node.
    /// </summary>
    [JsonPropertyName("proc_total")]
    public ulong TotalProcesses { get; init; }

    /// <summary>
    /// Represents the mode of rate reporting for the node, indicating the level of detail (e.g., None, Basic, or Detailed).
    /// </summary>
    [JsonPropertyName("rates_mode")]
    public RatesMode RatesMode { get; init; }

    /// <summary>
    /// Represents the total duration in milliseconds that the node has been operational.
    /// </summary>
    [JsonPropertyName("uptime")]
    public long Uptime { get; init; }

    /// <summary>
    /// Represents the total number of processes currently waiting to run on the node's scheduler.
    /// </summary>
    [JsonPropertyName("run_queue")]
    public int RunQueue { get; init; }

    /// <summary>
    /// Represents the type of the node.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// Represents the number of processor cores detected on the system.
    /// </summary>
    [JsonPropertyName("processors")]
    public uint AvailableCoresDetected { get; init; }

    /// <summary>
    /// Represents a collection of exchange types associated with the node.
    /// </summary>
    [JsonPropertyName("exchange_types")]
    public IList<ExchangeType> ExchangeTypes { get; init; }

    /// <summary>
    /// Represents the list of authentication mechanisms supported by the node.
    /// </summary>
    [JsonPropertyName("auth_mechanisms")]
    public IList<AuthenticationMechanism> AuthenticationMechanisms { get; init; }

    /// <summary>
    /// Represents the collection of applications associated with the node.
    /// </summary>
    [JsonPropertyName("applications")]
    public IList<Application> Applications { get; init; }

    /// <summary>
    /// Represents the list of contexts associated with the node.
    /// </summary>
    [JsonPropertyName("contexts")]
    public IList<NodeContext> Contexts { get; init; }

    /// <summary>
    /// Specifies the file path for the log file associated with the node.
    /// </summary>
    [JsonPropertyName("log_file")]
    public string LogFile { get; init; }

    /// <summary>
    /// Represents the collection of log file paths associated with the node.
    /// </summary>
    [JsonPropertyName("log_files")]
    public IList<string> LogFiles { get; init; }

    /// <summary>
    /// Represents the path to the SASL log file associated with the node.
    /// </summary>
    [JsonPropertyName("sasl_log_file")]
    public string SaslLogFile { get; init; }

    /// <summary>
    /// Represents the directory path where the database is stored for the node.
    /// </summary>
    [JsonPropertyName("db_dir")]
    public string DatabaseDirectory { get; init; }

    /// <summary>
    /// Represents the collection of configuration file paths associated with the node.
    /// </summary>
    [JsonPropertyName("config_files")]
    public IList<string> ConfigFiles { get; init; }

    /// <summary>
    /// Represents the interval in seconds for network connection tick time on the node.
    /// </summary>
    [JsonPropertyName("net_ticktime")]
    public long NetworkTickTime { get; init; }

    /// <summary>
    /// Specifies the list of plugins that are enabled on the node.
    /// </summary>
    [JsonPropertyName("enabled_plugins")]
    public IList<string> EnabledPlugins { get; init; }

    /// <summary>
    /// Defines the strategy used to calculate memory allocation on the node.
    /// </summary>
    [JsonPropertyName("mem_calculation_strategy")]
    public string MemoryCalculationStrategy { get; init; }

    /// <summary>
    /// Represents the name of the node in the system.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Indicates whether the node is currently running.
    /// </summary>
    [JsonPropertyName("running")]
    public bool IsRunning { get; init; }

    /// <summary>
    /// Indicates the total amount of memory, in bytes, currently being used by the node.
    /// </summary>
    [JsonPropertyName("mem_used")]
    public ulong MemoryUsed { get; init; }

    /// <summary>
    /// Represents detailed information about the memory usage rate on the node.
    /// </summary>
    [JsonPropertyName("mem_used_details")]
    public Rate MemoryUsageDetails { get; init; }

    /// <summary>
    /// Represents the number of file descriptors currently in use by the node.
    /// </summary>
    [JsonPropertyName("fd_used")]
    public ulong FileDescriptorUsed { get; init; }

    /// <summary>
    /// Provides rate details regarding the usage of file descriptors on the node.
    /// </summary>
    [JsonPropertyName("fd_used_details")]
    public Rate FileDescriptorUsedDetails { get; init; }

    /// <summary>
    /// Represents the number of sockets currently in use on the node.
    /// </summary>
    [JsonPropertyName("sockets_used")]
    public ulong SocketsUsed { get; init; }

    /// <summary>
    /// Provides rate details regarding the usage of sockets on the node.
    /// </summary>
    [JsonPropertyName("sockets_used_details")]
    public Rate SocketsUsedDetails { get; init; }

    /// <summary>
    /// Represents the total number of processes currently in use on the node.
    /// </summary>
    [JsonPropertyName("proc_used")]
    public ulong ProcessesUsed { get; init; }

    /// <summary>
    /// Provides rate details regarding the usage of processes on the node.
    /// </summary>
    [JsonPropertyName("proc_used_details")]
    public Rate ProcessUsageDetails { get; init; }

    /// <summary>
    /// Represents the amount of free disk space available on the node, measured in bytes.
    /// </summary>
    [JsonPropertyName("disk_free")]
    public ulong FreeDiskSpace { get; init; }

    /// <summary>
    /// Represents rate details regarding the available free disk space on the node.
    /// </summary>
    [JsonPropertyName("disk_free_details")]
    public Rate FreeDiskSpaceDetails { get; init; }

    /// <summary>
    /// Represents the total number of garbage collections that have occurred on the node.
    /// </summary>
    [JsonPropertyName("gc_num")]
    public ulong NumberOfGarbageCollected { get; init; }

    /// <summary>
    /// Represents detailed rate metrics related to garbage collection activity on the node.
    /// </summary>
    [JsonPropertyName("gc_num_details")]
    public Rate GcDetails { get; init; }

    /// <summary>
    /// Represents the total number of bytes reclaimed by the garbage collector on the node.
    /// </summary>
    [JsonPropertyName("gc_bytes_reclaimed")]
    public ulong BytesReclaimedByGarbageCollector { get; init; }

    /// <summary>
    /// Provides rate details regarding the number of bytes reclaimed by garbage collection on the node.
    /// </summary>
    [JsonPropertyName("gc_bytes_reclaimed_details")]
    public Rate ReclaimedBytesFromGCDetails { get; init; }

    /// <summary>
    /// Represents the total number of context switches that have occurred on the node.
    /// </summary>
    [JsonPropertyName("context_switches")]
    public ulong ContextSwitches { get; init; }

    /// <summary>
    /// Represents rate information regarding the context switches occurring on the node.
    /// </summary>
    [JsonPropertyName("context_switches_details")]
    public Rate ContextSwitchDetails { get; init; }

    /// <summary>
    /// Indicates the total number of I/O read operations performed on the node.
    /// </summary>
    [JsonPropertyName("io_read_count")]
    public ulong TotalIOReads { get; init; }

    /// <summary>
    /// Provides detailed rate information about I/O read operations on the node.
    /// </summary>
    [JsonPropertyName("io_read_count_details")]
    public Rate IOReadDetails { get; init; }

    /// <summary>
    /// Represents the total number of bytes read through I/O operations on the node.
    /// </summary>
    [JsonPropertyName("io_read_bytes")]
    public ulong TotalIOBytesRead { get; init; }

    /// <summary>
    /// Represents the rate details for the number of bytes read during I/O operations on the node.
    /// </summary>
    [JsonPropertyName("io_read_bytes_details")]
    public Rate IOBytesReadDetails { get; init; }

    /// <summary>
    /// Represents the average time taken for I/O read operations on the node, measured in milliseconds.
    /// </summary>
    [JsonPropertyName("io_read_avg_time")]
    public decimal AvgIOReadTime { get; init; }

    /// <summary>
    /// Contains detailed information about the rate of average I/O read operation times on the node.
    /// </summary>
    [JsonPropertyName("io_read_avg_time_details")]
    public Rate AvgIOReadTimeDetails { get; init; }

    /// <summary>
    /// Represents the total number of I/O write operations performed on the node.
    /// </summary>
    [JsonPropertyName("io_write_count")]
    public ulong TotalIOWrites { get; init; }

    /// <summary>
    /// Contains detailed rate information regarding I/O write operations on the node.
    /// </summary>
    [JsonPropertyName("io_write_count_details")]
    public Rate IOWriteDetails { get; init; }

    /// <summary>
    /// Represents the total number of bytes written during I/O operations on the node.
    /// </summary>
    [JsonPropertyName("io_write_bytes")]
    public ulong TotalIOBytesWritten { get; init; }

    /// <summary>
    /// Provides details on the rate at which bytes are written during I/O operations on the node.
    /// </summary>
    [JsonPropertyName("io_write_bytes_details")]
    public Rate IOBytesWrittenDetails { get; init; }

    /// <summary>
    /// Represents the average time taken for I/O write operations on the node, measured in milliseconds.
    /// </summary>
    [JsonPropertyName("io_write_avg_time")]
    public decimal AvgTimePerIOWrite { get; init; }

    /// <summary>
    /// Represents detailed metrics for the average time spent on I/O write operations,
    /// including rate or frequency, on the node.
    /// </summary>
    [JsonPropertyName("io_write_avg_time_details")]
    public Rate AvgTimePerIOWriteDetails { get; init; }

    /// <summary>
    /// Represents the total count of I/O sync operations performed by the node.
    /// </summary>
    [JsonPropertyName("io_sync_count")]
    public ulong IOSyncCount { get; init; }

    /// <summary>
    /// Specifies the rate details related to synchronous I/O operations performed by the node.
    /// </summary>
    [JsonPropertyName("io_sync_count_details")]
    public Rate IOSyncsDetails { get; init; }

    /// <summary>
    /// Represents the average time taken for synchronous I/O operations.
    /// </summary>
    [JsonPropertyName("io_sync_avg_time")]
    public decimal AverageIOSyncTime { get; init; }

    /// <summary>
    /// Represents the rate details associated with the average time taken for synchronous I/O operations performed by the node.
    /// </summary>
    [JsonPropertyName("io_sync_avg_time_details")]
    public Rate AvgIOSyncTimeDetails { get; init; }

    /// <summary>
    /// Represents the total number of I/O seek operations performed.
    /// </summary>
    [JsonPropertyName("io_seek_count")]
    public ulong IOSeekCount { get; init; }

    /// <summary>
    /// Provides rate details for the number of I/O seek operations performed.
    /// </summary>
    [JsonPropertyName("io_seek_count_details")]
    public Rate IOSeeksDetails { get; init; }

    /// <summary>
    /// Represents the average time taken for I/O seek operations.
    /// </summary>
    [JsonPropertyName("io_seek_avg_time")]
    public decimal AverageIOSeekTime { get; init; }

    /// <summary>
    /// Represents detailed rate information regarding the average time taken for I/O seek operations.
    /// </summary>
    [JsonPropertyName("io_seek_avg_time_details")]
    public Rate AvgIOSeekTimeDetails { get; init; }

    /// <summary>
    /// Represents the total count of I/O file handles that have been reopened.
    /// </summary>
    [JsonPropertyName("io_reopen_count")]
    public ulong TotalIOReopened { get; init; }

    /// <summary>
    /// Represents detailed rate information regarding the number of I/O file handle reopens.
    /// </summary>
    [JsonPropertyName("io_reopen_count_details")]
    public Rate IOReopenedDetails { get; init; }

    /// <summary>
    /// Represents the total number of RAM-based Mnesia transactions performed on the node.
    /// </summary>
    [JsonPropertyName("mnesia_ram_tx_count")]
    public ulong TotalMnesiaRamTransactions { get; init; }

    /// <summary>
    /// Contains detailed rate information about the number of transactions processed in Mnesia RAM.
    /// </summary>
    [JsonPropertyName("mnesia_ram_tx_count_details")]
    public Rate MnesiaRAMTransactionCountDetails { get; init; }

    /// <summary>
    /// Represents the total number of Mnesia disk transactions processed.
    /// </summary>
    [JsonPropertyName("mnesia_disk_tx_count")]
    public ulong TotalMnesiaDiskTransactions { get; init; }

    /// <summary>
    /// Represents the rate details for Mnesia disk transactions.
    /// </summary>
    [JsonPropertyName("mnesia_disk_tx_count_details")]
    public Rate MnesiaDiskTransactionCountDetails { get; init; }

    /// <summary>
    /// Represents the total number of message store read operations performed.
    /// </summary>
    [JsonPropertyName("msg_store_read_count")]
    public ulong TotalMessageStoreReads { get; init; }

    /// <summary>
    /// Provides detailed rate information for message store read operations.
    /// </summary>
    [JsonPropertyName("msg_store_read_count_details")]
    public Rate MessageStoreReadDetails { get; init; }

    /// <summary>
    /// Represents the total number of message store write operations performed.
    /// </summary>
    [JsonPropertyName("msg_store_write_count")]
    public ulong TotalMessageStoreWrites { get; init; }

    /// <summary>
    /// Provides detailed rate metrics regarding message store write operations.
    /// </summary>
    [JsonPropertyName("msg_store_write_count_details")]
    public Rate MessageStoreWriteDetails { get; init; }

    /// <summary>
    /// Represents the total number of journal write operations performed on the queue index.
    /// </summary>
    [JsonPropertyName("queue_index_journal_write_count")]
    public ulong TotalQueueIndexJournalWrites { get; init; }

    /// <summary>
    /// Represents the details of the rate at which journal write operations are performed on the queue index.
    /// </summary>
    [JsonPropertyName("queue_index_journal_write_count_details")]
    public Rate QueueIndexJournalWriteDetails { get; init; }

    /// <summary>
    /// Indicates the total number of write operations performed on the queue index by the node.
    /// </summary>
    [JsonPropertyName("queue_index_write_count")]
    public ulong TotalQueueIndexWrites { get; init; }

    /// <summary>
    /// Represents detailed rate information regarding the operation of writing to the queue index on the node.
    /// </summary>
    [JsonPropertyName("queue_index_write_count_details")]
    public Rate QueueIndexWriteDetails { get; init; }

    /// <summary>
    /// Represents the total number of read operations performed on the queue index of the node.
    /// </summary>
    [JsonPropertyName("queue_index_read_count")]
    public ulong TotalQueueIndexReads { get; init; }

    /// <summary>
    /// Represents detailed rate information regarding the operation of reading from the queue index on the node.
    /// </summary>
    [JsonPropertyName("queue_index_read_count_details")]
    public Rate QueueIndexReadDetails { get; init; }

    /// <summary>
    /// Represents the total number of attempts made to open file handles on the node.
    /// </summary>
    [JsonPropertyName("io_file_handle_open_attempt_count")]
    public ulong TotalOpenFileHandleAttempts { get; init; }

    /// <summary>
    /// Provides detailed rate information regarding the attempts to open file handles on the node.
    /// </summary>
    [JsonPropertyName("io_file_handle_open_attempt_count_details")]
    public Rate FileHandleOpenAttemptDetails { get; init; }

    /// <summary>
    /// Gets the average time taken for attempts to open file handles on the node.
    /// </summary>
    [JsonPropertyName("io_file_handle_open_attempt_avg_time")]
    public decimal OpenFileHandleAttemptsAvgTime { get; init; }

    /// <summary>
    /// Provides detailed average time rate information regarding the attempts to open file handles on the node.
    /// </summary>
    [JsonPropertyName("io_file_handle_open_attempt_avg_time_details")]
    public Rate FileHandleOpenAttemptAvgTimeDetails { get; init; }

    /// <summary>
    /// Represents metrics gathered from garbage collection activities on the node.
    /// </summary>
    [JsonPropertyName("metrics_gc_queue_length")]
    public GarbageCollectionMetrics GarbageCollectionMetrics { get; init; }

    /// <summary>
    /// Represents the total number of channels that have been closed on the node.
    /// </summary>
    [JsonPropertyName("channel_closed")]
    public ulong TotalChannelsClosed { get; init; }

    /// <summary>
    /// Provides detailed rate information regarding the closed channels on the node.
    /// </summary>
    [JsonPropertyName("channel_closed_details")]
    public Rate ClosedChannelDetails { get; init; }

    /// <summary>
    /// Gets the total number of channels created on the node.
    /// </summary>
    [JsonPropertyName("channel_created")]
    public ulong TotalChannelsCreated { get; init; }

    /// <summary>
    /// Contains details about the rate of channels created on the node.
    /// </summary>
    [JsonPropertyName("channel_created_details")]
    public Rate CreatedChannelDetails { get; init; }

    /// <summary>
    /// Gets the total number of connections closed on the node.
    /// </summary>
    [JsonPropertyName("connection_closed")]
    public ulong TotalConnectionsClosed { get; init; }

    /// <summary>
    /// Provides details about the rate at which connections are being closed on the node.
    /// </summary>
    [JsonPropertyName("connection_closed_details")]
    public Rate ClosedConnectionDetails { get; init; }

    /// <summary>
    /// Gets the total number of connections created on the node.
    /// </summary>
    [JsonPropertyName("connection_created")]
    public ulong TotalConnectionsCreated { get; init; }

    /// <summary>
    /// Gets details about the rate of created connections on the node.
    /// </summary>
    [JsonPropertyName("connection_created_details")]
    public Rate CreatedConnectionDetails { get; init; }

    /// <summary>
    /// Gets the total number of queues created on the node.
    /// </summary>
    [JsonPropertyName("queue_created")]
    public ulong TotalQueuesCreated { get; init; }

    /// <summary>
    /// Represents details about the rate at which queues are created on the node.
    /// </summary>
    [JsonPropertyName("queue_created_details")]
    public Rate CreatedQueueDetails { get; init; }

    /// <summary>
    /// Gets the total number of queues declared on the node.
    /// </summary>
    [JsonPropertyName("queue_declared")]
    public ulong TotalQueuesDeclared { get; init; }

    /// <summary>
    /// Provides information about the rate details of declared queues on the node.
    /// </summary>
    [JsonPropertyName("queue_declared_details")]
    public Rate DeclaredQueueDetails { get; init; }

    /// <summary>
    /// Gets the total number of queues deleted on the node.
    /// </summary>
    [JsonPropertyName("queue_deleted")]
    public ulong TotalQueuesDeleted { get; init; }

    /// <summary>
    /// Gets the rate details of deleted queues on the node.
    /// </summary>
    [JsonPropertyName("queue_deleted_details")]
    public Rate DeletedQueueDetails { get; init; }
}