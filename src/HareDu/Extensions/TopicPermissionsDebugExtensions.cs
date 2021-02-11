namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class TopicPermissionsDebugExtensions
    {
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
    }
}