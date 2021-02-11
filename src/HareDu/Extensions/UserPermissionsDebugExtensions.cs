namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class UserPermissionsDebugExtensions
    {
        public static Task<ResultList<UserPermissionsInfo>> ScreenDump(this Task<ResultList<UserPermissionsInfo>> result)
        {
            var results = result
                .GetResult()
                .Select(x => x.Data);

            foreach (var item in results)
            {
                Console.WriteLine($"User: {item.User}");
                Console.WriteLine($"Virtual Host: {item.VirtualHost}");
                Console.WriteLine($"Configure: {item.Configure}");
                Console.WriteLine($"Read: {item.Read}");
                Console.WriteLine($"Write: {item.Write}");
                Console.WriteLine("-------------------");
                Console.WriteLine();
            }

            return result;
        }
    }
}