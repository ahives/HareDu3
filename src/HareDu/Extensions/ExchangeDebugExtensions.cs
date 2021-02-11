namespace HareDu.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ExchangeDebugExtensions
    {
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
    }
}