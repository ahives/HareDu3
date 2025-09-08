namespace HareDu.Snapshotting.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;
using Core;
using Core.Configuration;
using Core.HTTP;
using Core.Security;
using Core.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serialization;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers HareDu snapshotting components and related dependencies into the service collection.
    /// </summary>
    /// <param name="services">The dependency injection container where services will be registered.</param>
    /// <param name="settingsFile">
    /// The path to the configuration file containing HareDu settings. Defaults to "appsettings.json" if not provided.
    /// </param>
    /// <returns>The updated service collection, allowing method chaining.</returns>
    public static IServiceCollection AddHareDuSnapshotting([NotNull] this IServiceCollection services, [NotNull] string settingsFile = "appsettings.json")
    {
        var config = new HareDuConfig();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsFile, false)
            .Build();

        configuration.Bind("HareDu", config);

        Throw.IfInvalid(config.Broker);
        Throw.IfInvalid(config.Diagnostics);
        Throw.IfInvalid(config.KB);

        services.TryAddSingleton(config);
        services.TryAddSingleton<IHareDuClient, HareDuClient>();
        services.TryAddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.TryAddSingleton<IHareDuDeserializer, BrokerDeserializer>();
        services.TryAddSingleton<IHareDuFactory, HareDuFactory>();
        services.TryAddSingleton<ISnapshotFactory, SnapshotFactory>();

        return services;
    }

    /// <summary>
    /// Registers HareDu snapshotting components and related dependencies into the service collection.
    /// </summary>
    /// <param name="services">The dependency injection container where services will be registered.</param>
    /// <param name="configurator">
    /// A delegate to configure HareDu settings, allowing customization of broker, diagnostics, and knowledge base configurations.
    /// </param>
    /// <returns>The updated service collection, enabling method chaining.</returns>
    public static IServiceCollection AddHareDuSnapshotting([NotNull] this IServiceCollection services, [NotNull] Action<HareDuConfigurator> configurator)
    {
        HareDuConfig config = configurator is null
            ? ConfigCache.Default
            : new HareDuConfigProvider()
                .Configure(configurator);

        services.AddSingleton(config);

        Throw.IfInvalid(config.Broker);
        Throw.IfInvalid(config.Diagnostics);
        Throw.IfInvalid(config.KB);

        services.TryAddSingleton(config);
        services.TryAddSingleton<IHareDuClient, HareDuClient>();
        services.TryAddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.TryAddSingleton<IHareDuDeserializer, BrokerDeserializer>();
        services.TryAddSingleton<IHareDuFactory, HareDuFactory>();
        services.TryAddSingleton<ISnapshotFactory, SnapshotFactory>();

        return services;
    }
}