namespace HareDu.Snapshotting.Model
{
    public record QueueOperationMetrics
    {
        /// <summary>
        /// Total number of publish operations sent to all queues and rate at which they were sent in msg/s.
        /// </summary>
        public QueueOperation Incoming { get; init; }
        
        /// <summary>
        /// Total number of "Get" operations sent to all queue with acknowledgement and the corresponding rate at which they were sent in msg/s.
        /// </summary>
        public QueueOperation Gets { get; init; }
        
        /// <summary>
        /// Total number of "Get" operations on the channel without acknowledgement and the corresponding rate in msg/s.
        /// </summary>
        public QueueOperation GetsWithoutAck { get; init; }
        
        /// <summary>
        /// Total number of messages that were delivered to consumers and the corresponding rate in msg/s.
        /// </summary>
        public QueueOperation Delivered { get; init; }
        
        /// <summary>
        /// Total number of messages that were delivered to consumers without acknowledgement and the corresponding rate in msg/s.
        /// </summary>
        public QueueOperation DeliveredWithoutAck { get; init; }
        
        /// <summary>
        /// Total number of messages delivered to consumers via "Get" operations and the corresponding rate in msg/s. 
        /// </summary>
        public QueueOperation DeliveredGets { get; init; }
        
        /// <summary>
        /// Total/rate (msg/s) of messages that were redelivered to consumers.
        /// </summary>
        public QueueOperation Redelivered { get; init; }
        
        /// <summary>
        /// Total/rate (msg/s) of messages that were acknowledged to have been consumed.
        /// </summary>
        public QueueOperation Acknowledged { get; init; }
        
        /// <summary>
        /// Total number of not routed operations.
        /// </summary>
        public QueueOperation NotRouted { get; init; }
    }
}