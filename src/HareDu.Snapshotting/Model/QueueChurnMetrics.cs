namespace HareDu.Snapshotting.Model
{
    public record QueueChurnMetrics
    {
        /// <summary>
        /// Total number of messages published to the queue and rate at which they were sent and the corresponding rate in msg/s.
        /// </summary>
        public QueueDepth Incoming { get; init; }
        
        /// <summary>
        /// Total number of messages sitting in the queue that were consumed but not acknowledged and the corresponding rate in msg/s.
        /// </summary>
        public QueueDepth Unacknowledged { get; init; }
        
        /// <summary>
        /// Total number of messages sitting in the queue ready to be delivered to consumers and the corresponding rate in msg/s.
        /// </summary>
        public QueueDepth Ready { get; init; }
        
        /// <summary>
        /// Total number of "Get" operations on the queue with acknowledgement and the corresponding rate in msg/s.
        /// </summary>
        public QueueDepth Gets { get; init; }
        
        /// <summary>
        /// Total number of "Get" operations on the queue without acknowledgement and the corresponding rate in msg/s.
        /// </summary>
        public QueueDepth GetsWithoutAck { get; init; }
        
        /// <summary>
        /// Total number of messages that were delivered to consumers and the corresponding rate in msg/s.
        /// </summary>
        public QueueDepth Delivered { get; init; }
        
        /// <summary>
        /// Total number of messages that were delivered to consumers without acknowledgement and the corresponding rate in msg/s.
        /// </summary>
        public QueueDepth DeliveredWithoutAck { get; init; }
        
        /// <summary>
        /// Total number of messages delivered to consumers via "Get" operations and the corresponding rate in msg/s. 
        /// </summary>
        public QueueDepth DeliveredGets { get; init; }
        
        /// <summary>
        /// Total/rate (msg/s) of messages that were redelivered to consumers.
        /// </summary>
        public QueueDepth Redelivered { get; init; }
        
        /// <summary>
        /// Total/rate (msg/s) of messages that were acknowledged to have been consumed.
        /// </summary>
        public QueueDepth Acknowledged { get; init; }
        
        /// <summary>
        /// Total number of messages that 
        /// </summary>
        public QueueDepth Aggregate { get; init; }
    }
}