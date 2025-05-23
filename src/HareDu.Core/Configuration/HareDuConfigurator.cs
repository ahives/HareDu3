namespace HareDu.Core.Configuration;

using System;

/// <summary>
/// Defines the configuration interface for setting up HareDu, including broker connections and diagnostics.
/// </summary>
public interface HareDuConfigurator
{
    /// <summary>
    /// Configures diagnostic settings for monitoring and analyzing system health.
    /// </summary>
    /// <param name="configurator">An action that provides the configuration of diagnostic probes and related settings.</param>
    void Diagnostics(Action<DiagnosticsConfigurator> configurator);

    /// <summary>
    /// Configures RabbitMQ broker connection settings.
    /// </summary>
    /// <param name="configurator">An action that provides the broker configuration settings.</param>
    void Broker(Action<BrokerConfigurator> configurator);
}