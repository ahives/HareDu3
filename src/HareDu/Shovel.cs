namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Shovel :
        BrokerObject
    {
        Task<ResultList<ShovelInfo>> GetAll(CancellationToken cancellationToken = default);
        
        Task<Result> Create(string shovel, string vhost, Action<ShovelConfiguration> configuration, CancellationToken cancellationToken = default);
        
        Task<Result> Delete(string shovel, string vhost, CancellationToken cancellationToken = default);
    }
}