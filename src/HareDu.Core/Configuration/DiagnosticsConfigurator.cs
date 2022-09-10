namespace HareDu.Core.Configuration;

using System;

public interface DiagnosticsConfigurator
{
    /// <summary>
    /// Configures the diagnostic scanner.
    /// </summary>
    /// <param name="configurator"></param>
    void Probes(Action<DiagnosticProbesConfigurator> configurator);
}