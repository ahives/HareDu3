namespace HareDu.Tests;

using System.IO;
using System.Net;
using Core.Configuration;
using Core.Security;
using HTTP;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

public class HareDuTesting
{
    protected ServiceCollection GetContainerBuilder(string file, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var services = new ServiceCollection();

        string data = File.ReadAllText($"{TestContext.CurrentContext.TestDirectory}/{file}");

        services.AddSingleton(ConfigCache.Default);
        services.AddSingleton<IHareDuClient>(x => new FakeHareDuClient(data, statusCode));
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();

        return services;
    }

    protected ServiceCollection GetContainerBuilder(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IHareDuClient>(x => new FakeHareDuClient(string.Empty, statusCode));
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();

        return services;
    }
}