namespace HareDu.Shovel.DependencyInjection;

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
    /// Registers the HareDu Shovel services into the provided IServiceCollection. This method configures the required dependencies
    /// for interacting with HareDu Shovel functionality using the specified settings file and configuration section.
    /// </summary>
    /// <param name="services">The service collection into which HareDu Shovel services will be registered.</param>
    /// <param name="settingsFile">The name of the settings file containing the configuration for HareDu. Defaults to "appsettings.json".</param>
    /// <param name="configSection">The configuration section within the settings file where the HareDu configuration is located. Defaults to "HareDuConfig".</param>
    /// <returns>An IServiceCollection instance with the HareDu Shovel services registered.</returns>
    public static IServiceCollection AddHareDuShovel(
        [NotNull] this IServiceCollection services,
        [NotNull] string settingsFile = "appsettings.json",
        [NotNull] string configSection = "HareDuConfig")
    {
        var config = new HareDuConfig();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsFile, false)
            .Build();

        configuration.Bind(configSection, config);

        Throw.IfInvalid(config.Broker);
        Throw.IfInvalid(config.Diagnostics);
        Throw.IfInvalid(config.KB);

        services.TryAddSingleton(config);
        services.TryAddSingleton<IHareDuClient, HareDuClient>();
        services.TryAddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.TryAddSingleton<IHareDuDeserializer, ShovelDeserializer>();
        services.TryAddSingleton<IHareDuFactory, HareDuFactory>();

        return services;
    }

    /// <summary>
    /// Registers the HareDu Shovel services into the provided IServiceCollection. This method configures the required dependencies
    /// for interacting with the HareDu Shovel functionality.
    /// </summary>
    /// <param name="services">The service collection into which HareDu Shovel services will be registered.</param>
    /// <param name="configurator">An action to configure the HareDu options.</param>
    /// <returns>An IServiceCollection instance with the HareDu Shovel services registered.</returns>
    public static IServiceCollection AddHareDuShovel(
        [NotNull] this IServiceCollection services,
        [NotNull] Action<HareDuConfigurator> configurator)
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
        services.TryAddSingleton<IHareDuDeserializer, ShovelDeserializer>();
        services.TryAddSingleton<IHareDuFactory, HareDuFactory>();

        return services;
    }
}