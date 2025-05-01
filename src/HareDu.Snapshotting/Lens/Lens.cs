namespace HareDu.Snapshotting.Lens;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Security;
using Model;

/// <summary>
/// Represents a mechanism to capture snapshots of a specific system component and to track the history of these snapshots.
/// </summary>
/// <typeparam name="T">The type of the snapshot to be captured.</typeparam>
public interface Lens<T>
    where T : Snapshot
{
    /// <summary>
    /// Provides access to the historical record of snapshots for a specific type of system component.
    /// </summary>
    /// <typeparam name="T">The type of snapshot associated with the history.</typeparam>
    /// <value>
    /// Contains the collection of previous snapshot results and provides methods to manage the snapshot history,
    /// such as purging specific snapshots or clearing all results.
    /// </value>
    ISnapshotHistory<T> History { get; }

    /// <summary>
    /// Takes a snapshot of the current state of the specified system component.
    /// </summary>
    /// <param name="provider">The credentials used for authenticating with the target component.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the snapshot result.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    Task<SnapshotResult<T>> TakeSnapshot(Action<HareDuCredentialProvider> provider, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers an observer that will receive notifications of snapshot context updates.
    /// </summary>
    /// <param name="observer">The observer to register for receiving updates on snapshot contexts.</param>
    /// <returns>An instance of the current lens with the observer registered.</returns>
    Lens<T> RegisterObserver(IObserver<SnapshotContext<T>> observer);

    /// <summary>
    /// Registers multiple observers for monitoring snapshot contexts.
    /// </summary>
    /// <param name="observers">A read-only list of observers to be registered for snapshot context updates.</param>
    /// <returns>The current instance of the <see cref="Lens{T}"/> to allow for method chaining.</returns>
    Lens<T> RegisterObservers(IReadOnlyList<IObserver<SnapshotContext<T>>> observers);
}