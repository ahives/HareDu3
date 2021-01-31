namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Internal;
    using Model;

    public interface Queue :
        BrokerObject
    {
        /// <summary>
        /// Returns all queues on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<QueueInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Creates the specified queue on the target virtual host and perhaps RabbitMQ node.
        /// </summary>
        /// <param name="action">Describes how the queue will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Create(Action<QueueCreateAction> action, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified queue on the target virtual host and perhaps RabbitMQ node.
        /// </summary>
        /// <param name="action">Describes how the queue will be deleted.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Delete(Action<QueueDeleteAction> action, CancellationToken cancellationToken = default);

        /// <summary>
        /// Purge all messages in the specified queue on the target virtual host on the current RAbbitMQ node.
        /// </summary>
        /// <param name="action">Describes how the queue will be purged.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Empty(Action<QueueEmptyAction> action, CancellationToken cancellationToken = default);

        /// <summary>
        /// Pull a specified number of messages from the specified queue on the target virtual host on the current RabbitMQ node.
        /// </summary>
        /// <param name="action">Describes how the queue will be purged.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<PeekedMessageInfo>> Peek(Action<QueuePeekAction> action, CancellationToken cancellationToken = default);
    }
}