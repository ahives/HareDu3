namespace HareDu.Snapshotting.Lens.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using HareDu.Core.Extensions;
using HareDu.Extensions;
using HareDu.Model;
using Extensions;
using Model;
using MassTransit;

class BrokerConnectivityLens :
    BaseLens<BrokerConnectivitySnapshot>,
    Lens<BrokerConnectivitySnapshot>
{
    readonly List<IDisposable> _observers;

    public ISnapshotHistory<BrokerConnectivitySnapshot> History => _timeline.Value;

    public BrokerConnectivityLens(IBrokerObjectFactory factory)
        : base(factory)
    {
        _observers = new List<IDisposable>();
    }

    public async Task<SnapshotResult<BrokerConnectivitySnapshot>> TakeSnapshot(CancellationToken cancellationToken = default)
    {
        var cluster = await _factory
            .GetBrokerSystemOverview(cancellationToken)
            .ConfigureAwait(false);

        if (cluster.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve cluster information."));

            return new EmptySnapshotResult<BrokerConnectivitySnapshot>();
        }

        var connections = await _factory
            .GetAllConnections(cancellationToken)
            .ConfigureAwait(false);

        if (connections.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve connection information."));

            return new EmptySnapshotResult<BrokerConnectivitySnapshot>();
        }

        var channels = await _factory
            .GetAllChannels(cancellationToken)
            .ConfigureAwait(false);

        if (channels.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve channel information."));

            return new EmptySnapshotResult<BrokerConnectivitySnapshot>();
        }

        var snapshot = GetSnapshot(cluster, channels, connections);

        string identifier = NewId.Next().ToString();

        SaveSnapshot(identifier, snapshot);
        NotifyObservers(identifier, snapshot);

        return new SnapshotResult<BrokerConnectivitySnapshot> {Identifier = identifier, Snapshot = snapshot, Timestamp = DateTimeOffset.UtcNow};
    }

    public Lens<BrokerConnectivitySnapshot> RegisterObserver(IObserver<SnapshotContext<BrokerConnectivitySnapshot>> observer)
    {
        if (observer != null)
            _observers.Add(Subscribe(observer));

        return this;
    }

    public Lens<BrokerConnectivitySnapshot> RegisterObservers(
        IReadOnlyList<IObserver<SnapshotContext<BrokerConnectivitySnapshot>>> observers)
    {
        if (observers == null)
            return this;

        for (int i = 0; i < observers.Count; i++)
            _observers.Add(Subscribe(observers[i]));

        return this;
    }

    BrokerConnectivitySnapshot GetSnapshot(Result<BrokerOverviewInfo> cluster, ResultList<ChannelInfo> channels,
        ResultList<ConnectionInfo> connections)
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