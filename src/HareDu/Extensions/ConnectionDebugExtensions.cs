namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ConnectionDebugExtensions
    {
        public static Task<ResultList<ConnectionInfo>> ScreenDump(this Task<ResultList<ConnectionInfo>> result)
        {
            var results = result
                .GetResult()
                .Select(x => x.Data);

            foreach (var item in results)
            {
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Channels: {item.Channels}");
                Console.WriteLine($"Authentication Mechanism: {item.AuthenticationMechanism}");
                Console.WriteLine($"Connected At: {item.ConnectedAt}");
                Console.WriteLine($"Connection Timeout: {item.ConnectionTimeout}");
                Console.WriteLine();
                Console.WriteLine("Garbage Collection");
                Console.WriteLine($"Full Sweep After: {item.GarbageCollectionDetails?.FullSweepAfter}");
                Console.WriteLine($"Minimum Heap Size: {item.GarbageCollectionDetails?.MinimumHeapSize}");
                Console.WriteLine($"Maximum Heap Size: {item.GarbageCollectionDetails?.MaximumHeapSize}");
                Console.WriteLine($"Minimum Binary Virtual Heap Size: {item.GarbageCollectionDetails?.MinimumBinaryVirtualHeapSize}");
                Console.WriteLine($"Minor: {item.GarbageCollectionDetails?.MinorGarbageCollection}");
                Console.WriteLine($"Host: {item.Host}");
                Console.WriteLine($"Max Channels: {item.OpenChannelsLimit}");
                Console.WriteLine($"Max Frame Size (bytes): {item.MaxFrameSizeInBytes}");
                Console.WriteLine($"Bytes Received: {item.PacketBytesReceived}");
                Console.WriteLine($"Packets Received: {item.PacketsReceived}");
                Console.WriteLine($"Peer Certificate Issuer: {item.PeerCertificateIssuer}");
                Console.WriteLine($"Peer Certificate Subject: {item.PeerCertificateSubject}");
                Console.WriteLine($"Peer Host: {item.PeerHost}");
                Console.WriteLine($"Peer Port: {item.PeerPort}");
                Console.WriteLine($"Port: {item.Port}");
                Console.WriteLine($"Octets Received (rate): {item.PacketBytesReceivedDetails?.Value}");
                Console.WriteLine($"Octets Sent (rate): {item.PacketBytesSentDetails?.Value}");
                Console.WriteLine($"Send Pending: {item.SendPending}");
                Console.WriteLine("SSL");
                Console.WriteLine($"\tIs Secure: {item.IsSsl}");
                Console.WriteLine($"\tCipher Algorithm: {item.SslCipherAlgorithm}");
                Console.WriteLine($"\tHash Function: {item.SslHashFunction}");
                Console.WriteLine($"\tKey Exchange Algorithm: {item.SslKeyExchangeAlgorithm}");
                Console.WriteLine($"\tProtocol: {item.SslProtocol}");
                Console.WriteLine($"State: {item.State}");
                Console.WriteLine($"Time Period Peer Certificate Valid: {item.TimePeriodPeerCertificateValid}");
                Console.WriteLine($"Reductions: {item.TotalReductions} (total), {item.ReductionDetails?.Value} (rate)");
                Console.WriteLine($"Type: {item.Type}");
                Console.WriteLine($"Virtual Host: {item.VirtualHost}");
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }

            return result;
        }
    }
}