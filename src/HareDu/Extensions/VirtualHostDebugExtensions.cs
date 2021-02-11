namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class VirtualHostDebugExtensions
    {
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
                {
                    Console.WriteLine($"\t\tKey: {pair.Key}, Value: {pair.Value}");
                }
                
                Console.WriteLine("\t-------------------");
                Console.WriteLine();
            }

            return result;
        }
    }
}