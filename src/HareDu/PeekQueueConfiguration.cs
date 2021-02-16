namespace HareDu
{
    using System;

    public interface PeekQueueConfiguration
    {
        /// <summary>
        /// Specify the name of the queue.
        /// </summary>
        /// <param name="name">Name of the queue</param>
        void Queue(string name);

        /// <summary>
        /// Specify how to redeliver messages to the queue.
        /// </summary>
        /// <param name="configuration"></param>
        void Configure(Action<QueuePeekConfiguration> configuration);
        
        /// <summary>
        /// Specify where the queue lives (i.e. virtual host, etc.).
        /// </summary>
        /// <param name="target"></param>
        void Targeting(Action<QueueTarget> target);
    }
}