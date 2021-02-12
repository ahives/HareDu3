namespace HareDu.Internal
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    class ServerImpl :
        BaseBrokerObject,
        Server
    {
        public ServerImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<Result<ServerInfo>> Get(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/definitions";
            
            return await Get<ServerInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result<ServerHealthInfo>> GetHealth(Action<HealthCheckAction> action,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new HealthCheckActionImpl();
            action?.Invoke(impl);

            string url;
            
            switch (impl.CheckUpType)
            {
                case HealthCheckType.VirtualHost:
                    url = $"api/aliveness-test/{impl.RmqObjectName.ToSanitizedName()}";
                    break;
                    
                case HealthCheckType.Node:
                    url = $"api/healthchecks/node/{impl.RmqObjectName}";
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return await Get<ServerHealthInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        
        class HealthCheckActionImpl :
            HealthCheckAction
        {
            public string RmqObjectName { get; private set; }
            public HealthCheckType CheckUpType { get; private set; }
            
            public void VirtualHost(string name)
            {
                CheckUpType = HealthCheckType.VirtualHost;
                RmqObjectName = name;
            }

            public void Node(string name)
            {
                CheckUpType = HealthCheckType.Node;
                RmqObjectName = name;
            }
        }
    }
}