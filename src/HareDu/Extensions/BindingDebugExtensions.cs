namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class BindingDebugExtensions
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
    }
}