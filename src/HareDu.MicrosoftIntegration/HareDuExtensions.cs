namespace HareDu.MicrosoftIntegration;

using System;
using System.Diagnostics.CodeAnalysis;
using Core;
using Core.Configuration;
using Core.HTTP;
using Core.Security;
using Diagnostics;
using Diagnostics.KnowledgeBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snapshotting;

public static class HareDuExtensions
{
    /// <summary>
    /// Registers and configures all necessary components to use the Broker, Diagnostic, and Snapshotting APIs.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the HareDu services will be added.</param>
    /// <param name="settingsFile">The name of the configuration file containing HareDu settings. The default is "appsettings.json".</param>
    /// <param name="configSection">The name of the configuration section in the settings file. The default is "HareDuConfig".</param>
    /// <returns>The updated IServiceCollection with HareDu services registered.</returns>
    public static IServiceCollection AddHareDu(
        [NotNull] this IServiceCollection services,
        [NotNull] string settingsFile = "appsettings.json",
        [NotNull] string configSection = "HareDuConfig")
    {
        var config = new HareDuConfig();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsFile, false)
            .Build();

        configuration.Bind(configSection, config);

        services.AddSingleton(config);
        services.AddSingleton<IHareDuClient, HareDuClient>();
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IScanner, Scanner>();
        services.AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
        services.AddSingleton<IScannerFactory, ScannerFactory>();
        services.AddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
        services.AddSingleton<ISnapshotFactory, SnapshotFactory>();

        return services;
    }

    /// <summary>
    /// Registers and configures all necessary components to use the Broker, Diagnostic, and Snapshotting APIs.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the HareDu services will be added.</param>
    /// <param name="configurator">An action to configure the HareDu settings.</param>
    /// <returns>The IServiceCollection with HareDu services registered.</returns>
    public static IServiceCollection AddHareDu(
        [NotNull] this IServiceCollection services,
        [NotNull] Action<HareDuConfigurator> configurator)
    {
        HareDuConfig config = configurator is null
            ? ConfigCache.Default
            : new HareDuConfigProvider()
                .Configure(configurator);

        services.AddSingleton(config);
        services.AddSingleton<IHareDuClient, HareDuClient>();
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IScanner, Scanner>();
        services.AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
        services.AddSingleton<IScannerFactory, ScannerFactory>();
        services.AddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
        services.AddSingleton<ISnapshotFactory, SnapshotFactory>();

        return services;
    }
}