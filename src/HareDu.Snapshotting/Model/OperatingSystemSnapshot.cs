namespace HareDu.Snapshotting.Model
{
    public record OperatingSystemSnapshot :
        Snapshot
    {
        public string NodeIdentifier { get; init; }
        
        public string ProcessId { get; init; }

        public FileDescriptorChurnMetrics FileDescriptors { get; init; }
        
        public SocketDescriptorChurnMetrics SocketDescriptors { get; init; }
    }
}