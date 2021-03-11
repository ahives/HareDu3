namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface TopicPermissions :
        BrokerObject
    {
        /// <summary>
        /// Returns all the topic permissions.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<ResultList<TopicPermissionsInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new topic permission for the specified user per a particular exchange and virtual host.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="exchange"></param>
        /// <param name="vhost"></param>
        /// <param name="configurator"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Create(string username, string exchange, string vhost, Action<TopicPermissionsConfigurator> configurator, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete all topic permissions associate with the specified user on the specified virtual host.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="vhost"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default);
    }
}