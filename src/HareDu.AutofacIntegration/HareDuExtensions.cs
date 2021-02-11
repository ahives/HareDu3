namespace HareDu.AutofacIntegration
{
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
                    IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile(settingsFile, false)
                        .Build();

                    HareDuConfig config = new HareDuConfig();

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

            return builder;
        }
    }
}