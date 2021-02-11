namespace HareDu.MicrosoftIntegration
{
    using Core.Configuration;
    using Diagnostics;
    using Diagnostics.KnowledgeBase;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class HareDuExtensions
    {
        public static IServiceCollection AddHareDu(this IServiceCollection services, string settingsFile = "appsettings.json", string configSection = "HareDuConfig")
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(settingsFile, false)
                .Build();

            HareDuConfig config = new HareDuConfig();
            
            configuration.Bind(configSection, config);

            services.AddSingleton(config);
            services.AddSingleton<IBrokerObjectFactory, BrokerObjectFactory>();
            services.AddSingleton<IScanner, Scanner>();
            services.AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
            services.AddSingleton<IScannerFactory, ScannerFactory>();
            
            return services;
        }
    }
}