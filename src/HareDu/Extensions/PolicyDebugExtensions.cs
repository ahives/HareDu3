namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class PolicyDebugExtensions
    {
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
    }
}