namespace HareDu.Snapshotting.Model;

using System;

public record QueueSnapshot
{
    /// <summary>
    /// Name of the queue.
    /// </summary>
    public string Identifier { get; init; }
        
    /// <summary>
    /// Name of the virtual host that the queue belongs to.
    /// </summary>
    public string VirtualHost { get; init; }
        
    /// <summary>
    /// Name of the physical node the queue is on.
    /// </summary>
    public string Node { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    public QueueChurnMetrics Messages { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    public QueueMemoryDetails Memory { get; init; }
        
    public QueueInternals Internals { get; init; }
        
    public ulong Consumers { get; init; }
        
    public decimal ConsumerUtilization { get; init; }
        
    public DateTimeOffset IdleSince { get; init; }
}