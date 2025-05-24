namespace HareDu.Diagnostics;

using System;
using System.Collections.Generic;
using Probes;
using Scanners;
using HareDu.Snapshotting.Model;

/// <summary>
/// Represents a factory for managing and providing access to diagnostic scanners and probes.
/// </summary>
public interface IScannerFactory
{
    /// <summary>
    /// A collection of diagnostic probes mapped by their unique identifiers, enabling lookup and management of probes
    /// for analyzing system snapshots and returning evaluation results.
    /// </summary>
    IReadOnlyDictionary<string, DiagnosticProbe> Probes { get; }

    /// <summary>
    /// Collection of diagnostic scanners mapped by their unique identifiers.
    /// </summary>
    IReadOnlyDictionary<string, object> Scanners { get; }

    /// <summary>
    /// Attempts to retrieve a diagnostic scanner for the specified type of snapshot.
    /// </summary>
    /// <typeparam name="T">The type of snapshot associated with the diagnostic scanner. Must implement the <see cref="Snapshot"/> interface.</typeparam>
    /// <param name="scanner">When this method returns, contains the diagnostic scanner for the specified snapshot type if the operation is successful; otherwise, <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if a diagnostic scanner for the specified snapshot type was successfully retrieved; otherwise, <c>false</c>.
    /// </returns>
    bool TryGet<T>(out DiagnosticScanner<T> scanner)
        where T : Snapshot;

    /// <summary>
    /// Registers a list of observers to receive updates about probe context notifications.
    /// </summary>
    /// <param name="observers">The collection of observers to register for probe context updates.</param>
    void RegisterObservers(IReadOnlyList<IObserver<ProbeContext>> observers);

    /// <summary>
    /// Registers a single observer to the diagnostic scanner to receive notifications when a snapshot is taken.
    /// </summary>
    /// <param name="observer">The observer to be registered.</param>
    void RegisterObserver(IObserver<ProbeContext> observer);

    /// <summary>
    /// Attempts to register a diagnostic probe within the current diagnostic system.
    /// </summary>
    /// <typeparam name="T">The type of diagnostic probe being registered. Must implement the <see cref="DiagnosticProbe"/> interface.</typeparam>
    /// <param name="probe">The diagnostic probe instance to register.</param>
    /// <returns>
    /// <c>true</c> if the diagnostic probe was successfully registered; otherwise, <c>false</c>.
    /// </returns>
    bool TryRegisterProbe<T>(T probe)
        where T : DiagnosticProbe;

    /// <summary>
    /// Attempts to register a diagnostic scanner for the specified type of snapshot.
    /// </summary>
    /// <typeparam name="T">The type of snapshot associated with the diagnostic scanner. Must implement the <see cref="Snapshot"/> interface.</typeparam>
    /// <param name="scanner">The diagnostic scanner to be registered for the specified snapshot type.</param>
    /// <returns>
    /// <c>true</c> if the diagnostic scanner was successfully registered; otherwise, <c>false</c>.
    /// </returns>
    bool TryRegisterScanner<T>(DiagnosticScanner<T> scanner)
        where T : Snapshot;

    /// <summary>
    /// Attempts to register all diagnostic probes discovered in the assembly.
    /// </summary>
    /// <returns>
    /// <c>true</c> if all diagnostic probes were successfully registered; otherwise, <c>false</c>.
    /// </returns>
    bool TryRegisterAllProbes();

    /// <summary>
    /// Attempts to register all diagnostic scanners available in the current assembly
    /// that meet the necessary interface and base type requirements.
    /// </summary>
    /// <returns>
    /// <c>true</c> if all applicable diagnostic scanners were successfully registered; otherwise, <c>false</c>.
    /// </returns>
    bool TryRegisterAllScanners();
}