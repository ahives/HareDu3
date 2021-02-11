namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class VirtualHostLimitsDebugExtensions
    {
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
                {
                    Console.WriteLine($"\tKey: {pair.Key}, Value: {pair.Value}");
                }
                Console.WriteLine();
            }

            return result;
        }
    }
}