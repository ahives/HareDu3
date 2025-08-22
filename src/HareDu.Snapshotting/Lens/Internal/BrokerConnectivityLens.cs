namespace HareDu.Snapshotting.Lens.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Security;
using HareDu.Core.Extensions;
using HareDu.Model;
using Extensions;
using Model;

class BrokerConnectivityLens :
    BaseLens<BrokerConnectivitySnapshot>,
    Lens<BrokerConnectivitySnapshot>
{
    readonly List<IDisposable> _observers;

    public ISnapshotHistory<BrokerConnectivitySnapshot> History => _timeline.Value;

    public BrokerConnectivityLens(IBrokerFactory factory)
        : base(factory)
    {
        _observers = new List<IDisposable>();
    }

    public async Task<SnapshotResult<BrokerConnectivitySnapshot>> TakeSnapshot(Action<HareDuCredentialProvider> provider, CancellationToken cancellationToken = default)
    {
        var cluster = await _factory
            .API<Broker>(provider)
            .GetOverview(cancellationToken)
            .ConfigureAwait(false);

        if (cluster.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve cluster information."));

            return new EmptySnapshotResult<BrokerConnectivitySnapshot>();
        }

        var connections = await _factory
            .API<Connection>(provider)
            .GetAll(null, cancellationToken)
            .ConfigureAwait(false);

        if (connections.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve connection information."));

            return new EmptySnapshotResult<BrokerConnectivitySnapshot>();
        }

        var channels = await _factory
            .API<Channel>(provider)
            .GetAll(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (channels.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve channel information."));

            return new EmptySnapshotResult<BrokerConnectivitySnapshot>();
        }

        var snapshot = GetSnapshot(cluster, channels, connections);

        string identifier = Guid.CreateVersion7(DateTimeOffset.UtcNow).ToString();

        SaveSnapshot(identifier, snapshot);
        NotifyObservers(identifier, snapshot);

        return new SnapshotResult<BrokerConnectivitySnapshot>
            {Identifier = identifier, Snapshot = snapshot, Timestamp = DateTimeOffset.UtcNow};
    }

    public Lens<BrokerConnectivitySnapshot> RegisterObserver(IObserver<SnapshotContext<BrokerConnectivitySnapshot>> observer)
    {
        if (observer is not null)
            _observers.Add(Subscribe(observer));

        return this;
    }

    public Lens<BrokerConnectivitySnapshot> RegisterObservers(
        IReadOnlyList<IObserver<SnapshotContext<BrokerConnectivitySnapshot>>> observers)
    {
        if (observers is null)
            return this;

        for (int i = 0; i < observers.Count; i++)
            _observers.Add(Subscribe(observers[i]));

        return this;
    }

    BrokerConnectivitySnapshot GetSnapshot(Result<BrokerOverviewInfo> cluster, Results<ChannelInfo> channels,
        Results<ConnectionInfo> connections)
    {
        var systemOverview = cluster.Select(x => x.Data);
        var snapshot = new BrokerConnectivitySnapshot
        {
            ClusterName = systemOverview.ClusterName,
            BrokerVersion = systemOverview.RabbitMqVersion,
            ChannelsClosed = new()
            {
                Total = systemOverview.ChurnRates?.TotalChannelsClosed ?? 0,
                Rate = systemOverview.ChurnRates?.ClosedChannelDetails?.Value ?? 0
            },
            ChannelsCreated = new()
            {
                Total = systemOverview.ChurnRates?.TotalChannelsCreated ?? 0,
                Rate = systemOverview.ChurnRates?.CreatedChannelDetails?.Value ?? 0
            },
            ConnectionsCreated = new()
            {
                Total = systemOverview.ChurnRates?.TotalConnectionsCreated ?? 0,
                Rate = systemOverview.ChurnRates?.CreatedConnectionDetails?.Value ?? 0
            },
            ConnectionsClosed = new()
            {
                Total = systemOverview.ChurnRates?.TotalConnectionsClosed ?? 0,
                Rate = systemOverview.ChurnRates?.ClosedConnectionDetails?.Value ?? 0
            },
            Connections = connections
                .Select(x => x.Data)
                .Select(x => new ConnectionSnapshot
                {
                    Identifier = x.Name,
                    NetworkTraffic = new()
                    {
                        MaxFrameSize = x.MaxFrameSizeInBytes,
                        Sent = new()
                        {
                            Total = x.PacketsSent,
                            Bytes = x.PacketBytesSent,
                            Rate = x.PacketBytesSentDetails?.Value ?? 0
                        },
                        Received = new()
                        {
                            Total = x.PacketsReceived,
                            Bytes = x.PacketBytesReceived,
                            Rate = x.PacketBytesReceivedDetails?.Value ?? 0
                        }
                    },
                    Channels = channels.Select(x => x.Data).FilterByConnection(x.Name),
                    OpenChannelsLimit = x.OpenChannelsLimit,
                    NodeIdentifier = x.Node,
                    VirtualHost = x.VirtualHost,
                    State = x.State
                })
                .ToList()
        };

        return snapshot;
    }
}