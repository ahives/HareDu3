namespace HareDu.Core.Configuration;

using System;

/// <summary>
/// Provides the ability to configure diagnostics, including the setup of diagnostic probes.
/// </summary>
public interface DiagnosticsConfigurator
{
    /// <summary>
    /// Configures diagnostic probes with the specified settings.
    /// </summary>
    /// <param name="configurator">Action delegate used to configure diagnostic probes.</param>
    void Probes(Action<DiagnosticProbesConfigurator> configurator);
}