namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class BrokerImpl :
    BaseBrokerObject,
    Broker
{
    public BrokerImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Result<BrokerOverviewInfo>> GetOverview(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetRequest<BrokerOverviewInfo>("api/overview", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> RebalanceAllQueues(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await PostEmptyRequest("api/rebalance/queues", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<AlarmState>> IsAlarmsInEffect(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = "api/health/checks/alarms";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<AlarmState>
                {Data = AlarmState.InEffect, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<AlarmState>
                {Data = AlarmState.NotInEffect, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<AlarmState>
            {
                Data = AlarmState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether an alarm is in effect or not.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }

    public async Task<Result<BrokerState>> IsBrokerAlive(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = $"api/aliveness-test/{vhost.ToSanitizedName()}";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<BrokerState>
                {Data = BrokerState.Alive, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<BrokerState>
                {Data = BrokerState.NotAlive, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<BrokerState>
            {
                Data = BrokerState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether an alarm is in effect or not.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }

    public async Task<Result<VirtualHostState>> IsVirtualHostsRunning(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = "api/health/checks/virtual-hosts";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<VirtualHostState>
                {Data = VirtualHostState.Running, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<VirtualHostState>
                {Data = VirtualHostState.NotRunning, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<VirtualHostState>
            {
                Data = VirtualHostState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether all virtual hosts are running.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }

    public async Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = "api/health/checks/node-is-mirror-sync-critical";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<NodeMirrorSyncState>
                {Data = NodeMirrorSyncState.WithSyncedMirrorsOnline, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<NodeMirrorSyncState>
                {Data = NodeMirrorSyncState.WithoutSyncedMirrorsOnline, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<NodeMirrorSyncState>
            {
                Data = NodeMirrorSyncState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether or not there are mirrored queues present without any mirrors online.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }

    public async Task<Result<NodeQuorumState>> IsNodeQuorumCritical(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = "api/health/checks/node-is-quorum-critical";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<NodeQuorumState>
                {Data = NodeQuorumState.MinimumQuorum, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<NodeQuorumState>
                {Data = NodeQuorumState.BelowMinimumQuorum, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<NodeQuorumState>
            {
                Data = NodeQuorumState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether or not quorum queues have a minimum online quorum.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }

    public async Task<Result<ProtocolListenerState>> IsProtocolActiveListener(
        Action<ProtocolListenerConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new ProtocolListenerConfiguratorImpl();
        configurator?.Invoke(impl);

        string url = $"api/health/checks/protocol-listener/{impl.Protocol}";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<ProtocolListenerState>
                {Data = ProtocolListenerState.Active, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<ProtocolListenerState>
                {Data = ProtocolListenerState.NotActive, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<ProtocolListenerState>
            {
                Data = ProtocolListenerState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether or not quorum queues have a minimum online quorum.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }


    class ProtocolListenerConfiguratorImpl :
        ProtocolListenerConfigurator
    {
        public string Protocol { get; private set; }

        public string Amqp091() => Protocol = "amqp091";

        public string Amqp10() => Protocol = "amqp10";

        public string Mqtt() => Protocol = "mqtt";

        public string Stomp() => Protocol = "stomp";

        public string WebMqtt() => Protocol = "web-mqtt";

        public string WebStomp() => Protocol = "web-stomp";
    }
}