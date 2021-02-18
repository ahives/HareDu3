namespace HareDu.Snapshotting.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;
    using HareDu.Extensions;
    using HareDu.Model;
    using MassTransit;
    using Model;

    class ClusterImpl :
        BaseSnapshotLens<ClusterSnapshot>,
        SnapshotLens<ClusterSnapshot>
    {
        readonly List<IDisposable> _observers;

        public SnapshotHistory<ClusterSnapshot> History => _timeline.Value;

        public ClusterImpl(IBrokerObjectFactory factory)
            : base(factory)
        {
            _observers = new List<IDisposable>();
        }

        public async Task<SnapshotResult<ClusterSnapshot>> TakeSnapshot(CancellationToken cancellationToken = default)
        {
            var cluster = await _factory
                .GetSystemOverview(cancellationToken)
                .ConfigureAwait(false);

            if (cluster.HasFaulted)
            {
                NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve cluster information."));
                
                return new EmptySnapshotResult<ClusterSnapshot>();
            }

            var nodes = await _factory
                .GetAllNodes(cancellationToken)
                .ConfigureAwait(false);

            if (nodes.HasFaulted)
            {
                NotifyObserversOfError(new HareDuSnapshotException("Unable to retrieve node information."));
                
                return new EmptySnapshotResult<ClusterSnapshot>();
            }

            var systemOverview = cluster.Select(x => x.Data);
            
            var snapshot = new ClusterSnapshot
            {
                ClusterName = systemOverview.ClusterName,
                BrokerVersion = systemOverview.RabbitMqVersion,
                Nodes = nodes
                    .Select(x => x.Data)
                    .Select(x => MapNodeSnapshot(systemOverview, x))
                    .ToList()
            };
            
            string identifier = NewId.Next().ToString();
            DateTimeOffset timestamp = DateTimeOffset.UtcNow;

            SaveSnapshot(identifier, snapshot, timestamp);
            NotifyObservers(identifier, snapshot, timestamp);

            return new SnapshotResult<ClusterSnapshot>{Identifier = identifier, Snapshot = snapshot, Timestamp = timestamp};
        }

        public SnapshotLens<ClusterSnapshot> RegisterObserver(IObserver<SnapshotContext<ClusterSnapshot>> observer)
        {
            if (observer != null)
                _observers.Add(Subscribe(observer));

            return this;
        }

        public SnapshotLens<ClusterSnapshot> RegisterObservers(
            IReadOnlyList<IObserver<SnapshotContext<ClusterSnapshot>>> observers)
        {
            if (observers == null)
                return this;
            
            for (int i = 0; i < observers.Count; i++)
                _observers.Add(Subscribe(observers[i]));

            return this;
        }

        NodeSnapshot MapNodeSnapshot(SystemOverviewInfo systemOverview, NodeInfo node) =>
            new ()
            {
                Identifier = node.Name,
                Uptime = node.Uptime,
                ClusterIdentifier = systemOverview.ClusterName,
                OS = MapOperatingSystemSnapshot(node),
                Runtime = MapRuntimeSnapshot(systemOverview, node),
                ContextSwitching = new () {Total = node.ContextSwitches, Rate = node.ContextSwitchDetails?.Value ?? 0},
                Disk = MapDiskSnapshot(node),
                NetworkPartitions = node.Partitions.ToList(),
                AvailableCoresDetected = node.AvailableCoresDetected,
                Memory = new ()
                {
                    NodeIdentifier = node.Name,
                    Used = node.MemoryUsed,
                    UsageRate = node.MemoryUsageDetails?.Value ?? 0,
                    Limit = node.MemoryLimit,
                    AlarmInEffect = node.MemoryAlarm
                },
                IsRunning = node.IsRunning,
                InterNodeHeartbeat = node.NetworkTickTime
            };

        OperatingSystemSnapshot MapOperatingSystemSnapshot(NodeInfo node) =>
            new ()
            {
                NodeIdentifier = node.Name,
                ProcessId = node.OperatingSystemProcessId,
                FileDescriptors = new ()
                {
                    Available = node.TotalFileDescriptors,
                    Used = node.FileDescriptorUsed,
                    UsageRate = node.FileDescriptorUsedDetails?.Value ?? 0,
                    OpenAttempts = node.TotalOpenFileHandleAttempts,
                    OpenAttemptRate = node.FileHandleOpenAttemptDetails?.Value ?? 0,
                    AvgTimePerOpenAttempt = node.FileHandleOpenAttemptAvgTimeDetails?.Value ?? 0,
                    AvgTimeRatePerOpenAttempt = node.FileHandleOpenAttemptAvgTimeDetails?.Value ?? 0
                },
                SocketDescriptors = new ()
                {
                    Available = node.TotalSocketsAvailable,
                    Used = node.SocketsUsed,
                    UsageRate = node.SocketsUsedDetails?.Value ?? 0
                }
            };

        DiskSnapshot MapDiskSnapshot(NodeInfo node) =>
            new ()
            {
                NodeIdentifier = node.Name,
                Capacity = new () {Available = node.FreeDiskSpace, Rate = node.FreeDiskSpaceDetails?.Value ?? 0},
                Limit = node.FreeDiskLimit,
                AlarmInEffect = node.FreeDiskAlarm,
                IO = new ()
                {
                    Reads = new ()
                    {
                        Total = node.TotalIOReads,
                        Rate = node.IOReadDetails?.Value ?? 0,
                        Bytes = new () {Total = node.TotalIOBytesRead, Rate = node.IOBytesReadDetails?.Value ?? 0},
                        WallTime = new () {Average = node.AvgIOReadTime, Rate = node.AvgIOReadTimeDetails?.Value ?? 0}
                    },
                    Writes = new ()
                    {
                        Total = node.TotalIOWrites,
                        Rate = node.IOWriteDetails?.Value ?? 0,
                        Bytes = new () {Total = node.TotalIOBytesWritten, Rate = node.IOBytesWrittenDetails?.Value ?? 0},
                        WallTime = new () {Average = node.AvgTimePerIOWrite, Rate = node.AvgTimePerIOWriteDetails?.Value ?? 0}
                    },
                    Seeks = new ()
                    {
                        Total = node.IOSeekCount,
                        Rate = node.IOSeeksDetails?.Value ?? 0,
                        Bytes = new () {Total = 0, Rate = 0},
                        WallTime = new () {Average = node.AverageIOSeekTime, Rate = node.AvgIOSeekTimeDetails?.Value ?? 0}
                    },
                    FileHandles = new () {Recycled = node.TotalIOReopened, Rate = node.IOReopenedDetails?.Value ?? 0}
                }
            };

        BrokerRuntimeSnapshot MapRuntimeSnapshot(SystemOverviewInfo systemOverview, NodeInfo node) =>
            new ()
            {
                ClusterIdentifier = systemOverview.ClusterName,
                Identifier = node.Name,
                Version = systemOverview.ErlangVersion,
                Processes = new ()
                {
                    Limit = node.TotalProcesses,
                    Used = node.ProcessesUsed,
                    UsageRate = node.ProcessUsageDetails?.Value ?? 0
                },
                Database = new ()
                {
                    Transactions = new ()
                    {
                        RAM = new () {Total = node.TotalMnesiaRamTransactions, Rate = node.MnesiaRAMTransactionCountDetails?.Value ?? 0},
                        Disk = new () {Total = node.TotalMnesiaDiskTransactions, Rate = node.MnesiaDiskTransactionCountDetails?.Value ?? 0}
                    },
                    Index = new ()
                    {
                        Reads = new () {Total = node.TotalQueueIndexReads, Rate = node.QueueIndexReadDetails?.Value ?? 0},
                        Writes = new () {Total = node.TotalQueueIndexWrites, Rate = node.QueueIndexWriteDetails?.Value ?? 0},
                        Journal = new () {Writes = new (){Total = node.TotalQueueIndexJournalWrites, Rate = node.QueueIndexJournalWriteDetails?.Value ?? 0}}
                    },
                    Storage = new ()
                    {
                        Reads = new () {Total = node.TotalMessageStoreReads, Rate = node.MessageStoreReadDetails?.Value ?? 0},
                        Writes = new () {Total = node.TotalMessageStoreWrites, Rate = node.MessageStoreWriteDetails?.Value ?? 0}
                    }
                },
                GC = new ()
                {
                    ChannelsClosed = new () {Total = node.TotalChannelsClosed, Rate = node.ClosedChannelDetails?.Value ?? 0},
                    ConnectionsClosed = new () {Total = node.TotalConnectionsClosed, Rate = node.ClosedConnectionDetails?.Value ?? 0},
                    QueuesDeleted = new () {Total = node.TotalQueuesDeleted, Rate = node.DeletedQueueDetails?.Value ?? 0},
                    ReclaimedBytes = new () {Total = node.BytesReclaimedByGarbageCollector, Rate = node.ReclaimedBytesFromGCDetails?.Value ?? 0}
                }
            };
    }
}