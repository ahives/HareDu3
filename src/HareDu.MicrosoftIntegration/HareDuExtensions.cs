namespace HareDu.MicrosoftIntegration;

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Core.Configuration;
using Diagnostics;
using Diagnostics.KnowledgeBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snapshotting;

public static class HareDuExtensions
{
    /// <summary>
    /// Registers all the necessary components to use the low level HareDu Broker, Diagnostic, and Snapshotting APIs.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="settingsFile">The full path of where the configuration settings file is located.</param>
    /// <param name="configSection">The section found within the configuration file.</param>
    /// <returns></returns>
    public static IServiceCollection AddHareDu(this IServiceCollection services, string settingsFile = "appsettings.json", string configSection = "HareDuConfig")
    {
        HareDuConfig config = new HareDuConfig();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsFile, false)
            .Build();

        configuration.Bind(configSection, config);

        services.AddSingleton(config);
        services.AddHttpClient("broker", client =>
            {
                client.BaseAddress = new Uri($"{config.Broker.Url}/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestVersion = HttpVersion.Version30;

                if (config.Broker.Timeout != TimeSpan.Zero)
                    client.Timeout = config.Broker.Timeout;
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                Credentials = new NetworkCredential(config.Broker.Credentials.Username,
                    config.Broker.Credentials.Password)
            });
        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IScanner, Scanner>();
        services.AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
        services.AddSingleton<IScannerFactory, ScannerFactory>();
        services.AddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
        services.AddSingleton<ISnapshotFactory, SnapshotFactory>();

        return services;
    }

    /// <summary>
    /// Registers all the necessary components to use the low level HareDu Broker, Diagnostic, and Snapshotting APIs.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurator">Configure Broker and Diagnostic APIs programmatically.</param>
    /// <returns></returns>
    public static IServiceCollection AddHareDu(this IServiceCollection services, Action<HareDuConfigurator> configurator)
    {
        HareDuConfig config = configurator is null
            ? ConfigCache.Default
            : new HareDuConfigProvider()
                .Configure(configurator);

        services.AddSingleton(config);
        services.AddHttpClient("broker", client =>
            {
                client.BaseAddress = new Uri($"{config.Broker.Url}/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (config.Broker.Timeout != TimeSpan.Zero)
                    client.Timeout = config.Broker.Timeout;
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                Credentials = new NetworkCredential(config.Broker.Credentials.Username,
                    config.Broker.Credentials.Password)
            });
        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IScanner, Scanner>();
        services.AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
        services.AddSingleton<IScannerFactory, ScannerFactory>();
        services.AddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
        services.AddSingleton<ISnapshotFactory, SnapshotFactory>();

        return services;
    }
}