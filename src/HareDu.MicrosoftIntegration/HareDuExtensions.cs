namespace HareDu.MicrosoftIntegration
{
    using System;
    using Core.Configuration;
    using Diagnostics;
    using Diagnostics.KnowledgeBase;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Snapshotting;

    public static class HareDuExtensions
    {
        public static IServiceCollection AddHareDu(this IServiceCollection services, string settingsFile = "appsettings.json", string configSection = "HareDuConfig")
        {
            HareDuConfig config = new HareDuConfig();
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(settingsFile, false)
                .Build();

            configuration.Bind(configSection, config);

            services.AddSingleton(config);
            services.AddSingleton<IBrokerObjectFactory, BrokerObjectFactory>();
            services.AddSingleton<IScanner, Scanner>();
            services.AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
            services.AddSingleton<IScannerFactory, ScannerFactory>();
            services.AddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
            services.AddSingleton<ISnapshotFactory>(x => new SnapshotFactory(x.GetService<IBrokerObjectFactory>()));
            
            return services;
        }

        public static IServiceCollection AddHareDu(this IServiceCollection services, Action<HareDuConfigurator> configurator)
        {
            HareDuConfig config = new HareDuConfigProvider()
                .Configure(configurator);

            services.AddSingleton(config);
            services.AddSingleton<IBrokerObjectFactory, BrokerObjectFactory>();
            services.AddSingleton<IScanner, Scanner>();
            services.AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
            services.AddSingleton<IScannerFactory, ScannerFactory>();
            services.AddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
            services.AddSingleton<ISnapshotFactory>(x => new SnapshotFactory(x.GetService<IBrokerObjectFactory>()));
            
            return services;
        }
    }
}