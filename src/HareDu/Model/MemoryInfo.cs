namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record MemoryInfo
    {
        [JsonPropertyName("connection_readers")]
        public long ConnectionReaders { get; init; }
        
        [JsonPropertyName("connection_writers")]
        public long ConnectionWriters { get; init; }
        
        [JsonPropertyName("connection_channels")]
        public long ConnectionChannels { get; init; }
        
        [JsonPropertyName("connection_other")]
        public long ConnectionOther { get; init; }
        
        [JsonPropertyName("queue_procs")]
        public long QueueProcesses { get; init; }
        
        [JsonPropertyName("queue_slave_procs")]
        public long QueueSlaveProcesses { get; init; }
        
        [JsonPropertyName("plugins")]
        public long Plugins { get; init; }
        
        [JsonPropertyName("other_proc")]
        public long OtherProcesses { get; init; }
        
        [JsonPropertyName("metrics")]
        public long Metrics { get; init; }
        
        [JsonPropertyName("mgmt_db")]
        public long ManagementDatabase { get; init; }
        
        [JsonPropertyName("mnesia")]
        public long Mnesia { get; init; }
        
        [JsonPropertyName("other_ets")]
        public long OtherInMemoryStorage { get; init; }
        
        [JsonPropertyName("binary")]
        public long Binary { get; init; }
        
        [JsonPropertyName("msg_index")]
        public long MessageIndex { get; init; }
        
        [JsonPropertyName("code")]
        public long ByteCode { get; init; }
        
        [JsonPropertyName("atom")]
        public long Atom { get; init; }
        
        [JsonPropertyName("other_system")]
        public long OtherSystem { get; init; }
        
        [JsonPropertyName("allocated_unused")]
        public long AllocatedUnused { get; init; }
        
        [JsonPropertyName("reserved_unallocated")]
        public long ReservedUnallocated { get; init; }
        
        [JsonPropertyName("strategy")]
        public string Strategy { get; init; }
        
        [JsonPropertyName("total")]
        public TotalMemoryInfo Total { get; init; }
        
        [JsonPropertyName("quorum_queue_procs")]
        public long QuorumQueueProcesses { get; init; }
        
        [JsonPropertyName("quorum_ets")]
        public long QuorumInMemoryStorage { get; init; }
    }
}