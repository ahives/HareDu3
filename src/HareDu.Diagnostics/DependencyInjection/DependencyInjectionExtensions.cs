namespace HareDu.Diagnostics.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;
using Core;
using Core.Configuration;
using Core.Security;
using Formatting;
using KnowledgeBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers HareDu diagnostics services into the specified IServiceCollection instance.
    /// </summary>
    /// <param name="services">The IServiceCollection instance where services are registered.</param>
    /// <param name="settingsFile">The name of the settings file (e.g., "appsettings.json") used for configuration. Defaults to "appsettings.json" if not provided.</param>
    /// <param name="configSection">The configuration section in the settings file to bind to HareDuConfig. Defaults to "HareDuConfig" if not provided.</param>
    /// <returns>The updated IServiceCollection instance for chaining additional service registrations.</returns>
    public static IServiceCollection AddHareDuDiagnostics(
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
        services.TryAddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
        services.TryAddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.TryAddSingleton<IScanner, Scanner>();
        services.TryAddSingleton<IScannerFactory, ScannerFactory>();
        services.TryAddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
        services.TryAddSingleton<IDiagnosticReportFormatter, DiagnosticReportTextFormatter>();

        return services;
    }

    /// <summary>
    /// Registers HareDu diagnostics services into the specified IServiceCollection instance.
    /// </summary>
    /// <param name="services">The IServiceCollection instance where services are registered.</param>
    /// <param name="configurator">The configuration action to set up the HareDuConfigurator, which provides configuration for HareDu services.</param>
    /// <returns>The updated IServiceCollection instance for chaining additional service registrations.</returns>
    public static IServiceCollection AddHareDuDiagnostics(
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
        services.TryAddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>();
        services.TryAddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();
        services.TryAddSingleton<IScanner, Scanner>();
        services.TryAddSingleton<IScannerFactory, ScannerFactory>();
        services.TryAddSingleton<IScannerResultAnalyzer, ScannerResultAnalyzer>();
        services.TryAddSingleton<IDiagnosticReportFormatter, DiagnosticReportTextFormatter>();

        return services;
    }
}