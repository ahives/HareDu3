namespace HareDu.Snapshotting.Tests.Fakes;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Testing;
using HareDu.Model;

public class FakeChannelImpl :
    Channel,
    HareDuTestingFake
{
    public async Task<Results<ChannelInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        var channel = new ChannelInfo
        {
            TotalReductions = 872634826473,
            VirtualHost = "TestVirtualHost",
            Node = "Node 1",
            FrameMax = 728349837,
            Name = "Channel 1",
            TotalChannels = 87,
            SentPending = 89,
            PrefetchCount = 78,
            UncommittedAcknowledgements = 98237843,
            UncommittedMessages = 383902,
            UnconfirmedMessages = 82930,
            UnacknowledgedMessages = 7882003,
            TotalConsumers = 90,
            ConnectionDetails = new ()
            {
                Name = "Connection 1"
            }
        };

        return new SuccessfulResults<ChannelInfo>{Data = new List<ChannelInfo> {channel}, DebugInfo = null};
    }

    public Task<Results<ChannelInfo>> GetAll(Action<PaginationConfigurator> pagination, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<Results<ChannelInfo>> GetByConnection(string connectionName, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<Results<ChannelInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<Result<ChannelInfo>> GetByName(string name, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}