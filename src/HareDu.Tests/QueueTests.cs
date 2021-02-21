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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(2);
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
            result.DebugInfo.Errors.Count.ShouldBe(2);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(2);
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
            result.DebugInfo.Errors.Count.ShouldBe(2);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty(string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("Queue1", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("Queue1", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty(string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("Queue1", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_empty_queue_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty(string.Empty, string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }
    }
}