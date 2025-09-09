namespace HareDu.Shovel.DependencyInjection;

using System.Diagnostics.CodeAnalysis;
using Core;
using Core.Configuration;
using Core.HTTP;
using Core.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers the HareDu Shovel services into the provided IServiceCollection. This method configures the required dependencies
    /// for interacting with the HareDu Shovel functionality.
    /// </summary>
    /// <param name="services">The service collection into which the HareDu Shovel services will be registered.</param>
    /// <param name="settingsFile">The configuration settings file (e.g., appsettings.json) containing HareDu configurations.</param>
    /// <returns>An IServiceCollection instance with the HareDu Shovel services registered.</returns>
    public static IServiceCollection AddHareDuShovel([NotNull] this IServiceCollection services, [NotNull] string settingsFile = "appsettings.json")
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
    public static IServiceCollection AddHareDuShovel([NotNull] this IServiceCollection services, [NotNull] Action<HareDuConfigurator> configurator)
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
        services.TryAddSingleton<IHareDuFactory, HareDuFactory>();

        return services;
    }
}