namespace HareDu.Snapshotting;

using Lens;
using Model;

/// <summary>
/// Provides the ability to create and manage lenses for capturing snapshots of specific types.
/// A snapshot represents a point-in-time state of a system or process.
/// </summary>
public interface ISnapshotFactory
{
    /// <summary>
    /// Creates a lens for capturing a specified snapshot type.
    /// </summary>
    /// <typeparam name="T">The type of snapshot to be captured. Must implement <see cref="Snapshot"/>.</typeparam>
    /// <returns>Returns a lens instance that enables capturing the desired snapshot.</returns>
    Lens<T> Lens<T>()
        where T : Snapshot;

    /// <summary>
    /// Registers a lens for capturing snapshots of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of snapshot to be registered. Must implement <see cref="Snapshot"/>.</typeparam>
    /// <param name="lens">The lens instance to register for capturing snapshots.</param>
    /// <returns>Returns the current instance of <see cref="ISnapshotFactory"/> to allow method chaining.</returns>
    ISnapshotFactory Register<T>(Lens<T> lens)
        where T : Snapshot;
}