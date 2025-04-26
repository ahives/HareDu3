namespace HareDu.Internal;

using System;
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

        var result = await GetRequest("api/health/checks/alarms", cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Successful.Result(AlarmState.InEffect, result.DebugInfo),
            FaultedResult => Panic.Result(AlarmState.NotInEffect, result.DebugInfo),
            _ => Panic.Result(AlarmState.NotRecognized, new()
            {
                URL = "api/health/checks/alarms",
                Errors = [new() {Reason = "Not able to determine whether an alarm is in effect or not.", Timestamp = DateTimeOffset.UtcNow}]
            })
        };
    }

    public async Task<Result<BrokerState>> IsBrokerAlive(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            return Panic.Result<BrokerState>("api/aliveness-test/{vhost}", [new (){Reason = "The name of the virtual host is missing."}]);

        var result = await GetRequest($"api/aliveness-test/{sanitizedVHost}", cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Successful.Result(BrokerState.Alive, result.DebugInfo),
            FaultedResult => Panic.Result(BrokerState.NotAlive, result.DebugInfo),
            _ => Panic.Result(BrokerState.NotRecognized, new()
            {
                URL = "api/aliveness-test/{vhost}",
                Errors = [new() {Reason = "Not able to determine whether an alarm is in effect or not.", Timestamp = DateTimeOffset.UtcNow}]
            })
        };
    }

    public async Task<Result<VirtualHostState>> IsVirtualHostsRunning(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await GetRequest("api/health/checks/virtual-hosts", cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Successful.Result(VirtualHostState.Running, result.DebugInfo),
            FaultedResult => Panic.Result(VirtualHostState.NotRunning, result.DebugInfo),
            _ => Panic.Result(VirtualHostState.NotRecognized, new()
            {
                URL = "api/health/checks/virtual-hosts",
                Errors = [new() {Reason = "Not able to determine whether all virtual hosts are running.", Timestamp = DateTimeOffset.UtcNow}]
            })
        };
    }

    public async Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await GetRequest("api/health/checks/node-is-mirror-sync-critical", cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Successful.Result(NodeMirrorSyncState.WithSyncedMirrorsOnline, result.DebugInfo),
            FaultedResult => Panic.Result(NodeMirrorSyncState.WithoutSyncedMirrorsOnline, result.DebugInfo),
            _ => Panic.Result(NodeMirrorSyncState.NotRecognized, new()
            {
                URL = "api/health/checks/node-is-mirror-sync-critical",
                Errors = [new() {Reason = "Not able to determine whether or not there are mirrored queues present without any mirrors online.", Timestamp = DateTimeOffset.UtcNow}]
            })
        };
    }

    public async Task<Result<NodeQuorumState>> IsNodeQuorumCritical(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await GetRequest("api/health/checks/node-is-quorum-critical", cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Successful.Result(NodeQuorumState.MinimumQuorum, result.DebugInfo),
            FaultedResult => Panic.Result(NodeQuorumState.BelowMinimumQuorum, result.DebugInfo),
            _ => Panic.Result(NodeQuorumState.NotRecognized, new()
            {
                URL = "api/health/checks/node-is-quorum-critical",
                Errors = [new() {Reason = "Not able to determine whether or not quorum queues have a minimum online quorum.", Timestamp = DateTimeOffset.UtcNow}]
            })
        };
    }

    public async Task<Result<ProtocolListenerState>> IsProtocolActiveListener(Protocol protocol, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (protocol == null || string.IsNullOrWhiteSpace(protocol.Value))
            return Panic.Result<ProtocolListenerState>("api/health/checks/protocol-listener/{protocol}", [new() {Reason = "The protocol is missing.", Timestamp = DateTimeOffset.UtcNow}]);

        var result = await GetRequest($"api/health/checks/protocol-listener/{protocol.Value}", cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => Successful.Result(ProtocolListenerState.Active, result.DebugInfo),
            FaultedResult => Panic.Result(ProtocolListenerState.NotActive, result.DebugInfo),
            _ => Panic.Result(ProtocolListenerState.NotRecognized, new()
            {
                URL = "api/health/checks/protocol-listener/{protocol}",
                Errors = [new() {Reason = "Not able to determine whether or not quorum queues have a minimum online quorum.", Timestamp = DateTimeOffset.UtcNow}]
            })
        };
    }
}