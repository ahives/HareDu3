namespace HareDu.AutofacIntegration
{
    using System;
    using Autofac;
    using Core.Configuration;
    using Diagnostics;
    using Diagnostics.KnowledgeBase;
    using Microsoft.Extensions.Configuration;

    public static class HareDuExtensions
    {
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
            
            builder.RegisterType<BrokerObjectFactory>()
                .As<IBrokerObjectFactory>()
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

            return builder;
        }

        public static ContainerBuilder AddHareDu(this ContainerBuilder builder, Action<HareDuConfigurator> configurator)
        {
            builder.Register(x =>
                {
                    HareDuConfig config = new HareDuConfigProvider()
                        .Configure(configurator);

                    return config;
                })
                .SingleInstance();
            
            builder.RegisterType<BrokerObjectFactory>()
                .As<IBrokerObjectFactory>()
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

            return builder;
        }
    }
}