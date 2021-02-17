namespace HareDu
{
    using System;

    public interface DeleteQueueConfiguration
    {
        /// <summary>
        /// Specify the name of the queue.
        /// </summary>
        /// <param name="name">Name of the queue</param>
        void Queue(string name);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurator"></param>
        void Configure(Action<DeleteQueueConfigurator> configurator);

        /// <summary>
        /// Specify where the queue lives (i.e. virtual host, etc.).
        /// </summary>
        /// <param name="target"></param>
        void Targeting(Action<QueueTarget> target);
    }
}