namespace HareDu.Tests
{
    using System;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class QueueTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_queues1()
        {
            var services = GetContainerBuilder("TestData/QueueInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .GetAll();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data[5].MessageStats.ShouldNotBeNull();
            result.Data[5].MessageStats.TotalMessageGets.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessageGetDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageGetDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessagesAcknowledged.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessagesAcknowledgedDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesAcknowledgedDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessagesDelivered.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessageDeliveryDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageDeliveryDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessagesPublished.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessagesPublishedDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesPublishedDetails.Value.ShouldBe(1000.0M);
            result.Data[5].MessageStats.TotalMessagesRedelivered.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessagesRedeliveredDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesRedeliveredDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessageDeliveryGets.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessageDeliveryGetDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageDeliveryGetDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessageDeliveredWithoutAck.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessagesDeliveredWithoutAckDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesDeliveredWithoutAckDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessageGetsWithoutAck.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessageGetsWithoutAckDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageGetsWithoutAckDetails.Value.ShouldBe(0.0M);
            result.Data[5].Consumers.ShouldBe<ulong>(1);
            result.Data[5].Durable.ShouldBeTrue();
            result.Data[5].Exclusive.ShouldBeFalse();
            result.Data[5].AutoDelete.ShouldBeFalse();
            result.Data[5].Memory.ShouldBe<ulong>(17628);
            result.Data[5].MessageBytesPersisted.ShouldBe<ulong>(0);
            result.Data[5].MessageBytesInRAM.ShouldBe<ulong>(100);
            result.Data[5].MessageBytesPagedOut.ShouldBe<ulong>(10);
            result.Data[5].TotalBytesOfAllMessages.ShouldBe<ulong>(10000);
            result.Data[5].UnacknowledgedMessages.ShouldBe<ulong>(30);
            result.Data[5].ReadyMessages.ShouldBe<ulong>(50);
            result.Data[5].MessagesInRAM.ShouldBe<ulong>(50);
            result.Data[5].TotalMessages.ShouldBe<ulong>(6700);
            result.Data[5].UnacknowledgedMessagesInRAM.ShouldBe<ulong>(30000);
            result.Data[5].TotalReductions.ShouldBe(77349645);
            result.Data[5].ReductionDetails.ShouldNotBeNull();
            result.Data[5].ReductionDetails?.Value.ShouldBe(0.0M);
            result.Data[5].UnacknowledgedMessageDetails?.Value.ShouldBe(0.0M);
            result.Data[5].ReadyMessageDetails?.Value.ShouldBe(0.0M);
            result.Data[5].MessageDetails?.Value.ShouldBe(0.0M);
            result.Data[5].Name.ShouldBe("consumer_queue");
            result.Data[5].Node.ShouldBe("rabbit@localhost");
            result.Data[5].IdleSince.ShouldBe(DateTimeOffset.Parse("2019-11-09 11:57:45"));
            result.Data[5].State.ShouldBe("running");
            result.Data[5].VirtualHost.ShouldBe("HareDu");
        }

        [Test]
        public async Task Should_be_able_to_get_all_queues2()
        {
            var services = GetContainerBuilder("TestData/QueueInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllQueues();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data[5].MessageStats.ShouldNotBeNull();
            result.Data[5].MessageStats.TotalMessageGets.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessageGetDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageGetDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessagesAcknowledged.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessagesAcknowledgedDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesAcknowledgedDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessagesDelivered.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessageDeliveryDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageDeliveryDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessagesPublished.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessagesPublishedDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesPublishedDetails.Value.ShouldBe(1000.0M);
            result.Data[5].MessageStats.TotalMessagesRedelivered.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessagesRedeliveredDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesRedeliveredDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessageDeliveryGets.ShouldBe<ulong>(50000);
            result.Data[5].MessageStats.MessageDeliveryGetDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageDeliveryGetDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessageDeliveredWithoutAck.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessagesDeliveredWithoutAckDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessagesDeliveredWithoutAckDetails.Value.ShouldBe(0.0M);
            result.Data[5].MessageStats.TotalMessageGetsWithoutAck.ShouldBe<ulong>(0);
            result.Data[5].MessageStats.MessageGetsWithoutAckDetails.ShouldNotBeNull();
            result.Data[5].MessageStats.MessageGetsWithoutAckDetails.Value.ShouldBe(0.0M);
            result.Data[5].Consumers.ShouldBe<ulong>(1);
            result.Data[5].Durable.ShouldBeTrue();
            result.Data[5].Exclusive.ShouldBeFalse();
            result.Data[5].AutoDelete.ShouldBeFalse();
            result.Data[5].Memory.ShouldBe<ulong>(17628);
            result.Data[5].MessageBytesPersisted.ShouldBe<ulong>(0);
            result.Data[5].MessageBytesInRAM.ShouldBe<ulong>(100);
            result.Data[5].MessageBytesPagedOut.ShouldBe<ulong>(10);
            result.Data[5].TotalBytesOfAllMessages.ShouldBe<ulong>(10000);
            result.Data[5].UnacknowledgedMessages.ShouldBe<ulong>(30);
            result.Data[5].ReadyMessages.ShouldBe<ulong>(50);
            result.Data[5].MessagesInRAM.ShouldBe<ulong>(50);
            result.Data[5].TotalMessages.ShouldBe<ulong>(6700);
            result.Data[5].UnacknowledgedMessagesInRAM.ShouldBe<ulong>(30000);
            result.Data[5].TotalReductions.ShouldBe(77349645);
            result.Data[5].ReductionDetails.ShouldNotBeNull();
            result.Data[5].ReductionDetails?.Value.ShouldBe(0.0M);
            result.Data[5].UnacknowledgedMessageDetails?.Value.ShouldBe(0.0M);
            result.Data[5].ReadyMessageDetails?.Value.ShouldBe(0.0M);
            result.Data[5].MessageDetails?.Value.ShouldBe(0.0M);
            result.Data[5].Name.ShouldBe("consumer_queue");
            result.Data[5].Node.ShouldBe("rabbit@localhost");
            result.Data[5].IdleSince.ShouldBe(DateTimeOffset.Parse("2019-11-09 11:57:45"));
            result.Data[5].State.ShouldBe("running");
            result.Data[5].VirtualHost.ShouldBe("HareDu");
        }

        [Test]
        public async Task Verify_can_peek_messages1()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek("Queue1", "HareDu", x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data[0].ShouldNotBeNull();
            result.Data[0]?.Exchange.ShouldBe("HareDu.IntegrationTesting.Core:FakeMessage");
            result.Data[0]?.Payload.ShouldNotBeNull();
            result.Data[0]?.Payload["messageId"].ToString().ShouldBe("b64a0000-0481-dca9-a948-08d7650c25d3");
            result.Data[0]?.Payload["conversationId"].ToString().ShouldBe("b64a0000-0481-dca9-aac4-08d7650c25d3");
            result.Data[0]?.Properties.ShouldNotBeNull();
            result.Data[0]?.Properties?.ContentType.ShouldNotBeNull();
            result.Data[0]?.Properties?.ContentType.ShouldBe("application/vnd.masstransit+json");
            result.Data[0]?.Properties?.CorrelationId.ShouldBe("b64a0000-0481-dca9-8c2c-08d7650c1eeb");
            result.Data[0]?.Properties?.MessageId.ShouldBe("b64a0000-0481-dca9-a948-08d7650c25d3");
            result.Data[0]?.Properties?.DeliveryMode.ShouldBe<uint>(2);
            result.Data[0]?.MessageCount.ShouldBe<ulong>(49999);
            result.Data[0]?.RoutingKey.ShouldBeNullOrEmpty();
            result.Data[0]?.Redelivered.ShouldBeTrue();
            result.Data[0]?.Properties?.Headers.ShouldNotBeNull();
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);

            result.Data[0]?.Properties?.Headers["Content-Type"].ToString().ShouldBe("application/vnd.masstransit+json");
            result.Data[0]?.Properties?.Headers["publishId"].ToString().ShouldBe("1");
        }

        [Test]
        public async Task Verify_can_peek_messages2()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .PeekQueue("Queue1", "HareDu", x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data[0].ShouldNotBeNull();
            result.Data[0]?.Exchange.ShouldBe("HareDu.IntegrationTesting.Core:FakeMessage");
            result.Data[0]?.Payload.ShouldNotBeNull();
            result.Data[0]?.Payload["messageId"].ToString().ShouldBe("b64a0000-0481-dca9-a948-08d7650c25d3");
            result.Data[0]?.Payload["conversationId"].ToString().ShouldBe("b64a0000-0481-dca9-aac4-08d7650c25d3");
            result.Data[0]?.Properties.ShouldNotBeNull();
            result.Data[0]?.Properties?.ContentType.ShouldNotBeNull();
            result.Data[0]?.Properties?.ContentType.ShouldBe("application/vnd.masstransit+json");
            result.Data[0]?.Properties?.CorrelationId.ShouldBe("b64a0000-0481-dca9-8c2c-08d7650c1eeb");
            result.Data[0]?.Properties?.MessageId.ShouldBe("b64a0000-0481-dca9-a948-08d7650c25d3");
            result.Data[0]?.Properties?.DeliveryMode.ShouldBe<uint>(2);
            result.Data[0]?.MessageCount.ShouldBe<ulong>(49999);
            result.Data[0]?.RoutingKey.ShouldBeNullOrEmpty();
            result.Data[0]?.Redelivered.ShouldBeTrue();
            result.Data[0]?.Properties?.Headers.ShouldNotBeNull();
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);

            result.Data[0]?.Properties?.Headers["Content-Type"].ToString().ShouldBe("application/vnd.masstransit+json");
            result.Data[0]?.Properties?.Headers["publishId"].ToString().ShouldBe("1");
        }

        [Test]
        public async Task Verify_cannot_peek_messages_1()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek(string.Empty, "HareDu", x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_cannot_peek_messages_2()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek(string.Empty,"HareDu",x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_cannot_peek_messages_3()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek("Queue1", string.Empty, x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_cannot_peek_messages_4()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek("Queue1", string.Empty, x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_cannot_peek_messages_5()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek("Queue1", string.Empty, x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_cannot_peek_messages_6()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek(string.Empty, string.Empty, x =>
                {
                    x.Take(1);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(1);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_cannot_peek_messages_9()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek("Queue1", "HareDu", x =>
                {
                    x.Take(0);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBe("auto");
            definition.Take.ShouldBe<uint>(0);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_cannot_peek_messages_15()
        {
            var services = GetContainerBuilder("TestData/PeekedMessageInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Peek("Queue1", string.Empty, x =>
                {
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                });

            result.HasFaulted.ShouldBeTrue();
            result.HasData.ShouldBeFalse();
            result.Data.ShouldBeEmpty();
            result.Errors.Count.ShouldBe(3);
            result.DebugInfo.ShouldNotBeNull();
            
            QueuePeekDefinition definition = result.DebugInfo.Request.ToObject<QueuePeekDefinition>(Deserializer.Options);
            
            definition.Encoding.ShouldBeNullOrEmpty();
            definition.Take.ShouldBe<uint>(0);
            definition.RequeueMode.ShouldBe("ack_requeue_true");
            definition.TruncateMessageThreshold.ShouldBe<ulong>(5000);
        }

        [Test]
        public async Task Verify_can_create_queue1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create("TestQueue31", "HareDu", "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                });

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
//            definition.Arguments["x-expires"].ShouldBe(1000);
//            definition.Arguments["x-message-ttl"].ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_can_create_queue2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateQueue("TestQueue31", "HareDu", "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                });

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
//            definition.Arguments["x-expires"].ShouldBe(1000);
//            definition.Arguments["x-message-ttl"].ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_cannot_create_queue_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create(string.Empty, "HareDu", "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_cannot_create_queue_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create(string.Empty, "HareDu", "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_cannot_create_queue_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create("TestQueue31", string.Empty, "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_cannot_create_queue_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create(string.Empty,"HareDu", "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_cannot_create_queue_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create(string.Empty, string.Empty, "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_cannot_create_queue_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create(string.Empty, string.Empty, "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.SetPerQueuedMessageExpiration(2000);
                    });
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(1000);
            definition.Arguments["x-message-ttl"].Cast<long>().ShouldBe(2000);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_can_override_arguments()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Create("TestQueue31", "HareDu", "Node1", x =>
                {
                    x.IsDurable();
                    x.AutoDeleteWhenNotInUse();
                    x.HasArguments(arg =>
                    {
                        arg.SetQueueExpiration(1000);
                        arg.Set<long>("x-expires", 980);
                    });
                });

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            QueueDefinition definition = result.DebugInfo.Request.ToObject<QueueDefinition>(Deserializer.Options);
            
            definition.Arguments["x-expires"].Cast<long>().ShouldBe(980);
            definition.Durable.ShouldBeTrue();
            definition.AutoDelete.ShouldBeTrue();
            definition.Node.ShouldBe("Node1");
        }

        [Test]
        public async Task Verify_can_delete_queue1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete("Queue1", "HareDu",x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/queues/HareDu/Queue1?if-unused=true");
        }

        [Test]
        public async Task Verify_can_delete_queue2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteQueue("Queue1", "HareDu",x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/queues/HareDu/Queue1?if-unused=true");
        }

        [Test]
        public async Task Verify_cannot_delete_queue_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete(string.Empty, "HareDu", x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_queue_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete(string.Empty, "HareDu", x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_queue_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete("Queue1", string.Empty, x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_queue_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete("Queue1", string.Empty, x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_queue_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete("Queue1", string.Empty, x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_queue_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete(string.Empty, string.Empty, x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_queue_()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Delete(string.Empty, string.Empty, x =>
                {
                    x.When(condition =>
                    {
                        condition.HasNoConsumers();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_can_empty_queue1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("Queue1", "HareDu");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_empty_queue2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .EmptyQueue("Queue1", "HareDu");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_empty_queue_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty(string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty(string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("Queue1", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("Queue1", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty(string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("Queue1", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty(string.Empty, string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }
    }
}