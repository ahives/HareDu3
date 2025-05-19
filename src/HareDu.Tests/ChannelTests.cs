namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class ChannelTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_channels1()
    {
        var result = await GetContainerBuilder("TestData/ChannelInfo.json")
            .BuildServiceProvider().GetService<IBrokerFactory>()
            .API<Channel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data[0].Confirm, Is.True);
            Assert.That(result.Data[0].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[0].GlobalPrefetchCount, Is.EqualTo(8));
            Assert.That(result.Data[0].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Number, Is.EqualTo(1));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(6149));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionDetails?.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[0].Transactional, Is.False);
            Assert.That(result.Data[0].OperationStats, Is.Null);
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].Confirm, Is.True);
            Assert.That(result.Data[1].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[1].GlobalPrefetchCount, Is.EqualTo(64));
            Assert.That(result.Data[1].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[1].Number, Is.EqualTo(2));
            Assert.That(result.Data[1].TotalReductions, Is.EqualTo(19755338));
            Assert.That(result.Data[1].State, Is.EqualTo(ChannelState.Running));
            Assert.That(result.Data[1].Transactional, Is.False);
            Assert.That(result.Data[1].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ReductionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672 (2)"));
            Assert.That(result.Data[1].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[1].User, Is.EqualTo("guest"));
            Assert.That(result.Data[1].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[1].VirtualHost, Is.EqualTo("TestVirtualHost"));
            Assert.That(result.Data[1].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ConnectionDetails.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[1].ConnectionDetails.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[1].ConnectionDetails.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesAcknowledgedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.TotalMessagesConfirmed, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesPublished, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesAcknowledged, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesDelivered, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveryGets, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesNotRouted, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGets, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesRedelivered, Is.EqualTo(3));
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value, Is.EqualTo(1473M));
            Assert.That(result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
        });
    }

    [Test]
    public async Task Verify_can_get_all_channels2()
    {
        var result = await GetContainerBuilder("TestData/ChannelInfo.json")
            .BuildServiceProvider().GetService<IBrokerFactory>()
            .API<Channel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll(x =>
            {
                x.Name("127.0.0.0:72368 -> 127.0.0.0:5672");
                x.Page(1);
                x.PageSize(2);
                x.UseRegex(false);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data[0].Confirm, Is.True);
            Assert.That(result.Data[0].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[0].GlobalPrefetchCount, Is.EqualTo(8));
            Assert.That(result.Data[0].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Number, Is.EqualTo(1));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(6149));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionDetails?.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[0].Transactional, Is.False);
            Assert.That(result.Data[0].OperationStats, Is.Null);
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].Confirm, Is.True);
            Assert.That(result.Data[1].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[1].GlobalPrefetchCount, Is.EqualTo(64));
            Assert.That(result.Data[1].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[1].Number, Is.EqualTo(2));
            Assert.That(result.Data[1].TotalReductions, Is.EqualTo(19755338));
            Assert.That(result.Data[1].State, Is.EqualTo(ChannelState.Running));
            Assert.That(result.Data[1].Transactional, Is.False);
            Assert.That(result.Data[1].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ReductionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672 (2)"));
            Assert.That(result.Data[1].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[1].User, Is.EqualTo("guest"));
            Assert.That(result.Data[1].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[1].VirtualHost, Is.EqualTo("TestVirtualHost"));
            Assert.That(result.Data[1].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ConnectionDetails.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[1].ConnectionDetails.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[1].ConnectionDetails.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesAcknowledgedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.TotalMessagesConfirmed, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesPublished, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesAcknowledged, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesDelivered, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveryGets, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesNotRouted, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGets, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesRedelivered, Is.EqualTo(3));
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value, Is.EqualTo(1473M));
            Assert.That(result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
        });
    }

    [Test]
    public async Task Verify_can_get_all_channels_ext()
    {
        var result = await GetContainerBuilder("TestData/ChannelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllChannels(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data[0].Confirm, Is.True);
            Assert.That(result.Data[0].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[0].GlobalPrefetchCount, Is.EqualTo(8));
            Assert.That(result.Data[0].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Number, Is.EqualTo(1));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(6149));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionDetails?.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[0].Transactional, Is.False);
            Assert.That(result.Data[0].OperationStats, Is.Null);
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].Confirm, Is.True);
            Assert.That(result.Data[1].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[1].GlobalPrefetchCount, Is.EqualTo(64));
            Assert.That(result.Data[1].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[1].Number, Is.EqualTo(2));
            Assert.That(result.Data[1].TotalReductions, Is.EqualTo(19755338));
            Assert.That(result.Data[1].State, Is.EqualTo(ChannelState.Running));
            Assert.That(result.Data[1].Transactional, Is.False);
            Assert.That(result.Data[1].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ReductionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672 (2)"));
            Assert.That(result.Data[1].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[1].User, Is.EqualTo("guest"));
            Assert.That(result.Data[1].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[1].VirtualHost, Is.EqualTo("TestVirtualHost"));
            Assert.That(result.Data[1].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ConnectionDetails.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[1].ConnectionDetails.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[1].ConnectionDetails.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesAcknowledgedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.TotalMessagesConfirmed, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesPublished, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesAcknowledged, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesDelivered, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveryGets, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesNotRouted, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGets, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesRedelivered, Is.EqualTo(3));
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value, Is.EqualTo(1473M));
            Assert.That(result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
        });
    }

    [Test]
    public async Task Verify_can_get_all_channels_by_connection()
    {
        var result = await GetContainerBuilder("TestData/ChannelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetChannelsByConnection(x => x.UsingCredentials("guest", "guest"), "test");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data[0].Confirm, Is.True);
            Assert.That(result.Data[0].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[0].GlobalPrefetchCount, Is.EqualTo(8));
            Assert.That(result.Data[0].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Number, Is.EqualTo(1));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(6149));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionDetails?.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[0].Transactional, Is.False);
            Assert.That(result.Data[0].OperationStats, Is.Null);
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].Confirm, Is.True);
            Assert.That(result.Data[1].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[1].GlobalPrefetchCount, Is.EqualTo(64));
            Assert.That(result.Data[1].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[1].Number, Is.EqualTo(2));
            Assert.That(result.Data[1].TotalReductions, Is.EqualTo(19755338));
            Assert.That(result.Data[1].State, Is.EqualTo(ChannelState.Running));
            Assert.That(result.Data[1].Transactional, Is.False);
            Assert.That(result.Data[1].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ReductionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672 (2)"));
            Assert.That(result.Data[1].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[1].User, Is.EqualTo("guest"));
            Assert.That(result.Data[1].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[1].VirtualHost, Is.EqualTo("TestVirtualHost"));
            Assert.That(result.Data[1].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ConnectionDetails.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[1].ConnectionDetails.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[1].ConnectionDetails.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesAcknowledgedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.TotalMessagesConfirmed, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesPublished, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesAcknowledged, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesDelivered, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveryGets, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesNotRouted, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGets, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesRedelivered, Is.EqualTo(3));
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value, Is.EqualTo(1473M));
            Assert.That(result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
        });
    }

    [Test]
    public async Task Verify_cannot_get_channels_when_connection_missing1()
    {
        var result = await GetContainerBuilder("TestData/ChannelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetChannelsByConnection(x => x.UsingCredentials("guest", "guest"), "test");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data[0].Confirm, Is.True);
            Assert.That(result.Data[0].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[0].GlobalPrefetchCount, Is.EqualTo(8));
            Assert.That(result.Data[0].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[0].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Number, Is.EqualTo(1));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(6149));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionDetails?.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].ConnectionDetails?.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[0].Transactional, Is.False);
            Assert.That(result.Data[0].OperationStats, Is.Null);
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].Confirm, Is.True);
            Assert.That(result.Data[1].UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data[1].GlobalPrefetchCount, Is.EqualTo(64));
            Assert.That(result.Data[1].UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data[1].PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data[1].Number, Is.EqualTo(2));
            Assert.That(result.Data[1].TotalReductions, Is.EqualTo(19755338));
            Assert.That(result.Data[1].State, Is.EqualTo(ChannelState.Running));
            Assert.That(result.Data[1].Transactional, Is.False);
            Assert.That(result.Data[1].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ReductionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672 (2)"));
            Assert.That(result.Data[1].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[1].User, Is.EqualTo("guest"));
            Assert.That(result.Data[1].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[1].VirtualHost, Is.EqualTo("TestVirtualHost"));
            Assert.That(result.Data[1].ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[1].ConnectionDetails.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data[1].ConnectionDetails.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[1].ConnectionDetails.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data[1].OperationStats, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesAcknowledgedDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data[1].OperationStats?.TotalMessagesConfirmed, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesPublished, Is.EqualTo(3150));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesAcknowledged, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesDelivered, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveryGets, Is.EqualTo(107974));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesNotRouted, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGets, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[1].OperationStats?.TotalMessagesRedelivered, Is.EqualTo(3));
            Assert.That(result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats?.MessagesPublishedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessageDeliveryDetails?.Value, Is.EqualTo(1463.8M));
            Assert.That(result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value, Is.EqualTo(1473M));
            Assert.That(result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
        });
    }

    [Test]
    public async Task Verify_cannot_get_channels_when_connection_missing2()
    {
        var result = await GetContainerBuilder("TestData/ChannelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetChannelsByConnection(x => x.UsingCredentials("guest", "guest"), string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.False);
            Assert.That(result.HasFaulted, Is.False);
        });
    }

    [Test]
    public async Task Verify_can_get_all_channels_by_connection_name()
    {
        var result = await GetContainerBuilder("TestData/ChannelInfo2.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetChannelByName(x => x.UsingCredentials("guest", "guest"), "test");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data.Confirm, Is.True);
            Assert.That(result.Data.UncommittedAcknowledgements, Is.EqualTo(0));
            Assert.That(result.Data.GlobalPrefetchCount, Is.EqualTo(8));
            Assert.That(result.Data.UnacknowledgedMessages, Is.EqualTo(0));
            Assert.That(result.Data.UncommittedMessages, Is.EqualTo(0));
            Assert.That(result.Data.UnconfirmedMessages, Is.EqualTo(0));
            Assert.That(result.Data.PrefetchCount, Is.EqualTo(0));
            Assert.That(result.Data.Number, Is.EqualTo(1));
            Assert.That(result.Data.TotalReductions, Is.EqualTo(6149));
            Assert.That(result.Data.ReductionDetails, Is.Not.Null);
            Assert.That(result.Data.ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data.ConnectionDetails, Is.Not.Null);
            Assert.That(result.Data.ConnectionDetails?.Name, Is.EqualTo("127.0.0.0:72368 -> 127.0.0.0:5672"));
            Assert.That(result.Data.ConnectionDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data.ConnectionDetails?.PeerPort, Is.EqualTo(98343));
            Assert.That(result.Data.Transactional, Is.False);
            Assert.That(result.Data.OperationStats, Is.Null);
        });
    }
}