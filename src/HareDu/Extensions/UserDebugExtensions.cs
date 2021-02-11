namespace HareDu.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class UserDebugExtensions
    {
        public static Task<ResultList<UserInfo>> ScreenDump(this Task<ResultList<UserInfo>> result)
        {
            var results = result
                .GetResult()
                .Select(x => x.Data);

            foreach (var item in results)
            {
                Console.WriteLine($"Username: {item.Username}");
                Console.WriteLine($"Password Hash: {item.PasswordHash}");
                Console.WriteLine($"Hashing Algorithm: {item.HashingAlgorithm}");
                Console.WriteLine($"Tags: {item.Tags}");
                Console.WriteLine("-------------------");
                Console.WriteLine();
            }

            return result;
        }
    }
}