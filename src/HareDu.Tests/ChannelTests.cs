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
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(2, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.Data[0].Confirm);
            Assert.AreEqual(0, result.Data[0].UncommittedAcknowledgements);
            Assert.AreEqual(8, result.Data[0].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[0].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[0].UncommittedMessages);
            Assert.AreEqual(0, result.Data[0].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[0].PrefetchCount);
            Assert.AreEqual(1, result.Data[0].Number);
            Assert.AreEqual(6149, result.Data[0].TotalReductions);
            Assert.IsNotNull(result.Data[0].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[0].ReductionDetails?.Value);
            Assert.IsNotNull(result.Data[0].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[0].ConnectionDetails?.Name);
            Assert.AreEqual("127.0.0.0", result.Data[0].ConnectionDetails?.PeerHost);
            Assert.AreEqual(98343, result.Data[0].ConnectionDetails?.PeerPort);
            Assert.IsFalse(result.Data[0].Transactional);
            Assert.IsNull(result.Data[0].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsTrue(result.Data[1].Confirm);
            Assert.AreEqual(0, result.Data[1].UncommittedAcknowledgements);
            Assert.AreEqual(64, result.Data[1].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[1].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[1].UncommittedMessages);
            Assert.AreEqual(0, result.Data[1].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[1].PrefetchCount);
            Assert.AreEqual(2, result.Data[1].Number);
            Assert.AreEqual(19755338, result.Data[1].TotalReductions);
            Assert.AreEqual(ChannelState.Running, result.Data[1].State);
            Assert.IsFalse(result.Data[1].Transactional);
            Assert.IsNotNull(result.Data[1].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[1].ReductionDetails.Value);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672 (2)", result.Data[1].Name);
            Assert.AreEqual("rabbit@localhost", result.Data[1].Node);
            Assert.AreEqual("guest", result.Data[1].User);
            Assert.AreEqual("guest", result.Data[1].UserWhoPerformedAction);
            Assert.AreEqual("TestVirtualHost", result.Data[1].VirtualHost);
            Assert.IsNotNull(result.Data[1].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[1].ConnectionDetails.Name);
            Assert.AreEqual("127.0.0.0", result.Data[1].ConnectionDetails.PeerHost);
            Assert.AreEqual(98343, result.Data[1].ConnectionDetails.PeerPort);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesConfirmedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesPublishedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesNotRoutedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesAcknowledgedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesConfirmed);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesPublished);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesAcknowledged);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesDelivered);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessageDeliveryGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessagesNotRouted);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGetsWithoutAck);
            Assert.AreEqual(3, result.Data[1].OperationStats?.TotalMessagesRedelivered);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryDetails?.Value);
            Assert.AreEqual(1473.0M, result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value);
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
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(2, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.Data[0].Confirm);
            Assert.AreEqual(0, result.Data[0].UncommittedAcknowledgements);
            Assert.AreEqual(8, result.Data[0].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[0].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[0].UncommittedMessages);
            Assert.AreEqual(0, result.Data[0].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[0].PrefetchCount);
            Assert.AreEqual(1, result.Data[0].Number);
            Assert.AreEqual(6149, result.Data[0].TotalReductions);
            Assert.IsNotNull(result.Data[0].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[0].ReductionDetails?.Value);
            Assert.IsNotNull(result.Data[0].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[0].ConnectionDetails?.Name);
            Assert.AreEqual("127.0.0.0", result.Data[0].ConnectionDetails?.PeerHost);
            Assert.AreEqual(98343, result.Data[0].ConnectionDetails?.PeerPort);
            Assert.IsFalse(result.Data[0].Transactional);
            Assert.IsNull(result.Data[0].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsTrue(result.Data[1].Confirm);
            Assert.AreEqual(0, result.Data[1].UncommittedAcknowledgements);
            Assert.AreEqual(64, result.Data[1].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[1].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[1].UncommittedMessages);
            Assert.AreEqual(0, result.Data[1].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[1].PrefetchCount);
            Assert.AreEqual(2, result.Data[1].Number);
            Assert.AreEqual(19755338, result.Data[1].TotalReductions);
            Assert.AreEqual(ChannelState.Running, result.Data[1].State);
            Assert.IsFalse(result.Data[1].Transactional);
            Assert.IsNotNull(result.Data[1].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[1].ReductionDetails.Value);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672 (2)", result.Data[1].Name);
            Assert.AreEqual("rabbit@localhost", result.Data[1].Node);
            Assert.AreEqual("guest", result.Data[1].User);
            Assert.AreEqual("guest", result.Data[1].UserWhoPerformedAction);
            Assert.AreEqual("TestVirtualHost", result.Data[1].VirtualHost);
            Assert.IsNotNull(result.Data[1].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[1].ConnectionDetails.Name);
            Assert.AreEqual("127.0.0.0", result.Data[1].ConnectionDetails.PeerHost);
            Assert.AreEqual(98343, result.Data[1].ConnectionDetails.PeerPort);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesConfirmedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesPublishedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesNotRoutedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesAcknowledgedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesConfirmed);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesPublished);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesAcknowledged);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesDelivered);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessageDeliveryGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessagesNotRouted);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGetsWithoutAck);
            Assert.AreEqual(3, result.Data[1].OperationStats?.TotalMessagesRedelivered);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryDetails?.Value);
            Assert.AreEqual(1473.0M, result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value);
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
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(2, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.Data[0].Confirm);
            Assert.AreEqual(0, result.Data[0].UncommittedAcknowledgements);
            Assert.AreEqual(8, result.Data[0].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[0].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[0].UncommittedMessages);
            Assert.AreEqual(0, result.Data[0].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[0].PrefetchCount);
            Assert.AreEqual(1, result.Data[0].Number);
            Assert.AreEqual(6149, result.Data[0].TotalReductions);
            Assert.IsNotNull(result.Data[0].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[0].ReductionDetails?.Value);
            Assert.IsNotNull(result.Data[0].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[0].ConnectionDetails?.Name);
            Assert.AreEqual("127.0.0.0", result.Data[0].ConnectionDetails?.PeerHost);
            Assert.AreEqual(98343, result.Data[0].ConnectionDetails?.PeerPort);
            Assert.IsFalse(result.Data[0].Transactional);
            Assert.IsNull(result.Data[0].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsTrue(result.Data[1].Confirm);
            Assert.AreEqual(0, result.Data[1].UncommittedAcknowledgements);
            Assert.AreEqual(64, result.Data[1].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[1].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[1].UncommittedMessages);
            Assert.AreEqual(0, result.Data[1].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[1].PrefetchCount);
            Assert.AreEqual(2, result.Data[1].Number);
            Assert.AreEqual(19755338, result.Data[1].TotalReductions);
            Assert.AreEqual(ChannelState.Running, result.Data[1].State);
            Assert.IsFalse(result.Data[1].Transactional);
            Assert.IsNotNull(result.Data[1].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[1].ReductionDetails.Value);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672 (2)", result.Data[1].Name);
            Assert.AreEqual("rabbit@localhost", result.Data[1].Node);
            Assert.AreEqual("guest", result.Data[1].User);
            Assert.AreEqual("guest", result.Data[1].UserWhoPerformedAction);
            Assert.AreEqual("TestVirtualHost", result.Data[1].VirtualHost);
            Assert.IsNotNull(result.Data[1].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[1].ConnectionDetails.Name);
            Assert.AreEqual("127.0.0.0", result.Data[1].ConnectionDetails.PeerHost);
            Assert.AreEqual(98343, result.Data[1].ConnectionDetails.PeerPort);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesConfirmedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesPublishedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesNotRoutedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesAcknowledgedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesConfirmed);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesPublished);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesAcknowledged);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesDelivered);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessageDeliveryGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessagesNotRouted);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGetsWithoutAck);
            Assert.AreEqual(3, result.Data[1].OperationStats?.TotalMessagesRedelivered);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryDetails?.Value);
            Assert.AreEqual(1473.0M, result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value);
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
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(2, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.Data[0].Confirm);
            Assert.AreEqual(0, result.Data[0].UncommittedAcknowledgements);
            Assert.AreEqual(8, result.Data[0].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[0].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[0].UncommittedMessages);
            Assert.AreEqual(0, result.Data[0].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[0].PrefetchCount);
            Assert.AreEqual(1, result.Data[0].Number);
            Assert.AreEqual(6149, result.Data[0].TotalReductions);
            Assert.IsNotNull(result.Data[0].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[0].ReductionDetails?.Value);
            Assert.IsNotNull(result.Data[0].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[0].ConnectionDetails?.Name);
            Assert.AreEqual("127.0.0.0", result.Data[0].ConnectionDetails?.PeerHost);
            Assert.AreEqual(98343, result.Data[0].ConnectionDetails?.PeerPort);
            Assert.IsFalse(result.Data[0].Transactional);
            Assert.IsNull(result.Data[0].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsTrue(result.Data[1].Confirm);
            Assert.AreEqual(0, result.Data[1].UncommittedAcknowledgements);
            Assert.AreEqual(64, result.Data[1].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[1].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[1].UncommittedMessages);
            Assert.AreEqual(0, result.Data[1].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[1].PrefetchCount);
            Assert.AreEqual(2, result.Data[1].Number);
            Assert.AreEqual(19755338, result.Data[1].TotalReductions);
            Assert.AreEqual(ChannelState.Running, result.Data[1].State);
            Assert.IsFalse(result.Data[1].Transactional);
            Assert.IsNotNull(result.Data[1].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[1].ReductionDetails.Value);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672 (2)", result.Data[1].Name);
            Assert.AreEqual("rabbit@localhost", result.Data[1].Node);
            Assert.AreEqual("guest", result.Data[1].User);
            Assert.AreEqual("guest", result.Data[1].UserWhoPerformedAction);
            Assert.AreEqual("TestVirtualHost", result.Data[1].VirtualHost);
            Assert.IsNotNull(result.Data[1].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[1].ConnectionDetails.Name);
            Assert.AreEqual("127.0.0.0", result.Data[1].ConnectionDetails.PeerHost);
            Assert.AreEqual(98343, result.Data[1].ConnectionDetails.PeerPort);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesConfirmedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesPublishedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesNotRoutedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesAcknowledgedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesConfirmed);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesPublished);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesAcknowledged);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesDelivered);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessageDeliveryGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessagesNotRouted);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGetsWithoutAck);
            Assert.AreEqual(3, result.Data[1].OperationStats?.TotalMessagesRedelivered);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryDetails?.Value);
            Assert.AreEqual(1473.0M, result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value);
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
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(2, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.Data[0].Confirm);
            Assert.AreEqual(0, result.Data[0].UncommittedAcknowledgements);
            Assert.AreEqual(8, result.Data[0].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[0].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[0].UncommittedMessages);
            Assert.AreEqual(0, result.Data[0].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[0].PrefetchCount);
            Assert.AreEqual(1, result.Data[0].Number);
            Assert.AreEqual(6149, result.Data[0].TotalReductions);
            Assert.IsNotNull(result.Data[0].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[0].ReductionDetails?.Value);
            Assert.IsNotNull(result.Data[0].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[0].ConnectionDetails?.Name);
            Assert.AreEqual("127.0.0.0", result.Data[0].ConnectionDetails?.PeerHost);
            Assert.AreEqual(98343, result.Data[0].ConnectionDetails?.PeerPort);
            Assert.IsFalse(result.Data[0].Transactional);
            Assert.IsNull(result.Data[0].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsTrue(result.Data[1].Confirm);
            Assert.AreEqual(0, result.Data[1].UncommittedAcknowledgements);
            Assert.AreEqual(64, result.Data[1].GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data[1].UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data[1].UncommittedMessages);
            Assert.AreEqual(0, result.Data[1].UnconfirmedMessages);
            Assert.AreEqual(0, result.Data[1].PrefetchCount);
            Assert.AreEqual(2, result.Data[1].Number);
            Assert.AreEqual(19755338, result.Data[1].TotalReductions);
            Assert.AreEqual(ChannelState.Running, result.Data[1].State);
            Assert.IsFalse(result.Data[1].Transactional);
            Assert.IsNotNull(result.Data[1].ReductionDetails);
            Assert.AreEqual(0.0M, result.Data[1].ReductionDetails.Value);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672 (2)", result.Data[1].Name);
            Assert.AreEqual("rabbit@localhost", result.Data[1].Node);
            Assert.AreEqual("guest", result.Data[1].User);
            Assert.AreEqual("guest", result.Data[1].UserWhoPerformedAction);
            Assert.AreEqual("TestVirtualHost", result.Data[1].VirtualHost);
            Assert.IsNotNull(result.Data[1].ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data[1].ConnectionDetails.Name);
            Assert.AreEqual("127.0.0.0", result.Data[1].ConnectionDetails.PeerHost);
            Assert.AreEqual(98343, result.Data[1].ConnectionDetails.PeerPort);
            Assert.IsNotNull(result.Data[1].OperationStats);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesConfirmedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesPublishedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesNotRoutedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesAcknowledgedDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageDeliveryGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesDeliveredWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessageGetsWithoutAckDetails);
            Assert.IsNotNull(result.Data[1].OperationStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesConfirmed);
            Assert.AreEqual(3150, result.Data[1].OperationStats?.TotalMessagesPublished);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesAcknowledged);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessagesDelivered);
            Assert.AreEqual(107974, result.Data[1].OperationStats?.TotalMessageDeliveryGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessagesNotRouted);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageDeliveredWithoutAck);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGets);
            Assert.AreEqual(0, result.Data[1].OperationStats?.TotalMessageGetsWithoutAck);
            Assert.AreEqual(3, result.Data[1].OperationStats?.TotalMessagesRedelivered);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesNotRoutedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(1463.8M, result.Data[1].OperationStats.MessageDeliveryDetails?.Value);
            Assert.AreEqual(1473.0M, result.Data[1].OperationStats.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[1].OperationStats.MessagesRedeliveredDetails?.Value);
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
            Assert.IsFalse(result.HasData);
            Assert.IsTrue(result.HasFaulted);
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
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.Data.Confirm);
            Assert.AreEqual(0, result.Data.UncommittedAcknowledgements);
            Assert.AreEqual(8, result.Data.GlobalPrefetchCount);
            Assert.AreEqual(0, result.Data.UnacknowledgedMessages);
            Assert.AreEqual(0, result.Data.UncommittedMessages);
            Assert.AreEqual(0, result.Data.UnconfirmedMessages);
            Assert.AreEqual(0, result.Data.PrefetchCount);
            Assert.AreEqual(1, result.Data.Number);
            Assert.AreEqual(6149, result.Data.TotalReductions);
            Assert.IsNotNull(result.Data.ReductionDetails);
            Assert.AreEqual(0.0M, result.Data.ReductionDetails?.Value);
            Assert.IsNotNull(result.Data.ConnectionDetails);
            Assert.AreEqual("127.0.0.0:72368 -> 127.0.0.0:5672", result.Data.ConnectionDetails?.Name);
            Assert.AreEqual("127.0.0.0", result.Data.ConnectionDetails?.PeerHost);
            Assert.AreEqual(98343, result.Data.ConnectionDetails?.PeerPort);
            Assert.IsFalse(result.Data.Transactional);
            Assert.IsNull(result.Data.OperationStats);
        });
    }
}