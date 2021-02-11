namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ScopedParameterDebugExtensions
    {
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
                {
                    Console.WriteLine($"\tKey: {pair.Key}, Value: {pair.Value}");
                }

                Console.WriteLine($"Virtual Host: {item.VirtualHost}");
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }

            return result;
        }
    }
}