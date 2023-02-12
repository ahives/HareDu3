namespace HareDu.Tests;

using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Extensions;

[TestFixture]
public class BrokerTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_system_overview1()
    {
        var result = await GetContainerBuilder("TestData/SystemOverviewInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .GetOverview();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data.ObjectTotals);
            Assert.IsNotNull(result.Data.QueueStats);
            Assert.IsNotNull(result.Data.MessageStats);
            Assert.IsNotNull(result.Data.ChurnRates);
            Assert.AreEqual("rabbit@haredu", result.Data.ClusterName);
            Assert.AreEqual("23.2", result.Data.ErlangVersion);
            Assert.AreEqual("Erlang/OTP 23 [erts-11.1.4] [source] [64-bit] [smp:4:4] [ds:4:4:10] [async-threads:64] [hipe] [dtrace]", result.Data.ErlangFullVersion);
            Assert.AreEqual("rabbit@localhost", result.Data.Node);
            Assert.AreEqual(3, result.Data.ObjectTotals?.TotalChannels);
            Assert.AreEqual(3, result.Data.ObjectTotals?.TotalConsumers);
            Assert.AreEqual(2, result.Data.ObjectTotals?.TotalConnections);
            Assert.AreEqual(100, result.Data.ObjectTotals?.TotalExchanges);
            Assert.AreEqual(11, result.Data.ObjectTotals?.TotalQueues);
            Assert.AreEqual(7, result.Data.MessageStats?.TotalMessageGets);
            Assert.IsNotNull(result.Data.MessageStats?.MessageGetDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageGetDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesAcknowledged);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesAcknowledgedDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesConfirmed);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesConfirmedDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesDelivered);
            Assert.IsNotNull(result.Data.MessageStats?.MessageDeliveryDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageDeliveryDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesDelivered);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesRedeliveredDetails?.Value);
            Assert.AreEqual(7, result.Data.MessageStats?.TotalMessagesRedelivered);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesPublishedDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesPublished);
            Assert.IsNotNull(result.Data.MessageStats?.MessageDeliveryGetDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(200007, result.Data.MessageStats?.TotalMessageDeliveryGets);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0, result.Data.MessageStats?.TotalMessageDeliveredWithoutAck);
            Assert.IsNotNull(result.Data.MessageStats?.MessageGetsWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(0, result.Data.MessageStats?.TotalMessageGetsWithoutAck);
            Assert.IsNotNull(result.Data.MessageStats?.UnroutableMessagesDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.UnroutableMessagesDetails?.Value);
            Assert.AreEqual(0, result.Data.MessageStats?.TotalUnroutableMessages);
            Assert.IsNotNull(result.Data.MessageStats?.DiskReadDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.DiskReadDetails?.Value);
            Assert.AreEqual(8734, result.Data.MessageStats?.TotalDiskReads);
            Assert.IsNotNull(result.Data.MessageStats?.DiskWriteDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.DiskWriteDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalDiskWrites);
            Assert.IsNotNull(result.Data.QueueStats);
            Assert.IsNotNull(result.Data.QueueStats?.MessageDetails);
            Assert.AreEqual(0.0M, result.Data.QueueStats?.MessageDetails?.Value);
            Assert.AreEqual(3, result.Data.QueueStats?.TotalMessages);
            Assert.IsNotNull(result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails);
            Assert.AreEqual(0.0M, result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails?.Value);
            Assert.AreEqual(0, result.Data.QueueStats?.TotalUnacknowledgedDeliveredMessages);
            Assert.IsNotNull(result.Data.QueueStats?.MessagesReadyForDeliveryDetails);
            Assert.AreEqual(0.0M, result.Data.QueueStats?.MessagesReadyForDeliveryDetails?.Value);
            Assert.AreEqual(3, result.Data.QueueStats?.TotalMessagesReadyForDelivery);
            Assert.IsNotNull(result.Data.ChurnRates);
            Assert.IsNotNull(result.Data.ChurnRates?.ClosedChannelDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.ClosedChannelDetails?.Value);
            Assert.AreEqual(52, result.Data.ChurnRates?.TotalChannelsClosed);
            Assert.IsNotNull(result.Data.ChurnRates?.CreatedChannelDetails);
            Assert.AreEqual(1.4M, result.Data.ChurnRates?.CreatedChannelDetails?.Value);
            Assert.AreEqual(61, result.Data.ChurnRates?.TotalChannelsCreated);
            Assert.IsNotNull(result.Data.ChurnRates?.ClosedConnectionDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.ClosedConnectionDetails?.Value);
            Assert.AreEqual(12, result.Data.ChurnRates?.TotalConnectionsClosed);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.ClosedConnectionDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.ClosedConnectionDetails?.Value);
            Assert.AreEqual(12, result.Data.ChurnRates?.TotalConnectionsClosed);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.CreatedConnectionDetails);
            Assert.AreEqual(0.2M, result.Data.ChurnRates?.CreatedConnectionDetails?.Value);
            Assert.AreEqual(14, result.Data.ChurnRates?.TotalConnectionsCreated);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.CreatedQueueDetails);
            Assert.AreEqual(0.2M, result.Data.ChurnRates?.CreatedQueueDetails?.Value);
            Assert.AreEqual(8, result.Data.ChurnRates?.TotalQueuesCreated);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.DeclaredQueueDetails);
            Assert.AreEqual(0.2M, result.Data.ChurnRates?.DeclaredQueueDetails?.Value);
            Assert.AreEqual(10, result.Data.ChurnRates?.TotalQueuesDeclared);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.AreEqual(5, result.Data.ChurnRates?.TotalQueuesDeleted);
            Assert.AreEqual("3.8.9", result.Data.RabbitMqVersion);
            Assert.AreEqual("3.8.9", result.Data.ManagementVersion);
            Assert.AreEqual("basic", result.Data.RatesMode);
            Assert.AreEqual(4, result.Data.ExchangeTypes.Count);
        });
    }

    [Test]
    public async Task Verify_alarms_are_in_effect_1()
    {
        var result = await GetContainerBuilder("TestData/AlarmsInEffect.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsAlarmsInEffect();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == AlarmState.InEffect);
        });
    }

    [Test]
    public async Task Verify_alarms_are_in_effect_2()
    {
        var result = await GetContainerBuilder("TestData/AlarmsInEffect.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsAlarmsInEffect();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == AlarmState.InEffect);
        });
    }

    [Test]
    public async Task Verify_alarms_are_not_in_effect_1()
    {
        var result = await GetContainerBuilder("TestData/AlarmsNotInEffect.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsAlarmsInEffect();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == AlarmState.NotInEffect);
        });
    }

    [Test]
    public async Task Verify_alarms_are_not_in_effect_2()
    {
        var result = await GetContainerBuilder("TestData/AlarmsNotInEffect.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsAlarmsInEffect();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == AlarmState.NotInEffect);
        });
    }

    [Test]
    public async Task Verify_broker_is_alive_1()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsAlive.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsBrokerAlive("Test1");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == BrokerState.Alive);
        });
    }

    [Test]
    public async Task Verify_broker_is_alive_2()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsAlive.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsBrokerAlive("Test1");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == BrokerState.Alive);
        });
    }

    [Test]
    public async Task Verify_broker_is_dead_1()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsNotAlive.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsBrokerAlive("Test1");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == BrokerState.NotAlive);
        });
    }

    [Test]
    public async Task Verify_broker_is_dead_2()
    {
        var result = await GetContainerBuilder("TestData/BrokerIsNotAlive.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsBrokerAlive("Test1");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == BrokerState.NotAlive);
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_running_1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsRunning.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsVirtualHostsRunning();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == VirtualHostState.Running);
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_running_2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsRunning.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsVirtualHostsRunning();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == VirtualHostState.Running);
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_not_running_1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsNotRunning.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsVirtualHostsRunning();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == VirtualHostState.NotRunning);
        });
    }

    [Test]
    public async Task Verify_broker_vhosts_are_not_running_2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostsNotRunning.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsVirtualHostsRunning();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == VirtualHostState.NotRunning);
        });
    }

    [Test]
    public async Task Verify_node_is_mirror_sync_critical_1()
    {
        var result = await GetContainerBuilder("TestData/MirrorSyncCritical.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsNodeMirrorSyncCritical();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == NodeMirrorSyncState.WithSyncedMirrorsOnline);
        });
    }

    [Test]
    public async Task Verify_node_is_not_mirror_sync_critical_2()
    {
        var result = await GetContainerBuilder("TestData/MirrorNotSyncCritical.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsNodeMirrorSyncCritical();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == NodeMirrorSyncState.WithoutSyncedMirrorsOnline);
        });
    }

    [Test]
    public async Task Verify_quorum_queues_not_critical_1()
    {
        var result = await GetContainerBuilder("TestData/QuorumNotCritical.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsNodeQuorumCritical();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == NodeQuorumState.MinimumQuorum);
        });
    }

    [Test]
    public async Task Verify_quorum_queues_not_critical_2()
    {
        var result = await GetContainerBuilder("TestData/QuorumCritical.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsNodeQuorumCritical();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == NodeQuorumState.BelowMinimumQuorum);
        });
    }

    [Test]
    public async Task Verify_protocol_active_listener_1()
    {
        var result = await GetContainerBuilder("TestData/ProtocolActiveListener.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsProtocolAnActiveListener(x => x.Amqp10());

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == ProtocolListenerState.Active);
        });
    }

    [Test]
    public async Task Verify_protocol_active_listener_2()
    {
        var result = await GetContainerBuilder("TestData/ProtocolActiveListener.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsProtocolActiveListener(x => x.Amqp10());

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == ProtocolListenerState.Active);
        });
    }

    [Test]
    public async Task Verify_protocol_not_active_listener_1()
    {
        var result = await GetContainerBuilder("TestData/ProtocolNotActiveListener.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .API<Broker>()
            .IsProtocolAnActiveListener(x => x.Amqp091());

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == ProtocolListenerState.NotActive);
        });
    }

    [Test]
    public async Task Verify_protocol_not_active_listener_2()
    {
        var result = await GetContainerBuilder("TestData/ProtocolNotActiveListener.json", HttpStatusCode.ServiceUnavailable)
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .IsProtocolActiveListener(x => x.Amqp091());

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsTrue(result.Data == ProtocolListenerState.NotActive);
        });
    }

    [Test]
    public async Task Verify_can_get_system_overview2()
    {
        var result = await GetContainerBuilder("TestData/SystemOverviewInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerApiFactory>()
            .GetBrokerOverview();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data.ObjectTotals);
            Assert.IsNotNull(result.Data.QueueStats);
            Assert.IsNotNull(result.Data.MessageStats);
            Assert.IsNotNull(result.Data.ChurnRates);
            Assert.AreEqual("rabbit@haredu", result.Data.ClusterName);
            Assert.AreEqual("23.2", result.Data.ErlangVersion);
            Assert.AreEqual("Erlang/OTP 23 [erts-11.1.4] [source] [64-bit] [smp:4:4] [ds:4:4:10] [async-threads:64] [hipe] [dtrace]", result.Data.ErlangFullVersion);
            Assert.AreEqual("rabbit@localhost", result.Data.Node);
            Assert.AreEqual(3, result.Data.ObjectTotals?.TotalChannels);
            Assert.AreEqual(3, result.Data.ObjectTotals?.TotalConsumers);
            Assert.AreEqual(2, result.Data.ObjectTotals?.TotalConnections);
            Assert.AreEqual(100, result.Data.ObjectTotals?.TotalExchanges);
            Assert.AreEqual(11, result.Data.ObjectTotals?.TotalQueues);
            Assert.AreEqual(7, result.Data.MessageStats?.TotalMessageGets);
            Assert.IsNotNull(result.Data.MessageStats?.MessageGetDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageGetDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesAcknowledged);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesAcknowledgedDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesConfirmed);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesConfirmedDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesDelivered);
            Assert.IsNotNull(result.Data.MessageStats?.MessageDeliveryDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageDeliveryDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesDelivered);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesRedeliveredDetails?.Value);
            Assert.AreEqual(7, result.Data.MessageStats?.TotalMessagesRedelivered);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesPublishedDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalMessagesPublished);
            Assert.IsNotNull(result.Data.MessageStats?.MessageDeliveryGetDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(200007, result.Data.MessageStats?.TotalMessageDeliveryGets);
            Assert.IsNotNull(result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0, result.Data.MessageStats?.TotalMessageDeliveredWithoutAck);
            Assert.IsNotNull(result.Data.MessageStats?.MessageGetsWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(0, result.Data.MessageStats?.TotalMessageGetsWithoutAck);
            Assert.IsNotNull(result.Data.MessageStats?.UnroutableMessagesDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.UnroutableMessagesDetails?.Value);
            Assert.AreEqual(0, result.Data.MessageStats?.TotalUnroutableMessages);
            Assert.IsNotNull(result.Data.MessageStats?.DiskReadDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.DiskReadDetails?.Value);
            Assert.AreEqual(8734, result.Data.MessageStats?.TotalDiskReads);
            Assert.IsNotNull(result.Data.MessageStats?.DiskWriteDetails);
            Assert.AreEqual(0.0M, result.Data.MessageStats?.DiskWriteDetails?.Value);
            Assert.AreEqual(200000, result.Data.MessageStats?.TotalDiskWrites);
            Assert.IsNotNull(result.Data.QueueStats);
            Assert.IsNotNull(result.Data.QueueStats?.MessageDetails);
            Assert.AreEqual(0.0M, result.Data.QueueStats?.MessageDetails?.Value);
            Assert.AreEqual(3, result.Data.QueueStats?.TotalMessages);
            Assert.IsNotNull(result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails);
            Assert.AreEqual(0.0M, result.Data.QueueStats?.UnacknowledgedDeliveredMessagesDetails?.Value);
            Assert.AreEqual(0, result.Data.QueueStats?.TotalUnacknowledgedDeliveredMessages);
            Assert.IsNotNull(result.Data.QueueStats?.MessagesReadyForDeliveryDetails);
            Assert.AreEqual(0.0M, result.Data.QueueStats?.MessagesReadyForDeliveryDetails?.Value);
            Assert.AreEqual(3, result.Data.QueueStats?.TotalMessagesReadyForDelivery);
            Assert.IsNotNull(result.Data.ChurnRates);
            Assert.IsNotNull(result.Data.ChurnRates?.ClosedChannelDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.ClosedChannelDetails?.Value);
            Assert.AreEqual(52, result.Data.ChurnRates?.TotalChannelsClosed);
            Assert.IsNotNull(result.Data.ChurnRates?.CreatedChannelDetails);
            Assert.AreEqual(1.4M, result.Data.ChurnRates?.CreatedChannelDetails?.Value);
            Assert.AreEqual(61, result.Data.ChurnRates?.TotalChannelsCreated);
            Assert.IsNotNull(result.Data.ChurnRates?.ClosedConnectionDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.ClosedConnectionDetails?.Value);
            Assert.AreEqual(12, result.Data.ChurnRates?.TotalConnectionsClosed);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.ClosedConnectionDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.ClosedConnectionDetails?.Value);
            Assert.AreEqual(12, result.Data.ChurnRates?.TotalConnectionsClosed);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.CreatedConnectionDetails);
            Assert.AreEqual(0.2M, result.Data.ChurnRates?.CreatedConnectionDetails?.Value);
            Assert.AreEqual(14, result.Data.ChurnRates?.TotalConnectionsCreated);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.CreatedQueueDetails);
            Assert.AreEqual(0.2M, result.Data.ChurnRates?.CreatedQueueDetails?.Value);
            Assert.AreEqual(8, result.Data.ChurnRates?.TotalQueuesCreated);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.IsNotNull(result.Data.ChurnRates?.DeclaredQueueDetails);
            Assert.AreEqual(0.2M, result.Data.ChurnRates?.DeclaredQueueDetails?.Value);
            Assert.AreEqual(10, result.Data.ChurnRates?.TotalQueuesDeclared);
            Assert.IsNotNull(result.Data.ChurnRates?.DeletedQueueDetails);
            Assert.AreEqual(0.0M, result.Data.ChurnRates?.DeletedQueueDetails?.Value);
            Assert.AreEqual(5, result.Data.ChurnRates?.TotalQueuesDeleted);
            Assert.AreEqual("3.8.9", result.Data.RabbitMqVersion);
            Assert.AreEqual("3.8.9", result.Data.ManagementVersion);
            Assert.AreEqual("basic", result.Data.RatesMode);
            Assert.AreEqual(4, result.Data.ExchangeTypes.Count);
        });
    }
}