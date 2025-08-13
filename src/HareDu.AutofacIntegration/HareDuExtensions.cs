namespace HareDu.AutofacIntegration;

using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Core;
using Core.Configuration;
using Core.HTTP;
using Core.Security;
using Diagnostics;
using Diagnostics.KnowledgeBase;
using Microsoft.Extensions.Configuration;
using Snapshotting;

public static class HareDuExtensions
{
    /// <summary>
    /// Adds and configures the HareDu components for dependency injection using the Autofac container.
    /// </summary>
    /// <param name="builder">The Autofac container builder instance.</param>
    /// <param name="settingsFile">The path to the settings file containing HareDu configuration. Defaults to "appsettings.json".</param>
    /// <param name="configSection">The configuration section in the settings file for HareDu. Defaults to "HareDuConfig".</param>
    /// <returns>The modified Autofac container builder instance.</returns>
    public static ContainerBuilder AddHareDu(
        [NotNull] this ContainerBuilder builder,
        [NotNull] string settingsFile = "appsettings.json",
        [NotNull] string configSection = "HareDuConfig")
    {
        builder.Register(x =>
            {
                HareDuConfig config = new HareDuConfig();

                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile(settingsFile, false)
                    .Build();

                configuration.Bind(configSection, config);

                return config;
            })
            .SingleInstance();

        builder.RegisterType<HareDuCredentialBuilder>()
            .As<IHareDuCredentialBuilder>()
            .SingleInstance();

        builder.RegisterType<HareDuClient>()
            .As<IHareDuClient>()
            .SingleInstance();

        builder.RegisterType<BrokerFactory>()
            .As<IBrokerFactory>()
            .SingleInstance();

        builder.RegisterType<Scanner>()
            .As<IScanner>()
            .SingleInstance();

        builder.RegisterType<KnowledgeBaseProvider>()
            .As<IKnowledgeBaseProvider>()
            .SingleInstance();

        builder.RegisterType<ScannerFactory>()
            .As<IScannerFactory>()
            .SingleInstance();

        builder.RegisterType<ScannerResultAnalyzer>()
            .As<IScannerResultAnalyzer>()
            .SingleInstance();

        builder.RegisterType<SnapshotFactory>()
            .As<ISnapshotFactory>()
            .SingleInstance();

        return builder;
    }

    /// <summary>
    /// Adds and configures the HareDu components for dependency injection using the Autofac container.
    /// </summary>
    /// <param name="builder">The Autofac container builder instance.</param>
    /// <param name="configurator">An action to configure the HareDu settings.</param>
    /// <returns>The modified Autofac container builder instance.</returns>
    public static ContainerBuilder AddHareDu(
        [NotNull] this ContainerBuilder builder,
        [NotNull] Action<HareDuConfigurator> configurator)
    {
        builder.Register(x =>
            {
                HareDuConfig config = configurator is null
                    ? ConfigCache.Default
                    : new HareDuConfigProvider()
                        .Configure(configurator);

                return config;
            })
            .SingleInstance();

        builder.RegisterType<HareDuCredentialBuilder>()
            .As<IHareDuCredentialBuilder>()
            .SingleInstance();

        builder.RegisterType<HareDuClient>()
            .As<IHareDuClient>()
            .SingleInstance();

        builder.RegisterType<BrokerFactory>()
            .As<IBrokerFactory>()
            .SingleInstance();

        builder.RegisterType<Scanner>()
            .As<IScanner>()
            .SingleInstance();

        builder.RegisterType<KnowledgeBaseProvider>()
            .As<IKnowledgeBaseProvider>()
            .SingleInstance();

        builder.RegisterType<ScannerFactory>()
            .As<IScannerFactory>()
            .SingleInstance();

        builder.RegisterType<ScannerResultAnalyzer>()
            .As<IScannerResultAnalyzer>()
            .SingleInstance();

        builder.RegisterType<SnapshotFactory>()
            .As<ISnapshotFactory>()
            .SingleInstance();

        return builder;
    }
}