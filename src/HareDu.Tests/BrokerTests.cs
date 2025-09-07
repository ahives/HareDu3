namespace HareDu.Tests;

using System.Net;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Extensions;
using Model;

[TestFixture]
public class BrokerTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_system_overview1()
    {
        var result = await GetContainerBuilder("TestData/SystemOverviewInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .GetOverview();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData);
            // Assert.IsNotNull(result.Data.ObjectTotals);
            Assert.That(result.Select(x => x.Data).Select(x => x.ObjectTotals), Is.Not.Null);
            Assert.That(result.Data.QueueStats, Is.Not.Null);
            Assert.That(result.Data.MessageStats, Is.Not.Null);
            Assert.That(result.Data.ChurnRates, Is.Not.Null);
            Assert.That(result.Data.ClusterName, Is.EqualTo("rabbit@haredu"));;
            Assert.That(result.Data.ErlangVersion, Is.EqualTo("23.2"));;
            Assert.That(result.Data.ErlangFullVersion, Is.EqualTo("Erlang/OTP 23 [erts-11.1.4] [source] [64-bit] [smp:4:4] [ds:4:4:10] [async-threads:64] [hipe] [dtrace]"));
            Assert.That(result.Data.Node, Is.EqualTo("rabbit@localhost"));;
            Assert.That(result.Data.ObjectTotals?.TotalChannels, Is.EqualTo(3));
            Assert.That(result.Data.ObjectTotals?.TotalConsumers, Is.EqualTo(3));;
            Assert.That(result.Data.ObjectTotals?.TotalConnections, Is.EqualTo(2));;
            Assert.That(result.Data.ObjectTotals?.TotalExchanges, Is.EqualTo(100));;
            Assert.That(result.Data.ObjectTotals?.TotalQueues, Is.EqualTo(11));;;
            Assert.That(result.Data.MessageStats?.TotalMessageGets, Is.EqualTo(7));;;
            Assert.That(result.Data.MessageStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageGetDetails?.Value, Is.EqualTo(0.0M));;
            Assert.That(result.Data.MessageStats?.TotalMessagesAcknowledged, Is.EqualTo(200000));
            Assert.That(result.Data.MessageStats?.MessagesAcknowledgedDetails, Is.Not.Null);;
            Assert.That(result.Data.MessageStats?.MessagesAcknowledgedDetails?.Value, Is.EqualTo(0.0M));;;
            Assert.That(result.Data.MessageStats?.TotalMessagesConfirmed, Is.EqualTo(200000));;
            Assert.That(result.Data.MessageStats?.MessagesConfirmedDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesConfirmedDetails?.Value, Is.EqualTo(0.0M));;;
            Assert.That(result.Data.MessageStats?.TotalMessagesDelivered, Is.EqualTo(200000));;;
            Assert.That(result.Data.MessageStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageDeliveryDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessagesDelivered, Is.EqualTo(200000));
            Assert.That(result.Data.MessageStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessagesRedelivered, Is.EqualTo(7));
            Assert.That(result.Data.MessageStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesPublishedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessagesPublished, Is.EqualTo(200000));
            Assert.That(result.Data.MessageStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageDeliveryGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessageDeliveryGets, Is.EqualTo(200007));
            Assert.That(result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data.MessageStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data.MessageStats?.UnroutableMessagesDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.UnroutableMessagesDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalUnroutableMessages, Is.EqualTo(0));
            Assert.That(result.Data.MessageStats?.DiskReadDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.DiskReadDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalDiskReads, Is.EqualTo(8734));
            Assert.That(result.Data.MessageStats?.DiskWriteDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.DiskWriteDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalDiskWrites, Is.EqualTo(200000));
            Assert.That(result.Data.QueueStats, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.MessageDetails, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.MessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.QueueStats?.TotalMessages, Is.EqualTo(3));
            Assert.That(result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.QueueStats?.TotalUnacknowledgedDeliveredMessages, Is.EqualTo(0));
            Assert.That(result.Data.QueueStats?.MessagesReadyForDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.MessagesReadyForDeliveryDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.QueueStats?.TotalMessagesReadyForDelivery, Is.EqualTo(3));
            Assert.That(result.Data.ChurnRates, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedChannelDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedChannelDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalChannelsClosed, Is.EqualTo(52));
            Assert.That(result.Data.ChurnRates?.CreatedChannelDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.CreatedChannelDetails?.Value, Is.EqualTo(1.4M));
            Assert.That(result.Data.ChurnRates?.TotalChannelsCreated, Is.EqualTo(61));
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalConnectionsClosed, Is.EqualTo(12));
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalConnectionsClosed, Is.EqualTo(12));
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.CreatedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.CreatedConnectionDetails?.Value, Is.EqualTo(0.2M));
            Assert.That(result.Data.ChurnRates?.TotalConnectionsCreated, Is.EqualTo(14));
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.CreatedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.CreatedQueueDetails?.Value, Is.EqualTo(0.2M));
            Assert.That(result.Data.ChurnRates?.TotalQueuesCreated, Is.EqualTo(8));;
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.DeclaredQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeclaredQueueDetails?.Value, Is.EqualTo(0.2M));;
            Assert.That(result.Data.ChurnRates?.TotalQueuesDeclared, Is.EqualTo(10));;
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalQueuesDeleted, Is.EqualTo(5));
            Assert.That(result.Data.RabbitMqVersion, Is.EqualTo("3.8.9"));
            Assert.That(result.Data.ManagementVersion, Is.EqualTo("3.8.9"));
            Assert.That(result.Data.RatesMode, Is.EqualTo("basic"));
            Assert.That(result.Data.ExchangeTypes.Count, Is.EqualTo(4));;
        });
    }

    [Test]
    public async Task Verify_alarms_are_in_effect_1()
    {
        var result = await GetContainerBuilder("TestData/BindingInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsAlarmsInEffect();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(AlarmState.InEffect));
        });
    }

    [Test]
    public async Task Verify_alarms_are_in_effect_2()
    {
        var result = await GetContainerBuilder("TestData/AlarmsInEffect.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsAlarmsInEffect(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(AlarmState.InEffect));
        });
    }

    [Test]
    public async Task Verify_alarms_are_not_in_effect_1()
    {
        var result = await GetContainerBuilder("TestData/AlarmsNotInEffect.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsAlarmsInEffect();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(AlarmState.InEffect));
        });
    }

    [Test]
    public async Task Verify_alarms_are_not_in_effect_2()
    {
        var result = await GetContainerBuilder("TestData/AlarmsNotInEffect.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsAlarmsInEffect(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(AlarmState.NotInEffect));
        });
    }

    [Test]
    public async Task Verify_broker_is_alive_1()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsAlive.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsBrokerAlive("Test1");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(BrokerState.Alive));
        });
    }

    [Test]
    public async Task Verify_broker_is_alive_2()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsAlive.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsBrokerAlive(x => x.UsingCredentials("guest", "guest"), "Test1");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(BrokerState.Alive));
        });
    }

    [Test]
    public async Task Verify_broker_is_dead_1()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsNotAlive.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsBrokerAlive("Test1");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(BrokerState.NotAlive));
        });
    }

    [Test]
    public async Task Verify_broker_is_dead_2()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsNotAlive.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsBrokerAlive(x => x.UsingCredentials("guest", "guest"), "Test1");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(BrokerState.NotAlive));
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_running_1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsRunning.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsVirtualHostsRunning();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(VirtualHostState.Running));
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_running_2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsRunning.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsVirtualHostsRunning(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(VirtualHostState.Running));
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_not_running_1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsNotRunning.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsVirtualHostsRunning();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(VirtualHostState.NotRunning));
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_not_running_2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsNotRunning.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsVirtualHostsRunning(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(VirtualHostState.NotRunning));
        });
    }

    [Test]
    public async Task Verify_node_is_mirror_sync_critical_1()
    {
        var result = await GetContainerBuilder("TestData/MirrorSyncCritical.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsNodeMirrorSyncCritical();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(NodeMirrorSyncState.WithSyncedMirrorsOnline));
        });
    }

    [Test]
    public async Task Verify_node_is_not_mirror_sync_critical_2()
    {
        var result = await GetContainerBuilder("TestData/MirrorNotSyncCritical.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsNodeMirrorSyncCritical(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(NodeMirrorSyncState.WithoutSyncedMirrorsOnline));
        });
    }

    [Test]
    public async Task Verify_quorum_queues_not_critical_1()
    {
        var result = await GetContainerBuilder("TestData/QuorumNotCritical.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsNodeQuorumCritical();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(NodeQuorumState.MinimumQuorum));
        });
    }

    [Test]
    public async Task Verify_quorum_queues_not_critical_2()
    {
        var result = await GetContainerBuilder("TestData/QuorumCritical.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsNodeQuorumCritical(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(NodeQuorumState.BelowMinimumQuorum));
        });
    }

    [Test]
    public async Task Verify_protocol_active_listener_1()
    {
        var result = await GetContainerBuilder("TestData/ProtocolActiveListener.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsProtocolActiveListener(Protocol.AMQP10);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(ProtocolListenerState.Active));
        });
    }

    [Test]
    public async Task Verify_protocol_active_listener_2()
    {
        var result = await GetContainerBuilder("TestData/ProtocolActiveListener.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsProtocolActiveListener(x => x.UsingCredentials("guest", "guest"), Protocol.AMQP10);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(ProtocolListenerState.Active));
        });
    }

    [Test]
    public async Task Verify_protocol_not_active_listener_1()
    {
        var result = await GetContainerBuilder("TestData/ProtocolNotActiveListener.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Broker>(x => x.UsingCredentials("guest", "guest"))
            .IsProtocolActiveListener(Protocol.AMQP091);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(ProtocolListenerState.NotActive));
        });
    }

    [Test]
    public async Task Verify_protocol_not_active_listener_2()
    {
        var result = await GetContainerBuilder("TestData/ProtocolNotActiveListener.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .IsProtocolActiveListener(x => x.UsingCredentials("guest", "guest"), Protocol.AMQP091);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.EqualTo(ProtocolListenerState.NotActive));
        });
    }

    [Test]
    public async Task Verify_can_get_system_overview2()
    {
        var result = await GetContainerBuilder("TestData/SystemOverviewInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetBrokerOverview(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData);
            // Assert.IsNotNull(result.Data.ObjectTotals);
            Assert.That(result.Select(x => x.Data).Select(x => x.ObjectTotals), Is.Not.Null);
            Assert.That(result.Data.QueueStats, Is.Not.Null);
            Assert.That(result.Data.MessageStats, Is.Not.Null);
            Assert.That(result.Data.ChurnRates, Is.Not.Null);
            Assert.That(result.Data.ClusterName, Is.EqualTo("rabbit@haredu"));;
            Assert.That(result.Data.ErlangVersion, Is.EqualTo("23.2"));;
            Assert.That(result.Data.ErlangFullVersion, Is.EqualTo("Erlang/OTP 23 [erts-11.1.4] [source] [64-bit] [smp:4:4] [ds:4:4:10] [async-threads:64] [hipe] [dtrace]"));
            Assert.That(result.Data.Node, Is.EqualTo("rabbit@localhost"));;
            Assert.That(result.Data.ObjectTotals?.TotalChannels, Is.EqualTo(3));
            Assert.That(result.Data.ObjectTotals?.TotalConsumers, Is.EqualTo(3));;
            Assert.That(result.Data.ObjectTotals?.TotalConnections, Is.EqualTo(2));;
            Assert.That(result.Data.ObjectTotals?.TotalExchanges, Is.EqualTo(100));;
            Assert.That(result.Data.ObjectTotals?.TotalQueues, Is.EqualTo(11));;;
            Assert.That(result.Data.MessageStats?.TotalMessageGets, Is.EqualTo(7));;;
            Assert.That(result.Data.MessageStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageGetDetails?.Value, Is.EqualTo(0.0M));;
            Assert.That(result.Data.MessageStats?.TotalMessagesAcknowledged, Is.EqualTo(200000));
            Assert.That(result.Data.MessageStats?.MessagesAcknowledgedDetails, Is.Not.Null);;
            Assert.That(result.Data.MessageStats?.MessagesAcknowledgedDetails?.Value, Is.EqualTo(0.0M));;;
            Assert.That(result.Data.MessageStats?.TotalMessagesConfirmed, Is.EqualTo(200000));;
            Assert.That(result.Data.MessageStats?.MessagesConfirmedDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesConfirmedDetails?.Value, Is.EqualTo(0.0M));;;
            Assert.That(result.Data.MessageStats?.TotalMessagesDelivered, Is.EqualTo(200000));;;
            Assert.That(result.Data.MessageStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageDeliveryDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessagesDelivered, Is.EqualTo(200000));
            Assert.That(result.Data.MessageStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessagesRedelivered, Is.EqualTo(7));
            Assert.That(result.Data.MessageStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesPublishedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessagesPublished, Is.EqualTo(200000));
            Assert.That(result.Data.MessageStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageDeliveryGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessageDeliveryGets, Is.EqualTo(200007));
            Assert.That(result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data.MessageStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data.MessageStats?.UnroutableMessagesDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.UnroutableMessagesDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalUnroutableMessages, Is.EqualTo(0));
            Assert.That(result.Data.MessageStats?.DiskReadDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.DiskReadDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalDiskReads, Is.EqualTo(8734));
            Assert.That(result.Data.MessageStats?.DiskWriteDetails, Is.Not.Null);
            Assert.That(result.Data.MessageStats?.DiskWriteDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.MessageStats?.TotalDiskWrites, Is.EqualTo(200000));
            Assert.That(result.Data.QueueStats, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.MessageDetails, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.MessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.QueueStats?.TotalMessages, Is.EqualTo(3));
            Assert.That(result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.QueueStats?.TotalUnacknowledgedDeliveredMessages, Is.EqualTo(0));
            Assert.That(result.Data.QueueStats?.MessagesReadyForDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data.QueueStats?.MessagesReadyForDeliveryDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.QueueStats?.TotalMessagesReadyForDelivery, Is.EqualTo(3));
            Assert.That(result.Data.ChurnRates, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedChannelDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedChannelDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalChannelsClosed, Is.EqualTo(52));
            Assert.That(result.Data.ChurnRates?.CreatedChannelDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.CreatedChannelDetails?.Value, Is.EqualTo(1.4M));
            Assert.That(result.Data.ChurnRates?.TotalChannelsCreated, Is.EqualTo(61));
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalConnectionsClosed, Is.EqualTo(12));
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.ClosedConnectionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalConnectionsClosed, Is.EqualTo(12));
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.CreatedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.CreatedConnectionDetails?.Value, Is.EqualTo(0.2M));
            Assert.That(result.Data.ChurnRates?.TotalConnectionsCreated, Is.EqualTo(14));
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.CreatedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.CreatedQueueDetails?.Value, Is.EqualTo(0.2M));
            Assert.That(result.Data.ChurnRates?.TotalQueuesCreated, Is.EqualTo(8));;
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.DeclaredQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeclaredQueueDetails?.Value, Is.EqualTo(0.2M));;
            Assert.That(result.Data.ChurnRates?.TotalQueuesDeclared, Is.EqualTo(10));;
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data.ChurnRates?.DeletedQueueDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ChurnRates?.TotalQueuesDeleted, Is.EqualTo(5));
            Assert.That(result.Data.RabbitMqVersion, Is.EqualTo("3.8.9"));
            Assert.That(result.Data.ManagementVersion, Is.EqualTo("3.8.9"));
            Assert.That(result.Data.RatesMode, Is.EqualTo("basic"));
            Assert.That(result.Data.ExchangeTypes.Count, Is.EqualTo(4));;
        });
    }
}