namespace HareDu
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Consumer :
        BrokerObject
    {
        /// <summary>
        /// Returns all consumers on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<ConsumerInfo>> GetAll(CancellationToken cancellationToken = default);
    }
}