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
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultList<TopicPermissionsInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="vhost"></param>
        /// <param name="configurator"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Create(string username, string vhost, Action<TopicPermissionsConfigurator> configurator, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default);
    }
}