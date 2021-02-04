namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record NodeInfo
    {
        [JsonPropertyName("partitions")]
        public IList<string> Partitions { get; init; }
        
        [JsonPropertyName("os_pid")]
        public string OperatingSystemProcessId { get; init; }

        [JsonPropertyName("fd_total")]
        public ulong TotalFileDescriptors { get; init; }

        [JsonPropertyName("sockets_total")]
        public ulong TotalSocketsAvailable { get; init; }

        [JsonPropertyName("mem_limit")]
        public ulong MemoryLimit { get; init; }

        [JsonPropertyName("mem_alarm")]
        public bool MemoryAlarm { get; init; }

        [JsonPropertyName("disk_free_limit")]
        public ulong FreeDiskLimit { get; init; }

        [JsonPropertyName("disk_free_alarm")]
        public bool FreeDiskAlarm { get; init; }

        [JsonPropertyName("proc_total")]
        public ulong TotalProcesses { get; init; }

        [JsonPropertyName("rates_mode")]
        public string RatesMode { get; init; }

        [JsonPropertyName("uptime")]
        public long Uptime { get; init; }

        [JsonPropertyName("run_queue")]
        public int RunQueue { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }
        
        [JsonPropertyName("processors")]
        public uint AvailableCoresDetected { get; init; }

        [JsonPropertyName("exchange_types")]
        public IList<ExchangeType> ExchangeTypes { get; init; }

        [JsonPropertyName("auth_mechanisms")]
        public IList<AuthenticationMechanism> AuthenticationMechanisms { get; init; }

        [JsonPropertyName("applications")]
        public IList<Application> Applications { get; init; }

        [JsonPropertyName("contexts")]
        public IList<NodeContext> Contexts { get; init; }

        [JsonPropertyName("log_file")]
        public string LogFile { get; init; }
        
        [JsonPropertyName("log_files")]
        public IList<string> LogFiles { get; init; }

        [JsonPropertyName("sasl_log_file")]
        public string SaslLogFile { get; init; }

        [JsonPropertyName("db_dir")]
        public string DatabaseDirectory { get; init; }
        
        [JsonPropertyName("config_files")]
        public IList<string> ConfigFiles { get; init; }

        [JsonPropertyName("net_ticktime")]
        public long NetworkTickTime { get; init; }

        [JsonPropertyName("enabled_plugins")]
        public IList<string> EnabledPlugins { get; init; }

        [JsonPropertyName("mem_calculation_strategy")]
        public string MemoryCalculationStrategy { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("running")]
        public bool IsRunning { get; init; }

        [JsonPropertyName("mem_used")]
        public ulong MemoryUsed { get; init; }

        [JsonPropertyName("mem_used_details")]
        public Rate MemoryUsageDetails { get; init; }

        [JsonPropertyName("fd_used")]
        public ulong FileDescriptorUsed { get; init; }

        [JsonPropertyName("fd_used_details")]
        public Rate FileDescriptorUsedDetails { get; init; }

        [JsonPropertyName("sockets_used")]
        public ulong SocketsUsed { get; init; }

        [JsonPropertyName("sockets_used_details")]
        public Rate SocketsUsedDetails { get; init; }

        [JsonPropertyName("proc_used")]
        public ulong ProcessesUsed { get; init; }

        [JsonPropertyName("proc_used_details")]
        public Rate ProcessUsageDetails { get; init; }

        [JsonPropertyName("disk_free")]
        public ulong FreeDiskSpace { get; init; }

        [JsonPropertyName("disk_free_details")]
        public Rate FreeDiskSpaceDetails { get; init; }

        [JsonPropertyName("gc_num")]
        public ulong NumberOfGarbageCollected { get; init; }

        [JsonPropertyName("gc_num_details")]
        public Rate GcDetails { get; init; }

        [JsonPropertyName("gc_bytes_reclaimed")]
        public ulong BytesReclaimedByGarbageCollector { get; init; }

        [JsonPropertyName("gc_bytes_reclaimed_details")]
        public Rate ReclaimedBytesFromGCDetails { get; init; }

        [JsonPropertyName("context_switches")]
        public ulong ContextSwitches { get; init; }

        [JsonPropertyName("context_switches_details")]
        public Rate ContextSwitchDetails { get; init; }

        [JsonPropertyName("io_read_count")]
        public ulong TotalIOReads { get; init; }

        [JsonPropertyName("io_read_count_details")]
        public Rate IOReadDetails { get; init; }

        [JsonPropertyName("io_read_bytes")]
        public ulong TotalIOBytesRead { get; init; }

        [JsonPropertyName("io_read_bytes_details")]
        public Rate IOBytesReadDetails { get; init; }

        [JsonPropertyName("io_read_avg_time")]
        public decimal AvgIOReadTime { get; init; }

        [JsonPropertyName("io_read_avg_time_details")]
        public Rate AvgIOReadTimeDetails { get; init; }

        [JsonPropertyName("io_write_count")]
        public ulong TotalIOWrites { get; init; }

        [JsonPropertyName("io_write_count_details")]
        public Rate IOWriteDetails { get; init; }

        [JsonPropertyName("io_write_bytes")]
        public ulong TotalIOBytesWritten { get; init; }

        [JsonPropertyName("io_write_bytes_details")]
        public Rate IOBytesWrittenDetails { get; init; }

        [JsonPropertyName("io_write_avg_time")]
        public decimal AvgTimePerIOWrite { get; init; }

        [JsonPropertyName("io_write_avg_time_details")]
        public Rate AvgTimePerIOWriteDetails { get; init; }

        [JsonPropertyName("io_sync_count")]
        public ulong IOSyncCount { get; init; }

        [JsonPropertyName("io_sync_count_details")]
        public Rate IOSyncsDetails { get; init; }

        [JsonPropertyName("io_sync_avg_time")]
        public decimal AverageIOSyncTime { get; init; }

        [JsonPropertyName("io_sync_avg_time_details")]
        public Rate AvgIOSyncTimeDetails { get; init; }

        [JsonPropertyName("io_seek_count")]
        public ulong IOSeekCount { get; init; }

        [JsonPropertyName("io_seek_count_details")]
        public Rate IOSeeksDetails { get; init; }

        [JsonPropertyName("io_seek_avg_time")]
        public decimal AverageIOSeekTime { get; init; }

        [JsonPropertyName("io_seek_avg_time_details")]
        public Rate AvgIOSeekTimeDetails { get; init; }

        [JsonPropertyName("io_reopen_count")]
        public ulong TotalIOReopened { get; init; }

        [JsonPropertyName("io_reopen_count_details")]
        public Rate IOReopenedDetails { get; init; }

        [JsonPropertyName("mnesia_ram_tx_count")]
        public ulong TotalMnesiaRamTransactions { get; init; }

        [JsonPropertyName("mnesia_ram_tx_count_details")]
        public Rate MnesiaRAMTransactionCountDetails { get; init; }

        [JsonPropertyName("mnesia_disk_tx_count")]
        public ulong TotalMnesiaDiskTransactions { get; init; }

        [JsonPropertyName("mnesia_disk_tx_count_details")]
        public Rate MnesiaDiskTransactionCountDetails { get; init; }

        [JsonPropertyName("msg_store_read_count")]
        public ulong TotalMessageStoreReads { get; init; }

        [JsonPropertyName("msg_store_read_count_details")]
        public Rate MessageStoreReadDetails { get; init; }

        [JsonPropertyName("msg_store_write_count")]
        public ulong TotalMessageStoreWrites { get; init; }

        [JsonPropertyName("msg_store_write_count_details")]
        public Rate MessageStoreWriteDetails { get; init; }

        [JsonPropertyName("queue_index_journal_write_count")]
        public ulong TotalQueueIndexJournalWrites { get; init; }

        [JsonPropertyName("queue_index_journal_write_count_details")]
        public Rate QueueIndexJournalWriteDetails { get; init; }

        [JsonPropertyName("queue_index_write_count")]
        public ulong TotalQueueIndexWrites { get; init; }

        [JsonPropertyName("queue_index_write_count_details")]
        public Rate QueueIndexWriteDetails { get; init; }

        [JsonPropertyName("queue_index_read_count")]
        public ulong TotalQueueIndexReads { get; init; }

        [JsonPropertyName("queue_index_read_count_details")]
        public Rate QueueIndexReadDetails { get; init; }

        [JsonPropertyName("io_file_handle_open_attempt_count")]
        public ulong TotalOpenFileHandleAttempts { get; init; }

        [JsonPropertyName("io_file_handle_open_attempt_count_details")]
        public Rate FileHandleOpenAttemptDetails { get; init; }

        [JsonPropertyName("io_file_handle_open_attempt_avg_time")]
        public decimal OpenFileHandleAttemptsAvgTime { get; init; }

        [JsonPropertyName("io_file_handle_open_attempt_avg_time_details")]
        public Rate FileHandleOpenAttemptAvgTimeDetails { get; init; }

        [JsonPropertyName("metrics_gc_queue_length")]
        public GarbageCollectionMetrics GarbageCollectionMetrics { get; init; }
        
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
}