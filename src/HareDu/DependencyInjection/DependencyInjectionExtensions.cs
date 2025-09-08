namespace HareDu.DependencyInjection;

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
    /// Adds HareDu services and configuration to the specified IServiceCollection instance
    /// using the provided settings file.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to which the HareDu services will be added.</param>
    /// <param name="settingsFile">The path to the settings file containing HareDu configuration. Defaults to "appsettings.json".</param>
    /// <returns>The updated IServiceCollection instance with HareDu services and configuration registered.</returns>
    public static IServiceCollection AddHareDu([NotNull] this IServiceCollection services, [NotNull] string settingsFile = "appsettings.json")
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
        services.TryAddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.TryAddSingleton<IHareDuFactory, HareDuFactory>();

        return services;
    }

    /// <summary>
    /// Registers HareDu services and configurations into the specified IServiceCollection instance using the provided configurator.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to which the HareDu services will be registered.</param>
    /// <param name="configurator">The action used to configure HareDu settings.</param>
    /// <returns>The updated IServiceCollection instance with HareDu services and configurations added.</returns>
    public static IServiceCollection AddHareDu([NotNull] this IServiceCollection services, [NotNull] Action<HareDuConfigurator> configurator)
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
        services.TryAddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.TryAddSingleton<IHareDuFactory, HareDuFactory>();

        return services;
    }
}