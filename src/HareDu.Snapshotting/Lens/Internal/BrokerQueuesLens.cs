namespace HareDu.Snapshotting.Lens.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using HareDu.Core.Extensions;
using HareDu.Extensions;
using HareDu.Model;
using Model;

class BrokerQueuesLens :
    BaseLens<BrokerQueuesSnapshot>,
    Lens<BrokerQueuesSnapshot>
{
    readonly List<IDisposable> _observers;

    public ISnapshotHistory<BrokerQueuesSnapshot> History => _timeline.Value;

    public BrokerQueuesLens(IBrokerObjectFactory factory)
        : base(factory)
    {
        _observers = new List<IDisposable>();
    }

    public async Task<SnapshotResult<BrokerQueuesSnapshot>> TakeSnapshot(CancellationToken cancellationToken = default)
    {
        var cluster = await _factory
            .GetBrokerSystemOverview(cancellationToken)
            .ConfigureAwait(false);

        if (cluster.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve cluster information."));
                
            return new EmptySnapshotResult<BrokerQueuesSnapshot>();
        }

        var queues = await _factory
            .GetAllQueues(cancellationToken)
            .ConfigureAwait(false);

        if (queues.HasFaulted)
        {
            NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve queue information."));
                
            return new EmptySnapshotResult<BrokerQueuesSnapshot>();
        }

        var systemOverview = cluster.Select(x => x.Data);
            
        var snapshot = new BrokerQueuesSnapshot
        {
            ClusterName = systemOverview.ClusterName,
            Churn = GetQueueChurnMetrics(systemOverview.MessageStats, systemOverview.QueueStats),
            Queues = queues
                .Select(x => x.Data)
                .Select(GetQueueSnapshot)
                .ToList()
        };
            
        string identifier = NewId.Next().ToString();

        SaveSnapshot(identifier, snapshot);
        NotifyObservers(identifier, snapshot);

        return new SnapshotResult<BrokerQueuesSnapshot>{Identifier = identifier, Snapshot = snapshot, Timestamp = DateTimeOffset.UtcNow};
    }

    public Lens<BrokerQueuesSnapshot> RegisterObserver(IObserver<SnapshotContext<BrokerQueuesSnapshot>> observer)
    {
        if (observer != null)
            _observers.Add(Subscribe(observer));

        return this;
    }

    public Lens<BrokerQueuesSnapshot> RegisterObservers(
        IReadOnlyList<IObserver<SnapshotContext<BrokerQueuesSnapshot>>> observers)
    {
        if (observers == null)
            return this;
        
        for (int i = 0; i < observers.Count; i++)
            _observers.Add(Subscribe(observers[i]));

        return this;
    }

    QueueSnapshot GetQueueSnapshot(QueueInfo queueInfo) =>
        new()
        {
            Identifier = queueInfo.Name,
            VirtualHost = queueInfo.VirtualHost,
            Node = queueInfo.Node,
            Messages = GetQueueChurnMetrics(queueInfo),
            Memory = new ()
            {
                Total = queueInfo.Memory,
                RAM = new ()
                {
                    Target = queueInfo.BackingQueueStatus is null ? 0 : queueInfo.BackingQueueStatus.TargetTotalMessagesInRAM.ToLong(),
                    Total = queueInfo.MessagesInRAM,
                    Bytes = queueInfo.MessageBytesInRAM,
                    Unacknowledged = queueInfo.UnacknowledgedMessagesInRAM,
                    Ready = queueInfo.MessagesReadyForDeliveryInRAM
                },
                PagedOut = new (){Total = queueInfo.TotalMessagesPagedOut, Bytes = queueInfo.MessageBytesPagedOut}
            },
            Consumers = queueInfo.Consumers,
            ConsumerUtilization = queueInfo.ConsumerUtilization,
            IdleSince = queueInfo.IdleSince
        };

    QueueChurnMetrics GetQueueChurnMetrics(QueueInfo queueInfo) =>
        new()
        {
            Incoming = new(){Total = queueInfo.MessageStats?.TotalMessagesPublished ?? 0, Rate = queueInfo.MessageStats.MessagesPublishedDetails?.Value ?? 0},
            Aggregate = new(){Total = queueInfo.TotalMessages, Rate = queueInfo.MessageDetails?.Value ?? 0},
            Gets = new(){Total = queueInfo.MessageStats?.TotalMessageGets ?? 0, Rate = queueInfo.MessageStats.MessageGetDetails?.Value ?? 0},
            GetsWithoutAck = new(){Total = queueInfo.MessageStats?.TotalMessageGetsWithoutAck ?? 0, Rate = queueInfo.MessageStats.MessageGetsWithoutAckDetails?.Value ?? 0},
            DeliveredGets = new(){Total = queueInfo.MessageStats?.TotalMessageDeliveryGets ?? 0, Rate = queueInfo.MessageStats.MessageDeliveryGetDetails?.Value ?? 0},
            Delivered = new(){Total = queueInfo.MessageStats?.TotalMessagesDelivered ?? 0, Rate = queueInfo.MessageStats.MessageDeliveryDetails?.Value ?? 0},
            DeliveredWithoutAck = new(){Total = queueInfo.MessageStats?.TotalMessageDeliveredWithoutAck ?? 0, Rate = queueInfo.MessageStats.MessagesDeliveredWithoutAckDetails?.Value ?? 0},
            Redelivered = new(){Total = queueInfo.MessageStats?.TotalMessagesRedelivered ?? 0, Rate = queueInfo.MessageStats.MessagesRedeliveredDetails?.Value ?? 0},
            Acknowledged = new(){Total = queueInfo.MessageStats?.TotalMessagesAcknowledged ?? 0, Rate = queueInfo.MessageStats.MessagesAcknowledgedDetails?.Value ?? 0},
            Ready = new(){Total = queueInfo.ReadyMessages, Rate = queueInfo.ReadyMessageDetails?.Value ?? 0},
            Unacknowledged = new(){Total = queueInfo.UnacknowledgedMessages, Rate = queueInfo.UnacknowledgedMessageDetails?.Value ?? 0}
        };

    BrokerQueueChurnMetrics GetQueueChurnMetrics(MessageStats messageStats, QueueStats queueStats) =>
        new()
        {
            Incoming = new(){Total = messageStats.TotalMessagesPublished, Rate = messageStats.MessagesPublishedDetails?.Value ?? 0},
            NotRouted = new(){Total = messageStats.TotalUnroutableMessages, Rate = messageStats.UnroutableMessagesDetails?.Value ?? 0},
            Gets = new(){Total = messageStats.TotalMessageGets, Rate = messageStats.MessageGetDetails?.Value ?? 0},
            GetsWithoutAck = new(){Total = messageStats.TotalMessageGetsWithoutAck, Rate = messageStats.MessageGetsWithoutAckDetails?.Value ?? 0},
            DeliveredGets = new(){Total = messageStats.TotalMessageDeliveryGets, Rate = messageStats.MessageDeliveryGetDetails?.Value ?? 0},
            Delivered = new(){Total = messageStats.TotalMessagesDelivered, Rate = messageStats.MessageDeliveryDetails?.Value ?? 0},
            DeliveredWithoutAck = new(){Total = messageStats.TotalMessageDeliveredWithoutAck, Rate = messageStats.MessagesDeliveredWithoutAckDetails?.Value ?? 0},
            Redelivered = new(){Total = messageStats.TotalMessagesRedelivered, Rate = messageStats.MessagesRedeliveredDetails?.Value ?? 0},
            Acknowledged = new(){Total = messageStats.TotalMessagesAcknowledged, Rate = messageStats.MessagesAcknowledgedDetails?.Value ?? 0},
            Broker = new(){Total = queueStats.TotalMessages, Rate = queueStats.MessageDetails?.Value ?? 0},
            Ready = new(){Total = queueStats.TotalMessagesReadyForDelivery, Rate = queueStats.MessagesReadyForDeliveryDetails?.Value ?? 0},
            Unacknowledged = new(){Total = queueStats.TotalUnacknowledgedDeliveredMessages, Rate = queueStats.UnacknowledgedDeliveredMessagesDetails?.Value ?? 0}
        };
}