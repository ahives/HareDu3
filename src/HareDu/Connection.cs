namespace HareDu
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Connection :
        BrokerObject
    {
        /// <summary>
        /// Returns all connections on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>Asynchronous task of <see cref="ResultList{ConnectionInfo}"/></returns>
        Task<ResultList<ConnectionInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an active connection on the current RabbitMQ node.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Delete(string connection, CancellationToken cancellationToken = default);
    }
}