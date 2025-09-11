namespace HareDu.Tests;

using System;
using System.Threading.Tasks;
using Core;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class QueueTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_queues1()
    {
        var result = await GetContainerBuilder("TestData/QueueInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.TotalMessageGets, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageGetDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats.TotalMessagesAcknowledged, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessagesAcknowledgedDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesAcknowledgedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessagesDelivered, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessagesPublished, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesPublishedDetails?.Value, Is.EqualTo(1000.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessagesRedelivered, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessageDeliveryGets, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].Consumers, Is.EqualTo(1));
            Assert.That(result.Data[5].Durable, Is.True);
            Assert.That(result.Data[5].Exclusive, Is.False);
            Assert.That(result.Data[5].AutoDelete, Is.False);
            Assert.That(result.Data[5].Memory, Is.EqualTo(17628));
            Assert.That(result.Data[5].MessageBytesPersisted, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageBytesInRAM, Is.EqualTo(100));
            Assert.That(result.Data[5].MessageBytesPagedOut, Is.EqualTo(10));
            Assert.That(result.Data[5].TotalBytesOfAllMessages, Is.EqualTo(10000));
            Assert.That(result.Data[5].UnacknowledgedMessages, Is.EqualTo(30));
            Assert.That(result.Data[5].ReadyMessages, Is.EqualTo(50));
            Assert.That(result.Data[5].MessagesInRAM, Is.EqualTo(50));
            Assert.That(result.Data[5].TotalMessages, Is.EqualTo(6700));
            Assert.That(result.Data[5].UnacknowledgedMessagesInRAM, Is.EqualTo(30000));
            Assert.That(result.Data[5].TotalReductions, Is.EqualTo(77349645));
            Assert.That(result.Data[5].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[5].BackingQueueStatus, Is.Not.Null);
            Assert.That(result.Data[5].BackingQueueStatus.Q1, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Q2, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Q3, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Q4, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Mode, Is.EqualTo(BackingQueueMode.Default));
            Assert.That(result.Data[5].State, Is.EqualTo(QueueState.Running));
            Assert.That(result.Data[5].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].UnacknowledgedMessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].ReadyMessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].Name, Is.EqualTo("consumer_queue"));
            Assert.That(result.Data[5].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[5].IdleSince, Is.EqualTo(DateTimeOffset.Parse("2019-11-09 11:57:45")));
            Assert.That(result.Data[5].VirtualHost, Is.EqualTo("HareDu"));
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_queues2()
    {
        var result = await GetContainerBuilder("TestData/QueueInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetAllQueues(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.TotalMessageGets, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessageGetDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageGetDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats.TotalMessagesAcknowledged, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessagesAcknowledgedDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesAcknowledgedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessagesDelivered, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessagesPublished, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessagesPublishedDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesPublishedDetails?.Value, Is.EqualTo(1000.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessagesRedelivered, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessagesRedeliveredDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesRedeliveredDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessageDeliveryGets, Is.EqualTo(50000));
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryGetDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageDeliveryGetDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessageDeliveredWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessagesDeliveredWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageStats?.TotalMessageGetsWithoutAck, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageStats?.MessageGetsWithoutAckDetails, Is.Not.Null);
            Assert.That(result.Data[5].MessageStats?.MessageGetsWithoutAckDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].Consumers, Is.EqualTo(1));
            Assert.That(result.Data[5].Durable, Is.True);
            Assert.That(result.Data[5].Exclusive, Is.False);
            Assert.That(result.Data[5].AutoDelete, Is.False);
            Assert.That(result.Data[5].Memory, Is.EqualTo(17628));
            Assert.That(result.Data[5].MessageBytesPersisted, Is.EqualTo(0));
            Assert.That(result.Data[5].MessageBytesInRAM, Is.EqualTo(100));
            Assert.That(result.Data[5].MessageBytesPagedOut, Is.EqualTo(10));
            Assert.That(result.Data[5].TotalBytesOfAllMessages, Is.EqualTo(10000));
            Assert.That(result.Data[5].UnacknowledgedMessages, Is.EqualTo(30));
            Assert.That(result.Data[5].ReadyMessages, Is.EqualTo(50));
            Assert.That(result.Data[5].MessagesInRAM, Is.EqualTo(50));
            Assert.That(result.Data[5].TotalMessages, Is.EqualTo(6700));
            Assert.That(result.Data[5].UnacknowledgedMessagesInRAM, Is.EqualTo(30000));
            Assert.That(result.Data[5].TotalReductions, Is.EqualTo(77349645));
            Assert.That(result.Data[5].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[5].BackingQueueStatus, Is.Not.Null);
            Assert.That(result.Data[5].BackingQueueStatus.Q1, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Q2, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Q3, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Q4, Is.EqualTo(0));
            Assert.That(result.Data[5].BackingQueueStatus.Mode, Is.EqualTo(BackingQueueMode.Default));
            Assert.That(result.Data[5].State, Is.EqualTo(QueueState.Running));
            Assert.That(result.Data[5].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].UnacknowledgedMessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].ReadyMessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].MessageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[5].Name, Is.EqualTo("consumer_queue"));
            Assert.That(result.Data[5].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[5].IdleSince, Is.EqualTo(DateTimeOffset.Parse("2019-11-09 11:57:45")));
            Assert.That(result.Data[5].VirtualHost, Is.EqualTo("HareDu"));
        });
    }

    [Test]
    public async Task Verify_can_create_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue_on_error()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_can_create_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), "TestQueue31", "HareDu", "Node1", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu", "Node1", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu", "Node1", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), "TestQueue31", string.Empty, "Node1", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), string.Empty,"HareDu", "Node1", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, "Node1", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, "Node1", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("1000"));
            Assert.That(request.Arguments["x-message-ttl"].ToString(), Is.EqualTo("2000"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_can_override_arguments()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            QueueRequest request = BrokerDeserializer.Instance.ToObject<QueueRequest>(result.DebugInfo.Request);

            Assert.That(request.Arguments["x-expires"].ToString(), Is.EqualTo("980"));
            Assert.That(request.Durable, Is.True);
            Assert.That(request.AutoDelete, Is.True);
            Assert.That(request.Node, Is.EqualTo("Node1"));
        });
    }

    [Test]
    public async Task Verify_can_delete_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete("Queue1", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Url, Is.EqualTo("api/queues/HareDu/Queue1?if-unused=true"));
        });
    }

    [Test]
    public async Task Verify_can_delete_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Url, Is.EqualTo("api/queues/HareDu/Queue1?if-unused=true"));
        });
    }

    [Test]
    public async Task Verify_can_delete_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
                x.WhenEmpty();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Url, Is.EqualTo("api/queues/HareDu/Queue1?if-unused=true&if-empty=true"));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu", x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete("Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue13()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue14()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.WhenHasNoConsumers();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_can_empty_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Empty("Queue1", "HareDu");
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_empty_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .EmptyQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", "HareDu");
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_cannot_empty_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Empty(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .EmptyQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Empty("Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .EmptyQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Empty(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_empty_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .EmptyQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_can_sync_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Sync("Queue1", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            QueueSyncRequest request = BrokerDeserializer.Instance.ToObject<QueueSyncRequest>(result.DebugInfo.Request);

            Assert.That(request.Action, Is.EqualTo(QueueSyncAction.Sync));
        });
    }

    [Test]
    public async Task Verify_can_sync_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .SyncQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            QueueSyncRequest request = BrokerDeserializer.Instance.ToObject<QueueSyncRequest>(result.DebugInfo.Request);

            Assert.That(request.Action, Is.EqualTo(QueueSyncAction.Sync));
        });
    }

    [Test]
    public async Task Verify_can_sync_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CancelQueueSync(x => x.UsingCredentials("guest", "guest"), "Queue1", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            QueueSyncRequest request = BrokerDeserializer.Instance.ToObject<QueueSyncRequest>(result.DebugInfo.Request);

            Assert.That(request.Action, Is.EqualTo(QueueSyncAction.CancelSync));
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Sync(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .SyncQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Sync("Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .SyncQueue(x => x.UsingCredentials("guest", "guest"), "Queue1", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Sync(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_sync_queue6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .SyncQueue(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_can_create_exchange_binding_without_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        string vhost = "vhost";
        string queue = "queue";
        var result1 = services
            .GetService<IHareDuFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Create(vhost);
        var result2 = await services.GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create(queue, vhost, "rabbit@6089ab1a7b81", x =>
            {
                x.IsDurable();
            });
        string exchange = "exchange";
        var result3 = await services.GetService<IHareDuFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create(exchange, vhost);
        var result = await services.GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
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
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Unbind(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }
}