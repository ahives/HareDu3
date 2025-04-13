namespace HareDu.Tests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class QueueTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_queues1()
    {
        var result = await GetContainerBuilder("TestData/QueueInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data[5].MessageStats);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessageGets);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageGetDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageGetDetails.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats.TotalMessagesAcknowledged);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesAcknowledgedDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats?.TotalMessagesDelivered);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageDeliveryDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageDeliveryDetails?.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats?.TotalMessagesPublished);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesPublishedDetails);
            Assert.AreEqual(1000.0M, result.Data[5].MessageStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessagesRedelivered);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessagesRedeliveredDetails?.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats?.TotalMessageDeliveryGets);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageDeliveryGetDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessageDeliveredWithoutAck);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessageGetsWithoutAck);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageGetsWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(1, result.Data[5].Consumers);
            Assert.IsTrue(result.Data[5].Durable);
            Assert.IsFalse(result.Data[5].Exclusive);
            Assert.IsFalse(result.Data[5].AutoDelete);
            Assert.AreEqual(17628, result.Data[5].Memory);
            Assert.AreEqual(0, result.Data[5].MessageBytesPersisted);
            Assert.AreEqual(100, result.Data[5].MessageBytesInRAM);
            Assert.AreEqual(10, result.Data[5].MessageBytesPagedOut);
            Assert.AreEqual(10000, result.Data[5].TotalBytesOfAllMessages);
            Assert.AreEqual(30, result.Data[5].UnacknowledgedMessages);
            Assert.AreEqual(50, result.Data[5].ReadyMessages);
            Assert.AreEqual(50, result.Data[5].MessagesInRAM);
            Assert.AreEqual(6700, result.Data[5].TotalMessages);
            Assert.AreEqual(30000, result.Data[5].UnacknowledgedMessagesInRAM);
            Assert.AreEqual(77349645, result.Data[5].TotalReductions);
            Assert.IsNotNull(result.Data[5].ReductionDetails);
            Assert.IsNotNull(result.Data[5].BackingQueueStatus);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q1);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q2);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q3);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q4);
            Assert.AreEqual(BackingQueueMode.Default, result.Data[5].BackingQueueStatus.Mode);
            Assert.AreEqual(QueueState.Running, result.Data[5].State);
            Assert.AreEqual(0.0M, result.Data[5].ReductionDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[5].UnacknowledgedMessageDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[5].ReadyMessageDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[5].MessageDetails?.Value);
            Assert.AreEqual("consumer_queue", result.Data[5].Name);
            Assert.AreEqual("rabbit@localhost", result.Data[5].Node);
            Assert.AreEqual(DateTimeOffset.Parse("2019-11-09 11:57:45"), result.Data[5].IdleSince);
            Assert.AreEqual("HareDu", result.Data[5].VirtualHost);
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_queues2()
    {
        var result = await GetContainerBuilder("TestData/QueueInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllQueues();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data[5].MessageStats);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessageGets);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageGetDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageGetDetails.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats.TotalMessagesAcknowledged);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesAcknowledgedDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessagesAcknowledgedDetails?.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats?.TotalMessagesDelivered);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageDeliveryDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageDeliveryDetails?.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats?.TotalMessagesPublished);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesPublishedDetails);
            Assert.AreEqual(1000.0M, result.Data[5].MessageStats?.MessagesPublishedDetails?.Value);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessagesRedelivered);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesRedeliveredDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessagesRedeliveredDetails?.Value);
            Assert.AreEqual(50000, result.Data[5].MessageStats?.TotalMessageDeliveryGets);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageDeliveryGetDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageDeliveryGetDetails?.Value);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessageDeliveredWithoutAck);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails?.Value);
            Assert.AreEqual(0, result.Data[5].MessageStats?.TotalMessageGetsWithoutAck);
            Assert.IsNotNull(result.Data[5].MessageStats?.MessageGetsWithoutAckDetails);
            Assert.AreEqual(0.0M, result.Data[5].MessageStats?.MessageGetsWithoutAckDetails?.Value);
            Assert.AreEqual(1, result.Data[5].Consumers);
            Assert.IsTrue(result.Data[5].Durable);
            Assert.IsFalse(result.Data[5].Exclusive);
            Assert.IsFalse(result.Data[5].AutoDelete);
            Assert.AreEqual(17628, result.Data[5].Memory);
            Assert.AreEqual(0, result.Data[5].MessageBytesPersisted);
            Assert.AreEqual(100, result.Data[5].MessageBytesInRAM);
            Assert.AreEqual(10, result.Data[5].MessageBytesPagedOut);
            Assert.AreEqual(10000, result.Data[5].TotalBytesOfAllMessages);
            Assert.AreEqual(30, result.Data[5].UnacknowledgedMessages);
            Assert.AreEqual(50, result.Data[5].ReadyMessages);
            Assert.AreEqual(50, result.Data[5].MessagesInRAM);
            Assert.AreEqual(6700, result.Data[5].TotalMessages);
            Assert.AreEqual(30000, result.Data[5].UnacknowledgedMessagesInRAM);
            Assert.AreEqual(77349645, result.Data[5].TotalReductions);
            Assert.IsNotNull(result.Data[5].ReductionDetails);
            Assert.IsNotNull(result.Data[5].BackingQueueStatus);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q1);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q2);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q3);
            Assert.AreEqual(0, result.Data[5].BackingQueueStatus.Q4);
            Assert.AreEqual(BackingQueueMode.Default, result.Data[5].BackingQueueStatus.Mode);
            Assert.AreEqual(QueueState.Running, result.Data[5].State);
            Assert.AreEqual(0.0M, result.Data[5].ReductionDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[5].UnacknowledgedMessageDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[5].ReadyMessageDetails?.Value);
            Assert.AreEqual(0.0M, result.Data[5].MessageDetails?.Value);
            Assert.AreEqual("consumer_queue", result.Data[5].Name);
            Assert.AreEqual("rabbit@localhost", result.Data[5].Node);
            Assert.AreEqual(DateTimeOffset.Parse("2019-11-09 11:57:45"), result.Data[5].IdleSince);
            Assert.AreEqual("HareDu", result.Data[5].VirtualHost);
        });
    }

    [Test]
    public async Task Verify_can_create_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue_on_error()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Create("TestQueue31", "HareDu", "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(0);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
        });
    }

    [Test]
    public async Task Verify_can_create_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateQueue(string.Empty, "HareDu", "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateQueue(string.Empty, "HareDu", "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateQueue("TestQueue31", string.Empty, "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Create(string.Empty,"HareDu", "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateQueue(string.Empty,"HareDu", "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateQueue(string.Empty, string.Empty, "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();

            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateQueue(string.Empty, string.Empty, "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();

            Assert.AreEqual("1000", request.Arguments["x-expires"].ToString());
            Assert.AreEqual("2000", request.Arguments["x-message-ttl"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_can_override_arguments()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            
            QueueRequest request = result.DebugInfo.Request.ToObject<QueueRequest>();
                
            Assert.AreEqual("980", request.Arguments["x-expires"].ToString());
            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.AutoDelete);
            Assert.AreEqual("Node1", request.Node);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete("Queue1", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual("api/queues/HareDu/Queue1?if-unused=true", result.DebugInfo.URL);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue("Queue1", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual("api/queues/HareDu/Queue1?if-unused=true", result.DebugInfo.URL);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue("Queue1", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
                x.WhenEmpty();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual("api/queues/HareDu/Queue1?if-unused=true&if-empty=true", result.DebugInfo.URL);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete(string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue(string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete(string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue(string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete(string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue(string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue13()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete(string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue14()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteQueue(string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_empty_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Empty("Queue1", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_empty_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .EmptyQueue("Queue1", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_empty_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Empty(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .EmptyQueue(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Empty("Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .EmptyQueue("Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Empty(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .EmptyQueue(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_sync_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Sync("Queue1", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            QueueSyncRequest request = result.DebugInfo.Request.ToObject<QueueSyncRequest>();
                
            Assert.AreEqual(QueueSyncAction.Sync, request.Action);
        });
    }

    [Test]
    public async Task Verify_can_sync_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .SyncQueue("Queue1", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            QueueSyncRequest request = result.DebugInfo.Request.ToObject<QueueSyncRequest>();
                
            Assert.AreEqual(QueueSyncAction.Sync, request.Action);
        });
    }

    [Test]
    public async Task Verify_can_sync_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CancelQueueSync("Queue1", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            QueueSyncRequest request = result.DebugInfo.Request.ToObject<QueueSyncRequest>();
                
            Assert.AreEqual(QueueSyncAction.CancelSync, request.Action);
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Sync(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .SyncQueue(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Sync("Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .SyncQueue("Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Sync(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .SyncQueue(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_create_exchange_binding_without_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        string vhost = "vhost";
        string queue = "queue";
        var result1 = services
            .GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .Create(vhost);
        var result2 = services.GetService<IBrokerFactory>()
            .API<Queue>()
            .Create(queue, vhost, "rabbit@6089ab1a7b81", x =>
            {
                x.IsDurable();
            });
        string exchange = "exchange";
        var result3 = services
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Create(exchange, vhost);
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .BindToQueue("HareDu", exchange, x =>
            {
                x.Destination("queue");
            });

        // Assert.Multiple(() =>
        // {
        //     Assert.IsFalse(result.HasFaulted);
        //     Assert.IsNotNull(result.DebugInfo);
        //     Assert.IsNotNull(result.DebugInfo.Request);
        //
        //     BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();
        //         
        //     Assert.That(request.BindingKey, Is.Empty.Or.Null);
        //     Assert.IsNull(request.Arguments);
        // });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Unbind(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }
}