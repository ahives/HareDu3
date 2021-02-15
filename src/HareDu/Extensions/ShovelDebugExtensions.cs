namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ShovelDebugExtensions
    {
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