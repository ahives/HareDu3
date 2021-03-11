namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Queue :
        BrokerObject
    {
        /// <summary>
        /// Returns all queues on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<QueueInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the specified queue on the target virtual host and perhaps RabbitMQ node.
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="vhost"></param>
        /// <param name="node"></param>
        /// <param name="configuration">Describes how the queue will be created.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Create(string queue, string vhost, string node, Action<QueueConfigurator> configuration = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the specified queue on the target virtual host and perhaps RabbitMQ node.
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="vhost"></param>
        /// <param name="configurator">Describes how the queue will be deleted.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Delete(string queue, string vhost, Action<QueueDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Purge all messages in the specified queue on the target virtual host on the current RabbitMQ node.
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="vhost"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Empty(string queue, string vhost, CancellationToken cancellationToken = default);
    }
}