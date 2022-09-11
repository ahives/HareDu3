namespace HareDu.Snapshotting.Tests.Fakes;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Testing;
using HareDu.Model;

public class FakeChannelObject :
    Channel,
    HareDuTestingFake
{
    public async Task<ResultList<ChannelInfo>> GetAll(CancellationToken cancellationToken = default)
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

        return new SuccessfulResultList<ChannelInfo>{Data = new List<ChannelInfo> {channel}, DebugInfo = null};
    }
}