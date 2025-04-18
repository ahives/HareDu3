namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class VirtualHostTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_vhosts1()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("TestVirtualHost", result.Data[2].Name);
            Assert.AreEqual(301363575, result.Data[2].PacketBytesReceived);
            Assert.IsNotNull(result.Data[2].PacketBytesReceivedDetails);
            Assert.AreEqual(0.0M, result.Data[2].PacketBytesReceivedDetails?.Value);
            Assert.AreEqual(368933935, result.Data[2].PacketBytesSent);
            Assert.IsNotNull(result.Data[2].PacketBytesSentDetails);
            Assert.AreEqual(0.0M, result.Data[2].PacketBytesSentDetails?.Value);
            Assert.AreEqual(0, result.Data[2].TotalMessages);
            Assert.IsNotNull(result.Data[2].MessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].MessagesDetails?.Value);
            Assert.AreEqual(0, result.Data[2].ReadyMessages);
            Assert.IsNotNull(result.Data[2].ReadyMessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].ReadyMessagesDetails?.Value);
            Assert.IsNotNull(result.Data[2].MessageStats);
            Assert.AreEqual(3, result.Data[2].MessageStats?.TotalMessageGets);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageGetDetails);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageGetDetails?.Value);
            Assert.IsNotNull(result.Data[2].MessageStats);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesConfirmedDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesPublishedDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.UnroutableMessagesDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesAcknowledgedDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageDeliveryDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageDeliveryGetDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesDeliveredWithoutAckDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageGetDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageGetsWithoutAckDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesConfirmed);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesPublished);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(0, result.Data[2].MessageStats?.TotalUnroutableMessages);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.UnroutableMessagesDetails?.Value);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesAcknowledged);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesDelivered);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageDeliveryDetails?.Value);
            Assert.AreEqual(300003, result.Data[2].MessageStats?.TotalMessageDeliveryGets);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(0, result.Data[2].MessageStats?.TotalMessageDeliveredWithoutAck);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(3, result.Data[2].MessageStats?.TotalMessageGets);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageGetDetails?.Value);
            Assert.AreEqual(0, result.Data[2].MessageStats?.TotalMessageGetsWithoutAck);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(3, result.Data[2].MessageStats?.TotalMessagesRedelivered);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesRedeliveredDetails?.Value);
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_vhosts2()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .GetAllVirtualHosts();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("TestVirtualHost", result.Data[2].Name);
            Assert.AreEqual(301363575, result.Data[2].PacketBytesReceived);
            Assert.IsNotNull(result.Data[2].PacketBytesReceivedDetails);
            Assert.AreEqual(0.0M, result.Data[2].PacketBytesReceivedDetails?.Value);
            Assert.AreEqual(368933935, result.Data[2].PacketBytesSent);
            Assert.IsNotNull(result.Data[2].PacketBytesSentDetails);
            Assert.AreEqual(0.0M, result.Data[2].PacketBytesSentDetails?.Value);
            Assert.AreEqual(0, result.Data[2].TotalMessages);
            Assert.IsNotNull(result.Data[2].MessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].MessagesDetails?.Value);
            Assert.AreEqual(0, result.Data[2].ReadyMessages);
            Assert.IsNotNull(result.Data[2].ReadyMessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].ReadyMessagesDetails?.Value);
            Assert.IsNotNull(result.Data[2].MessageStats);
            Assert.AreEqual(3, result.Data[2].MessageStats?.TotalMessageGets);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageGetDetails);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageGetDetails?.Value);
            Assert.IsNotNull(result.Data[2].MessageStats);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesConfirmedDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesPublishedDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.UnroutableMessagesDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesAcknowledgedDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageDeliveryDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageDeliveryGetDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesDeliveredWithoutAckDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageGetDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessageGetsWithoutAckDetails);
            Assert.IsNotNull(result.Data[2].MessageStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesConfirmed);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesConfirmedDetails?.Value);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesPublished);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(0, result.Data[2].MessageStats?.TotalUnroutableMessages);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.UnroutableMessagesDetails?.Value);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesAcknowledged);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(300000, result.Data[2].MessageStats?.TotalMessagesDelivered);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageDeliveryDetails?.Value);
            Assert.AreEqual(300003, result.Data[2].MessageStats?.TotalMessageDeliveryGets);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(0, result.Data[2].MessageStats?.TotalMessageDeliveredWithoutAck);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(3, result.Data[2].MessageStats?.TotalMessageGets);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageGetDetails?.Value);
            Assert.AreEqual(0, result.Data[2].MessageStats?.TotalMessageGetsWithoutAck);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(3, result.Data[2].MessageStats?.TotalMessagesRedelivered);
            Assert.AreEqual(0.0M, result.Data[2].MessageStats?.MessagesRedeliveredDetails?.Value);
        });
    }

    [Test]
    public async Task Verify_Create_works1()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Create("HareDu7",x =>
            {
                x.WithTracingEnabled();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            VirtualHostRequest request = result.DebugInfo.Request.ToObject<VirtualHostRequest>();

            Assert.IsTrue(request.Tracing);
        });
    }

    [Test]
    public async Task Verify_Create_works2()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateVirtualHost("HareDu7",x =>
            {
                x.WithTracingEnabled();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            VirtualHostRequest request = result.DebugInfo.Request.ToObject<VirtualHostRequest>();

            Assert.IsTrue(request.Tracing);
        });
    }

    [Test]
    public async Task Verify_can_delete1()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Delete("HareDu7");

        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete2()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteVirtualHost("HareDu7");

        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_delete_1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Delete(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_start_vhost1()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Startup("FakeVirtualHost", "FakeNode");

        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_start_vhost2()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .StartupVirtualHost("FakeVirtualHost", "FakeNode");

        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_start_vhost_1()
    {
        var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Startup(string.Empty, "FakeNode");

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_start_vhost_2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Startup("FakeVirtualHost", string.Empty);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_start_vhost_3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Startup(string.Empty, string.Empty);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }
}