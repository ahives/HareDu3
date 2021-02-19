namespace HareDu.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class BrokerAdminDebuggingExtensions
    {
        public static Task<ResultList<BindingInfo>> ScreenDump(this Task<ResultList<BindingInfo>> result)
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
        
        public static Task<ResultList<ConsumerInfo>> ScreenDump(this Task<ResultList<ConsumerInfo>> result)
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
        
        public static Task<ResultList<ExchangeInfo>> ScreenDump(this Task<ResultList<ExchangeInfo>> result)
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

        public static ResultList<ExchangeInfo> ScreenDump(this ResultList<ExchangeInfo> result)
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
        
        public static Task<ResultList<GlobalParameterInfo>> ScreenDump(this Task<ResultList<GlobalParameterInfo>> result)
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
        
        public static Task<ResultList<PolicyInfo>> ScreenDump(this Task<ResultList<PolicyInfo>> result)
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
        
        public static Task<ResultList<ScopedParameterInfo>> ScreenDump(this Task<ResultList<ScopedParameterInfo>> result)
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
        
        public static Task<ResultList<TopicPermissionsInfo>> ScreenDump(this Task<ResultList<TopicPermissionsInfo>> result)
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
        
        public static Task<ResultList<UserInfo>> ScreenDump(this Task<ResultList<UserInfo>> result)
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
        
        public static Task<ResultList<UserPermissionsInfo>> ScreenDump(this Task<ResultList<UserPermissionsInfo>> result)
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
        
        public static Task<ResultList<VirtualHostInfo>> ScreenDump(this Task<ResultList<VirtualHostInfo>> result)
        {
            var results = result
                .GetResult()
                .Select(x => x.Data);

            foreach (var item in results)
            {
                Console.WriteLine($"\tName: {item.Name}");
                Console.WriteLine($"\tTracing: {item.Tracing}");
                Console.WriteLine("\tMessages");
                Console.WriteLine($"\t\tMessages: {item.TotalMessages} (total), {item.MessagesDetails?.Value} (rate)");
                Console.WriteLine($"\t\tReady: {item.ReadyMessages} (total), {item.ReadyMessagesDetails?.Value} (rate)");
                Console.WriteLine($"\t\tUnacknowledged: {item.UnacknowledgedMessages} (total), {item.UnacknowledgedMessagesDetails?.Value} (rate)");
                Console.WriteLine("\tPackets");
                Console.WriteLine($"\t\tSent: {item.PacketBytesSent} (bytes), {item.PacketBytesSentDetails?.Value} (rate)");
                Console.WriteLine($"\t\tReceived: {item.PacketBytesReceived} (bytes), {item.PacketBytesReceivedDetails?.Value} (rate)");
                Console.WriteLine("\tMessage Stats");
                Console.WriteLine($"\t\tGets: {item.MessageStats?.TotalMessageGets} (total), {item.MessageStats?.MessageGetDetails?.Value} (rate)");
                Console.WriteLine($"\t\tAcknowledged: {item.MessageStats?.TotalMessagesAcknowledged} (total), {item.MessageStats?.MessagesAcknowledgedDetails?.Value} (rate)");
                Console.WriteLine($"\t\tConfirmed: {item.MessageStats?.TotalMessagesConfirmed} (total), {item.MessageStats?.MessagesConfirmedDetails?.Value} (rate)");
                Console.WriteLine($"\t\tDelivered: {item.MessageStats?.TotalMessagesDelivered} (total), {item.MessageStats?.MessageDeliveryDetails?.Value} (rate)");
                Console.WriteLine($"\t\tPublished: {item.MessageStats?.TotalMessagesPublished} (total), {item.MessageStats?.MessagesPublishedDetails?.Value} (rate)");
                Console.WriteLine($"\t\tRedelivered: {item.MessageStats?.TotalMessagesRedelivered} (total), {item.MessageStats?.MessagesRedeliveredDetails?.Value} (rate)");
                Console.WriteLine($"\t\tUnroutable: {item.MessageStats?.TotalUnroutableMessages} (total), {item.MessageStats?.UnroutableMessagesDetails?.Value} (rate)");
                Console.WriteLine($"\t\tDelivery Gets: {item.MessageStats?.TotalMessageDeliveryGets} (total), {item.MessageStats?.MessageDeliveryGetDetails?.Value} (rate)");
                Console.WriteLine($"\t\tDelivered Without Ack: {item.MessageStats?.TotalMessageDeliveredWithoutAck} (total), {item.MessageStats?.MessagesDeliveredWithoutAckDetails?.Value} (rate)");
                Console.WriteLine($"\t\tGets Without Ack: {item.MessageStats?.TotalMessageGetsWithoutAck} (total), {item.MessageStats?.MessageGetsWithoutAckDetails?.Value} (rate)");

                foreach (var pair in item.ClusterState)
                    Console.WriteLine($"\t\tKey: {pair.Key}, Value: {pair.Value}");
                
                Console.WriteLine("\t-------------------");
                Console.WriteLine();
            }

            return result;
        }
        
        public static Task<ResultList<VirtualHostLimitsInfo>> ScreenDump(this Task<ResultList<VirtualHostLimitsInfo>> result)
        {
            var results = result
                .GetResult()
                .Select(x => x.Data);

            foreach (var item in results)
            {
                Console.WriteLine($"Virtual Host: {item.VirtualHostName}");
                
                Console.WriteLine("Parameters");
                foreach (var pair in item.Limits)
                    Console.WriteLine($"\tKey: {pair.Key}, Value: {pair.Value}");

                Console.WriteLine();
            }

            return result;
        }
        
        public static Task<ResultList<ShovelInfo>> ScreenDump(this Task<ResultList<ShovelInfo>> result)
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
    }
}