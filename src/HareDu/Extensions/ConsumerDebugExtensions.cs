namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ConsumerDebugExtensions
    {
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
    }
}