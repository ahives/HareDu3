namespace HareDu.Snapshotting.Tests.Fakes;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Testing;
using HareDu.Model;

public class FakeConnectionObject :
    Connection,
    HareDuTestingFake
{
    public async Task<ResultList<ConnectionInfo>> GetAll(CancellationToken cancellationToken = default)
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

        return new SuccessfulResultList<ConnectionInfo>{Data = new List<ConnectionInfo> {connection1, connection2, connection3}, DebugInfo = null};
    }

    public async Task<Result> Delete(string connection, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
}