namespace HareDu
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Channel :
        BrokerObject
    {
        /// <summary>
        /// Returns all channels on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<ChannelInfo>> GetAll(CancellationToken cancellationToken = default);
    }
}