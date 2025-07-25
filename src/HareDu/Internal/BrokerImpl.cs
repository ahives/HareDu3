namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class BrokerImpl :
    BaseBrokerImpl,
    Broker
{
    public BrokerImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<BrokerOverviewInfo>> GetOverview(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetRequest<BrokerOverviewInfo>("api/overview", RequestType.Broker, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> RebalanceQueues(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await PostEmptyRequest("api/rebalance/queues", RequestType.Broker, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<AlarmState>> IsAlarmsInEffect(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await GetRequest("api/health/checks/alarms", RequestType.Broker, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Response.Succeeded(AlarmState.InEffect, result.DebugInfo),
            UnsuccessfulResult => Response.Failed(AlarmState.NotInEffect, result.DebugInfo),
            _ => Response.Panic(AlarmState.NotRecognized, new()
            {
                URL = "api/health/checks/alarms",
                Errors = [Errors.Create("Not able to determine whether an alarm is in effect or not.")]
            })
        };
    }

    public async Task<Result<BrokerState>> IsBrokerAlive(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            return Response.Panic<BrokerState>("api/aliveness-test/{vhost}", [Errors.Create("The name of the virtual host is missing.")]);

        var result = await GetRequest($"api/aliveness-test/{sanitizedVHost}", RequestType.Broker, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Response.Succeeded(BrokerState.Alive, result.DebugInfo),
            UnsuccessfulResult => Response.Failed(BrokerState.NotAlive, result.DebugInfo),
            _ => Response.Panic(BrokerState.NotRecognized, new()
            {
                URL = "api/aliveness-test/{vhost}",
                Errors = [Errors.Create("Not able to determine whether an alarm is in effect or not.")]
            })
        };
    }

    public async Task<Result<VirtualHostState>> IsVirtualHostsRunning(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await GetRequest("api/health/checks/virtual-hosts", RequestType.Broker, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Response.Succeeded(VirtualHostState.Running, result.DebugInfo),
            UnsuccessfulResult => Response.Failed(VirtualHostState.NotRunning, result.DebugInfo),
            _ => Response.Panic(VirtualHostState.NotRecognized, new()
            {
                URL = "api/health/checks/virtual-hosts",
                Errors = [Errors.Create("Not able to determine whether all virtual hosts are running.")]
            })
        };
    }

    public async Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await GetRequest("api/health/checks/node-is-mirror-sync-critical", RequestType.Broker, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Response.Succeeded(NodeMirrorSyncState.WithSyncedMirrorsOnline, result.DebugInfo),
            UnsuccessfulResult => Response.Failed(NodeMirrorSyncState.WithoutSyncedMirrorsOnline, result.DebugInfo),
            _ => Response.Panic(NodeMirrorSyncState.NotRecognized, new()
            {
                URL = "api/health/checks/node-is-mirror-sync-critical",
                Errors = [Errors.Create("Not able to determine whether or not there are mirrored queues present without any mirrors online.")]
            })
        };
    }

    public async Task<Result<NodeQuorumState>> IsNodeQuorumCritical(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await GetRequest("api/health/checks/node-is-quorum-critical", RequestType.Broker, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Response.Succeeded(NodeQuorumState.MinimumQuorum, result.DebugInfo),
            UnsuccessfulResult => Response.Failed(NodeQuorumState.BelowMinimumQuorum, result.DebugInfo),
            _ => Response.Panic(NodeQuorumState.NotRecognized, new()
            {
                URL = "api/health/checks/node-is-quorum-critical",
                Errors = [Errors.Create("Not able to determine whether or not quorum queues have a minimum online quorum.")]
            })
        };
    }

    public async Task<Result<ProtocolListenerState>> IsProtocolActiveListener(Protocol protocol, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (protocol is null || string.IsNullOrWhiteSpace(protocol.Value))
            return Response.Panic<ProtocolListenerState>("api/health/checks/protocol-listener/{protocol}", [Errors.Create("The protocol is missing.")]);

        var result = await GetRequest($"api/health/checks/protocol-listener/{protocol.Value}", RequestType.Broker, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Response.Succeeded(ProtocolListenerState.Active, result.DebugInfo),
            UnsuccessfulResult => Response.Failed(ProtocolListenerState.NotActive, result.DebugInfo),
            _ => Response.Panic(ProtocolListenerState.NotRecognized, new()
            {
                URL = "api/health/checks/protocol-listener/{protocol}",
                Errors = [Errors.Create("Not able to determine whether or not quorum queues have a minimum online quorum.")]
            })
        };
    }
}