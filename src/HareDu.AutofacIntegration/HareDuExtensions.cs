namespace HareDu.AutofacIntegration;

using System;
using Autofac;
using Core.Configuration;
using Diagnostics;
using Diagnostics.KnowledgeBase;
using Microsoft.Extensions.Configuration;
using Snapshotting;

public static class HareDuExtensions
{
    /// <summary>
    /// Registers all the necessary components to use the low level HareDu Broker, Diagnostic, and Snapshotting APIs.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="settingsFile">The full path of where the configuration settings file is located.</param>
    /// <param name="configSection">The section found within the configuration file.</param>
    /// <returns></returns>
    public static ContainerBuilder AddHareDu(this ContainerBuilder builder, string settingsFile = "appsettings.json", string configSection = "HareDuConfig")
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

        builder.Register(x => new SnapshotFactory(x.Resolve<IBrokerFactory>()))
            .As<ISnapshotFactory>()
            .SingleInstance();

        return builder;
    }

    /// <summary>
    /// Registers all the necessary components to use the low level HareDu Broker, Diagnostic, and Snapshotting APIs.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configurator">Configure Broker and Diagnostic APIs programmatically.</param>
    /// <returns></returns>
    public static ContainerBuilder AddHareDu(this ContainerBuilder builder, Action<HareDuConfigurator> configurator)
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

        builder.Register(x => new SnapshotFactory(x.Resolve<IBrokerFactory>()))
            .As<ISnapshotFactory>()
            .SingleInstance();

        return builder;
    }
}