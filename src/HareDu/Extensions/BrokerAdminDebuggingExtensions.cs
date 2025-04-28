namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

public static class BrokerAdminDebuggingExtensions
{
    public static Task<Results<BindingInfo>> ScreenDump(this Task<Results<BindingInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);
            
        foreach (var item in results)
        {
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Source: {item.Source}");
            Console.WriteLine($"Destination: {item.Destination}");
            Console.WriteLine($"Destination Type: {item.DestinationType}");
            Console.WriteLine($"Routing Key: {item.RoutingKey}");
            Console.WriteLine($"Properties Key: {item.PropertiesKey}");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<ConsumerInfo>> ScreenDump(this Task<Results<ConsumerInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Acknowledgement Required: {item.AcknowledgementRequired}");
            Console.WriteLine("Channel");
            Console.WriteLine($"\tName: {item.ChannelDetails?.Name}");
            Console.WriteLine($"\tConnection Name: {item.ChannelDetails?.ConnectionName}");
            Console.WriteLine($"\tNode: {item.ChannelDetails?.Node}");
            Console.WriteLine($"\tNumber: {item.ChannelDetails?.Number}");
            Console.WriteLine($"\tPeer Host: {item.ChannelDetails?.PeerHost}");
            Console.WriteLine($"\tPeer Port: {item.ChannelDetails?.PeerPort}");
            Console.WriteLine($"\tUser: {item.ChannelDetails?.User}");
            Console.WriteLine($"Consumer Tag: {item.ConsumerTag}");
            Console.WriteLine($"Exclusive: {item.Exclusive}");
            Console.WriteLine($"Prefetch Count: {item.PreFetchCount}");
            Console.WriteLine($"Name: {item.QueueConsumerDetails?.Name}");
            Console.WriteLine($"Virtual Host: {item.QueueConsumerDetails?.VirtualHost}");
                
            Console.WriteLine();
            Console.WriteLine("Arguments");
                
            foreach (var pair in item.Arguments)
                Console.WriteLine($"\tKey: {pair.Key}, Value: {pair.Value}");

            Console.WriteLine("-------------------");
        }

        return result;
    }
        
    public static Task<Results<ExchangeInfo>> ScreenDump(this Task<Results<ExchangeInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);
            
        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Auto Delete: {item.AutoDelete}");
            Console.WriteLine($"Internal: {item.Internal}");
            Console.WriteLine($"Durable: {item.Durable}");
            Console.WriteLine($"Routing Type: {item.RoutingType}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        return result;
    }

    public static Results<ExchangeInfo> ScreenDump(this Results<ExchangeInfo> result)
    {
        var results = result
            .Select(x => x.Data);
            
        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Auto Delete: {item.AutoDelete}");
            Console.WriteLine($"Internal: {item.Internal}");
            Console.WriteLine($"Durable: {item.Durable}");
            Console.WriteLine($"Routing Type: {item.RoutingType}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        return result;
    }

    public static IReadOnlyList<ExchangeInfo> ScreenDump(this IReadOnlyList<ExchangeInfo> result)
    {
        foreach (var item in result)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Auto Delete: {item.AutoDelete}");
            Console.WriteLine($"Internal: {item.Internal}");
            Console.WriteLine($"Durable: {item.Durable}");
            Console.WriteLine($"Routing Type: {item.RoutingType}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    
        return result;
    }

    public static Task<Results<GlobalParameterInfo>> ScreenDump(this Task<Results<GlobalParameterInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Value: {item.Value}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<PolicyInfo>> ScreenDump(this Task<Results<PolicyInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Applied To: {item.AppliedTo}");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Pattern: {item.Pattern}");
            Console.WriteLine($"Priority: {item.Priority}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<OperatorPolicyInfo>> ScreenDump(this Task<Results<OperatorPolicyInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Applied To: {item.AppliedTo}");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Pattern: {item.Pattern}");
            Console.WriteLine($"Priority: {item.Priority}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<ScopedParameterInfo>> ScreenDump(this Task<Results<ScopedParameterInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Component: {item.Component}");

            foreach (var pair in item.Value)
                Console.WriteLine($"\tKey: {pair.Key}, Value: {pair.Value}");

            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<TopicPermissionsInfo>> ScreenDump(this Task<Results<TopicPermissionsInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Exchange: {item.Exchange}");
            Console.WriteLine($"Read: {item.Read}");
            Console.WriteLine($"Write: {item.Write}");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<UserInfo>> ScreenDump(this Task<Results<UserInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Username: {item.Username}");
            Console.WriteLine($"Password Hash: {item.PasswordHash}");
            Console.WriteLine($"Hashing Algorithm: {item.HashingAlgorithm}");
            Console.WriteLine($"Tags: {item.Tags}");
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<UserPermissionsInfo>> ScreenDump(this Task<Results<UserPermissionsInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"User: {item.User}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Configure: {item.Configure}");
            Console.WriteLine($"Read: {item.Read}");
            Console.WriteLine($"Write: {item.Write}");
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<VirtualHostInfo>> ScreenDump(this Task<Results<VirtualHostInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"\tName: {item.Name}");
            Console.WriteLine($"\t\tDescription: {item.Description}");
            Console.WriteLine($"\tTracing: {item.Tracing}");
            Console.WriteLine($"\tDefault Queue Type: {item.DefaultQueueType}");
            Console.WriteLine($"\tTags: {string.Join(',', item.Tags)}");
            Console.WriteLine($"\tTracing: {item.Tracing}");
            Console.WriteLine("\tMessages");
            Console.WriteLine($"\t\tMessages: {item.TotalMessages} (total), {item.MessagesDetails?.Value} (rate)");
            Console.WriteLine($"\t\tReady: {item.ReadyMessages} (total), {item.ReadyMessagesDetails?.Value} (rate)");
            Console.WriteLine($"\t\tUnacknowledged: {item.UnacknowledgedMessages} (total), {item.UnacknowledgedMessagesDetails?.Value} (rate)");

            foreach (var pair in item.ClusterState)
                Console.WriteLine($"\t\tKey: {pair.Key}, Value: {pair.Value}");
                
            Console.WriteLine("\t-------------------");
            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<VirtualHostLimitsInfo>> ScreenDump(this Task<Results<VirtualHostLimitsInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
                
            Console.WriteLine("Parameters");
            foreach (var pair in item.Limits)
                Console.WriteLine($"\tKey: {pair.Key}, Value: {pair.Value}");

            Console.WriteLine();
        }

        return result;
    }
        
    public static Task<Results<ShovelInfo>> ScreenDump(this Task<Results<ShovelInfo>> result)
    {
        var results = result.Result.Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Node: {item.Node}");
            Console.WriteLine($"Timestamp: {item.Timestamp}");
            Console.WriteLine($"Type: {item.Type}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"State: {item.State}");
            Console.WriteLine();
        }

        return result;
    }

    public static Task<Results<QueueInfo>> ScreenDump(this Task<Results<QueueInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Auto Delete: {item.AutoDelete}");
            Console.WriteLine($"Consumers: {item.Consumers}");
            Console.WriteLine($"Durable: {item.Durable}");
            Console.WriteLine($"Exclusive: {item.Exclusive}");
            Console.WriteLine($"Memory: {item.Memory}");
            Console.WriteLine($"Node: {item.Node}");
            Console.WriteLine($"Policy: {item.Policy}");
            Console.WriteLine($"State: {item.State}");
            Console.WriteLine($"Typr: {item.Type}");
            Console.WriteLine($"Consumer Utilization: {item.ConsumerUtilization}");
            Console.WriteLine();
            Console.WriteLine("Garbage Collection");
            Console.WriteLine($"\tFull Sweep After: {item.GC?.FullSweepAfter}");
            Console.WriteLine($"\tMaximum Heap Size: {item.GC?.MaximumHeapSize}");
            Console.WriteLine($"\tMinimum Heap Size: {item.GC?.MinimumHeapSize}");
            Console.WriteLine($"\tMinor Garbage Collection: {item.GC?.MinorGarbageCollection}");
            Console.WriteLine($"\tMinimum Binary Virtual Heap Size: {item.GC?.MinimumBinaryVirtualHeapSize}");
            Console.WriteLine("\t-------------------");
            Console.WriteLine();
            Console.WriteLine($"Idle Since: {item.IdleSince}");
            Console.WriteLine($"Message Rate: {item.MessageDetails?.Value}");
            Console.WriteLine($"Messages Persisted: {item.MessagesPersisted}");
            Console.WriteLine();
            Console.WriteLine("Message Stats");
            Console.WriteLine($"\tMessage Delivery Details: {item.MessageStats?.MessageDeliveryDetails?.Value}");
            Console.WriteLine($"\tMessage Get Details: {item.MessageStats?.MessageGetDetails?.Value}");
            Console.WriteLine($"\tMessages Acknowledged Details: {item.MessageStats?.MessagesAcknowledgedDetails?.Value}");
            Console.WriteLine($"\tMessages Published Details: {item.MessageStats?.MessagesPublishedDetails?.Value}");
            Console.WriteLine($"\tMessages Redelivered Details: {item.MessageStats?.MessagesRedeliveredDetails?.Value}");
            Console.WriteLine($"\tMessage Delivery Get Details: {item.MessageStats?.MessageDeliveryGetDetails?.Value}");
            Console.WriteLine($"\tMessage Gets Without Ack Details: {item.MessageStats?.MessageGetsWithoutAckDetails?.Value}");
            Console.WriteLine($"\tMessages Delivered Without Ack Details: {item.MessageStats?.MessagesDeliveredWithoutAckDetails?.Value}");
            Console.WriteLine($"\tTotal Message Gets: {item.MessageStats?.TotalMessageGets}");
            Console.WriteLine($"\tTotal Messages Acknowledged: {item.MessageStats?.TotalMessagesAcknowledged}");
            Console.WriteLine($"\tTotal Messages Delivered: {item.MessageStats?.TotalMessagesDelivered}");
            Console.WriteLine($"\tTotal Messages Published: {item.MessageStats?.TotalMessagesPublished}");
            Console.WriteLine($"\tTotal Messages Redelivered: {item.MessageStats?.TotalMessagesRedelivered}");
            Console.WriteLine($"\tTotal Message Delivery Gets: {item.MessageStats?.TotalMessageDeliveryGets}");
            Console.WriteLine($"\tTotal Message Delivered Without Ack: {item.MessageStats?.TotalMessageDeliveredWithoutAck}");
            Console.WriteLine($"\tTotal Message Gets Without Ack: {item.MessageStats?.TotalMessageGetsWithoutAck}");
            Console.WriteLine("\t-------------------");
            Console.WriteLine();
            Console.WriteLine($"Ready Messages: {item.ReadyMessages}");
            Console.WriteLine($"Reduction Rate: {item.ReductionDetails?.Value}");
            Console.WriteLine($"Total Messages: {item.TotalMessages}");
            Console.WriteLine($"Total Reductions: {item.TotalReductions}");
            Console.WriteLine($"Unacknowledged Messages: {item.UnacknowledgedMessages}");
            Console.WriteLine("Backing Queue Status");
            Console.WriteLine($"\tLength: {item.BackingQueueStatus?.Length}");
            Console.WriteLine($"\tMode: {item.BackingQueueStatus?.Mode}");
            Console.WriteLine($"\tQ1: {item.BackingQueueStatus?.Q1}");
            Console.WriteLine($"\tQ2: {item.BackingQueueStatus?.Q2}");
            Console.WriteLine($"\tQ3: {item.BackingQueueStatus?.Q3}");
            Console.WriteLine($"\tQ4: {item.BackingQueueStatus?.Q4}");
            Console.WriteLine($"\tNext Sequence Id: {item.BackingQueueStatus?.NextSequenceId}");
            Console.WriteLine($"\tAvg. Egress Rate: {item.BackingQueueStatus?.AvgEgressRate}");
            Console.WriteLine($"\tAvg. Ingress Rate: {item.BackingQueueStatus?.AvgIngressRate}");
            Console.WriteLine($"\tAvg. Acknowledgement Egress Rate: {item.BackingQueueStatus?.AvgAcknowledgementEgressRate}");
            Console.WriteLine($"\tAvg. Acknowledgement Ingress Rate: {item.BackingQueueStatus?.AvgAcknowledgementIngressRate}");
            Console.WriteLine($"\tTarget Total Messages in RAM: {item.BackingQueueStatus?.TargetTotalMessagesInRAM}");
            Console.WriteLine($"Exclusive Consumer Tag: {item.ExclusiveConsumerTag}");
            Console.WriteLine($"Head Message Timestamp: {item.HeadMessageTimestamp}");
            Console.WriteLine($"Message Bytes Persisted: {item.MessageBytesPersisted}");
            Console.WriteLine($"Messages (RAM): {item.MessagesInRAM}");
            Console.WriteLine($"Ready Message Rate: {item.ReadyMessageDetails?.Value}");
            Console.WriteLine($"Unacked Message Rate: {item.UnacknowledgedMessageDetails?.Value}");
            Console.WriteLine($"Message Bytes (RAM): {item.MessageBytesInRAM}");
            Console.WriteLine($"Message Bytes Paged Out: {item.MessageBytesPagedOut}");
            Console.WriteLine($"Total Messages Paged Out: {item.TotalMessagesPagedOut}");
            Console.WriteLine($"UnacknowledgedMessages (RAM): {item.UnacknowledgedMessagesInRAM}");
            Console.WriteLine($"Total Bytes Of All Messages: {item.TotalBytesOfAllMessages}");
            Console.WriteLine($"Messages Ready for Delivery (RAM): {item.MessagesReadyForDeliveryInRAM}");
            Console.WriteLine($"Total Bytes (Messages Delivered - Unacknowledged): {item.TotalBytesOfMessagesDeliveredButUnacknowledged}");
            Console.WriteLine($"Total Bytes (Messages Ready for Delivery): {item.TotalBytesOfMessagesReadyForDelivery}");
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        return result;
    }

    public static Task<Results<QueueDetailInfo>> ScreenDump(this Task<Results<QueueDetailInfo>> result)
    {
        var results = result
            .GetResult()
            .Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine($"Auto Delete: {item.AutoDelete}");
            Console.WriteLine($"Consumers: {item.Consumers}");
            Console.WriteLine($"Durable: {item.Durable}");
            Console.WriteLine($"Exclusive: {item.Exclusive}");
            Console.WriteLine($"Memory: {item.Memory}");
            Console.WriteLine($"Node: {item.Node}");
            Console.WriteLine($"Policy: {item.Policy}");
            Console.WriteLine($"State: {item.State}");
            Console.WriteLine($"Typr: {item.Type}");
            Console.WriteLine($"Storage Version: {item.StorageVersion}");
            Console.WriteLine($"Operating Policy: {item.OperatorPolicy}");
            Console.WriteLine($"Consumer Utilization: {item.ConsumerUtilization}");
            Console.WriteLine();
            Console.WriteLine("Garbage Collection");
            Console.WriteLine($"\tFull Sweep After: {item.GC?.FullSweepAfter}");
            Console.WriteLine($"\tMaximum Heap Size: {item.GC?.MaximumHeapSize}");
            Console.WriteLine($"\tMinimum Heap Size: {item.GC?.MinimumHeapSize}");
            Console.WriteLine($"\tMinor Garbage Collection: {item.GC?.MinorGarbageCollection}");
            Console.WriteLine($"\tMinimum Binary Virtual Heap Size: {item.GC?.MinimumBinaryVirtualHeapSize}");
            Console.WriteLine("\t-------------------");
            Console.WriteLine();
            Console.WriteLine($"Idle Since: {item.IdleSince}");
            Console.WriteLine($"Message Rate: {item.MessageDetails?.Value}");
            Console.WriteLine($"Messages Persisted: {item.MessagesPersisted}");
            Console.WriteLine();
            Console.WriteLine("Message Stats");
            Console.WriteLine($"\tMessage Delivery Details: {item.MessageStats?.MessageDeliveryDetails?.Value}");
            Console.WriteLine($"\tMessage Get Details: {item.MessageStats?.MessageGetDetails?.Value}");
            Console.WriteLine($"\tMessages Acknowledged Details: {item.MessageStats?.MessagesAcknowledgedDetails?.Value}");
            Console.WriteLine($"\tMessages Published Details: {item.MessageStats?.MessagesPublishedDetails?.Value}");
            Console.WriteLine($"\tMessages Redelivered Details: {item.MessageStats?.MessagesRedeliveredDetails?.Value}");
            Console.WriteLine($"\tMessage Delivery Get Details: {item.MessageStats?.MessageDeliveryGetDetails?.Value}");
            Console.WriteLine($"\tMessage Gets Without Ack Details: {item.MessageStats?.MessageGetsWithoutAckDetails?.Value}");
            Console.WriteLine($"\tMessages Delivered Without Ack Details: {item.MessageStats?.MessagesDeliveredWithoutAckDetails?.Value}");
            Console.WriteLine($"\tTotal Message Gets: {item.MessageStats?.TotalMessageGets}");
            Console.WriteLine($"\tTotal Messages Acknowledged: {item.MessageStats?.TotalMessagesAcknowledged}");
            Console.WriteLine($"\tTotal Messages Delivered: {item.MessageStats?.TotalMessagesDelivered}");
            Console.WriteLine($"\tTotal Messages Published: {item.MessageStats?.TotalMessagesPublished}");
            Console.WriteLine($"\tTotal Messages Redelivered: {item.MessageStats?.TotalMessagesRedelivered}");
            Console.WriteLine($"\tTotal Message Delivery Gets: {item.MessageStats?.TotalMessageDeliveryGets}");
            Console.WriteLine($"\tTotal Message Delivered Without Ack: {item.MessageStats?.TotalMessageDeliveredWithoutAck}");
            Console.WriteLine($"\tTotal Message Gets Without Ack: {item.MessageStats?.TotalMessageGetsWithoutAck}");
            Console.WriteLine("\t-------------------");
            Console.WriteLine();
            Console.WriteLine($"Ready Messages: {item.ReadyMessages}");
            Console.WriteLine($"Reduction Rate: {item.ReductionDetails?.Value}");
            Console.WriteLine($"Total Messages: {item.TotalMessages}");
            Console.WriteLine($"Total Reductions: {item.TotalReductions}");
            Console.WriteLine($"Unacknowledged Messages: {item.UnacknowledgedMessages}");
            Console.WriteLine("Backing Queue Status");
            Console.WriteLine($"\tLength: {item.BackingQueueStatus?.Length}");
            Console.WriteLine($"\tMode: {item.BackingQueueStatus?.Mode}");
            Console.WriteLine($"\tQ1: {item.BackingQueueStatus?.Q1}");
            Console.WriteLine($"\tQ2: {item.BackingQueueStatus?.Q2}");
            Console.WriteLine($"\tQ3: {item.BackingQueueStatus?.Q3}");
            Console.WriteLine($"\tQ4: {item.BackingQueueStatus?.Q4}");
            Console.WriteLine($"\tNext Sequence Id: {item.BackingQueueStatus?.NextSequenceId}");
            Console.WriteLine($"\tAvg. Egress Rate: {item.BackingQueueStatus?.AvgEgressRate}");
            Console.WriteLine($"\tAvg. Ingress Rate: {item.BackingQueueStatus?.AvgIngressRate}");
            Console.WriteLine($"\tAvg. Acknowledgement Egress Rate: {item.BackingQueueStatus?.AvgAcknowledgementEgressRate}");
            Console.WriteLine($"\tAvg. Acknowledgement Ingress Rate: {item.BackingQueueStatus?.AvgAcknowledgementIngressRate}");
            Console.WriteLine($"\tTarget Total Messages in RAM: {item.BackingQueueStatus?.TargetTotalMessagesInRAM}");
            Console.WriteLine($"Exclusive Consumer Tag: {item.ExclusiveConsumerTag}");
            Console.WriteLine($"Head Message Timestamp: {item.HeadMessageTimestamp}");
            Console.WriteLine($"Message Bytes Persisted: {item.MessageBytesPersisted}");
            Console.WriteLine($"Messages (RAM): {item.MessagesInRAM}");
            Console.WriteLine($"Ready Message Rate: {item.ReadyMessageDetails?.Value}");
            Console.WriteLine($"Unacked Message Rate: {item.UnacknowledgedMessageDetails?.Value}");
            Console.WriteLine($"Message Bytes (RAM): {item.MessageBytesInRAM}");
            Console.WriteLine($"Message Bytes Paged Out: {item.MessageBytesPagedOut}");
            Console.WriteLine($"Total Messages Paged Out: {item.TotalMessagesPagedOut}");
            Console.WriteLine($"UnacknowledgedMessages (RAM): {item.UnacknowledgedMessagesInRAM}");
            Console.WriteLine($"Total Bytes Of All Messages: {item.TotalBytesOfAllMessages}");
            Console.WriteLine($"Messages Ready for Delivery (RAM): {item.MessagesReadyForDeliveryInRAM}");
            Console.WriteLine($"Total Bytes (Messages Delivered - Unacknowledged): {item.TotalBytesOfMessagesDeliveredButUnacknowledged}");
            Console.WriteLine($"Total Bytes (Messages Ready for Delivery): {item.TotalBytesOfMessagesReadyForDelivery}");
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        return result;
    }
}