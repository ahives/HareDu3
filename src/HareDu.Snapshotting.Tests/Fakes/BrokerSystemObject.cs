namespace HareDu.Snapshotting.Tests.Fakes;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Testing;
using HareDu.Model;

public class BrokerSystemObject :
    BrokerSystem,
    HareDuTestingFake
{
    public async Task<Result<SystemOverviewInfo>> GetSystemOverview(CancellationToken cancellationToken = default)
    {
        var data = new SystemOverviewInfo()
        {
            RabbitMqVersion = "3.7.18",
            ErlangVersion = "22.1",
            MessageStats = new ()
            {
                TotalMessagesAcknowledged = 7287736,
                TotalMessageDeliveryGets = 78263767,
                TotalMessagesPublished = 1234,
                TotalMessagesConfirmed = 83,
                TotalUnroutableMessages = 737,
                TotalDiskReads = 83,
                TotalMessageGets = 723,
                TotalMessageGetsWithoutAck = 373,
                TotalMessagesDelivered = 7234,
                TotalMessagesRedelivered = 7237,
                TotalMessageDeliveredWithoutAck = 8723,
                MessagesPublishedDetails = new Rate{Value = 7},
                UnroutableMessagesDetails = new Rate{Value = 48},
                MessageGetDetails = new Rate{Value = 324},
                MessageGetsWithoutAckDetails = new Rate{Value = 84},
                MessageDeliveryDetails = new Rate{Value = 84},
                MessagesDeliveredWithoutAckDetails = new Rate{Value = 56},
                MessagesRedeliveredDetails = new Rate{Value = 89},
                MessagesAcknowledgedDetails = new Rate{Value = 723},
                MessageDeliveryGetDetails = new Rate{Value = 738},
                MessagesConfirmedDetails = new Rate{Value = 7293}
            },
            ClusterName = "fake_cluster",
            QueueStats = new ()
            {
                TotalMessagesReadyForDelivery = 82937489379,
                MessagesReadyForDeliveryDetails = new Rate{Value = 34.4M},
                TotalUnacknowledgedDeliveredMessages = 892387397238,
                UnacknowledgedDeliveredMessagesDetails = new Rate{Value = 73.3M},
                TotalMessages = 9230748297,
                MessageDetails = new Rate{Value = 80.3M}
            }
        };
            
        return new SuccessfulResult<SystemOverviewInfo>{Data = data, DebugInfo = null};
    }

    public async Task<Result> RebalanceAllQueues(CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
}