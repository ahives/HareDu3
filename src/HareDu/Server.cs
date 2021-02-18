namespace HareDu
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Server :
        BrokerObject
    {
        /// <summary>
        /// Returns all object definitions on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ServerInfo>> Get(CancellationToken cancellationToken = default);
    }
}