namespace HareDu.Shovel.Testing;

using Core;
using Core.Extensions;
using Model;

public static class AdminDebuggingExtensions
{
    public static Task<Results<ShovelInfo>> ScreenDump(this Task<Results<ShovelInfo>> result)
    {
        var results = result.Result.Select(x => x.Data);

        foreach (var item in results)
        {
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Virtual Host: {item.VirtualHost}");
            Console.WriteLine();
        }

        return result;
    }

    public static Task<Result<ShovelInfo>> ScreenDump(this Task<Result<ShovelInfo>> result)
    {
        var results = result.Result.Select(x => x.Data);

        Console.WriteLine($"Name: {results.Name}");
        Console.WriteLine($"Virtual Host: {results.VirtualHost}");

        return result;
    }
}