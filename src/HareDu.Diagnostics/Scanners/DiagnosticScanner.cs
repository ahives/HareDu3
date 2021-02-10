namespace HareDu.Diagnostics.Scanners
{
    using System.Collections.Generic;
    using Probes;
    using Snapshotting;

    public interface DiagnosticScanner<in T>
        where T : Snapshot
    {
        string Identifier { get; }

        IReadOnlyList<ProbeResult> Scan(T snapshot);

        void FilterProbes(IReadOnlyList<DiagnosticProbe> probes);
    }
}