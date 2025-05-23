namespace HareDu.Snapshotting.Tests.Fakes;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Testing;
using HareDu.Model;

public class FakeConnectionImpl :
    Connection,
    HareDuTestingFake
{
    public async Task<Results<ConnectionInfo>> GetAll(Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default)
    {
        var connection1 = new ConnectionInfo
        {
            TotalReductions = 897274932,
            OpenChannelsLimit = 982738,
            MaxFrameSizeInBytes = 627378937423,
            VirtualHost = "TestVirtualHost",
            Name = "Connection 1",
            Node = "Node 1",
            Channels = 7687264882,
            SendPending = 686219897,
            PacketsSent = 871998847,
            PacketBytesSent = 83008482374,
            PacketsReceived = 68721979894793,
            State = BrokerConnectionState.Blocked
        };
        var connection2 = new ConnectionInfo
        {
            TotalReductions = 897274932,
            OpenChannelsLimit = 982738,
            MaxFrameSizeInBytes = 627378937423,
            VirtualHost = "TestVirtualHost",
            Name = "Connection 2",
            Node = "Node 1",
            Channels = 7687264882,
            SendPending = 686219897,
            PacketsSent = 871998847,
            PacketBytesSent = 83008482374,
            PacketsReceived = 68721979894793,
            State = BrokerConnectionState.Blocked
        };
        var connection3 = new ConnectionInfo
        {
            TotalReductions = 897274932,
            OpenChannelsLimit = 982738,
            MaxFrameSizeInBytes = 627378937423,
            VirtualHost = "TestVirtualHost",
            Name = "Connection 3",
            Node = "Node 1",
            Channels = 7687264882,
            SendPending = 686219897,
            PacketsSent = 871998847,
            PacketBytesSent = 83008482374,
            PacketsReceived = 68721979894793,
            State = BrokerConnectionState.Blocked
        };

        return new SuccessfulResults<ConnectionInfo>{Data = new List<ConnectionInfo> {connection1, connection2, connection3}, DebugInfo = null};
    }

    public Task<Results<ConnectionInfo>> GetByVirtualHost(string vhost, Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<Results<ConnectionInfo>> GetByName(string name, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<Results<ConnectionInfo>> GetByUser(string username, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public async Task<Result> Delete(string connection, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<Result> DeleteByUser(string username, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}