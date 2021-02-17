namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class VirtualHostTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_vhosts()
        {
            var services = GetContainerBuilder("TestData/VirtualHostInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .GetAll()
                .ConfigureAwait(false);
            
            result.HasData.ShouldBeTrue();
            result.Data.Count.ShouldBe(3);
            result.HasFaulted.ShouldBeFalse();
            result.Data[2].Name.ShouldBe("TestVirtualHost");
            result.Data[2].PacketBytesReceived.ShouldBe<ulong>(301363575);
            result.Data[2].PacketBytesReceivedDetails.ShouldNotBeNull();
            result.Data[2].PacketBytesReceivedDetails?.Value.ShouldBe(0.0M);
            result.Data[2].PacketBytesSent.ShouldBe<ulong>(368933935);
            result.Data[2].PacketBytesSentDetails.ShouldNotBeNull();
            result.Data[2].PacketBytesSentDetails?.Value.ShouldBe(0.0M);
            result.Data[2].TotalMessages.ShouldBe<ulong>(0);
            result.Data[2].MessagesDetails.ShouldNotBeNull();
            result.Data[2].MessagesDetails?.Value.ShouldBe(0.0M);
            result.Data[2].ReadyMessages.ShouldBe<ulong>(0);
            result.Data[2].ReadyMessagesDetails.ShouldNotBeNull();
            result.Data[2].ReadyMessagesDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats.ShouldNotBeNull();
            result.Data[2].MessageStats?.TotalMessageGets.ShouldBe<ulong>(3);
            result.Data[2].MessageStats?.MessageGetDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessageGetDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessagesConfirmedDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessagesPublishedDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.UnroutableMessagesDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessagesAcknowledgedDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessageDeliveryDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessageDeliveryGetDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessagesDeliveredWithoutAckDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessageGetDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessageGetsWithoutAckDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.MessagesRedeliveredDetails.ShouldNotBeNull();
            result.Data[2].MessageStats?.TotalMessagesConfirmed.ShouldBe<ulong>(300000);
            result.Data[2].MessageStats?.MessagesConfirmedDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessagesPublished.ShouldBe<ulong>(300000);
            result.Data[2].MessageStats?.MessagesPublishedDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalUnroutableMessages.ShouldBe<ulong>(0);
            result.Data[2].MessageStats?.UnroutableMessagesDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessagesAcknowledged.ShouldBe<ulong>(300000);
            result.Data[2].MessageStats?.MessagesAcknowledgedDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessagesDelivered.ShouldBe<ulong>(300000);
            result.Data[2].MessageStats?.MessageDeliveryDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessageDeliveryGets.ShouldBe<ulong>(300003);
            result.Data[2].MessageStats?.MessageDeliveryGetDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessageDeliveredWithoutAck.ShouldBe<ulong>(0);
            result.Data[2].MessageStats?.MessagesDeliveredWithoutAckDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessageGets.ShouldBe<ulong>(3);
            result.Data[2].MessageStats?.MessageGetDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessageGetsWithoutAck.ShouldBe<ulong>(0);
            result.Data[2].MessageStats?.MessageGetsWithoutAckDetails?.Value.ShouldBe(0.0M);
            result.Data[2].MessageStats?.TotalMessagesRedelivered.ShouldBe<ulong>(3);
            result.Data[2].MessageStats?.MessagesRedeliveredDetails?.Value.ShouldBe(0.0M);
        }

        [Test]
        public async Task Verify_Create_works()
        {
            var services = GetContainerBuilder("TestData/ConsumerInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Create(x =>
                {
                    x.VirtualHost("HareDu7");
                    x.Configure(c =>
                    {
                        c.WithTracingEnabled();
                    });
                })
                .ConfigureAwait(false);
            
            result.DebugInfo.ShouldNotBeNull();
            
            VirtualHostDefinition definition = result.DebugInfo.Request.ToObject<VirtualHostDefinition>(Deserializer.Options);

            definition.Tracing.ShouldBeTrue();
        }

        [Test]
        public async Task Verify_can_delete()
        {
            var services = GetContainerBuilder("TestData/ConsumerInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Delete(x => x.VirtualHost("HareDu7"))
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Delete(x => x.VirtualHost(string.Empty))
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_2()
        {
            var services = GetContainerBuilder("TestData/ConsumerInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Delete(x => {})
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_can_start_vhost()
        {
            var services = GetContainerBuilder("TestData/ConsumerInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Startup(x =>
                {
                    x.VirtualHost("FakeVirtualHost");
                    x.Targeting(t =>
                    {
                        t.Node("FakeNode");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_start_vhost_1()
        {
            var services = GetContainerBuilder("TestData/ConsumerInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Startup(x =>
                {
                    x.VirtualHost(string.Empty);
                    x.Targeting(t =>
                    {
                        t.Node("FakeNode");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_start_vhost_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Startup(x =>
                {
                    x.VirtualHost("FakeVirtualHost");
                    x.Targeting(t =>
                    {
                        t.Node(string.Empty);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_start_vhost_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Startup(x =>
                {
                    x.VirtualHost(string.Empty);
                    x.Targeting(t =>
                    {
                        t.Node(string.Empty);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_start_vhost_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
                .Startup(x =>
                {
                    x.VirtualHost(string.Empty);
                    x.Targeting(t =>
                    {
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }
    }
}