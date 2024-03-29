namespace HareDu.Snapshotting;

using Lens;
using Model;

public interface ISnapshotFactory
{
    /// <summary>
    /// Returns a snapshot lens if present. Otherwise, it returns a default lens if none is present.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Lens<T> Lens<T>()
        where T : Snapshot;

    /// <summary>
    /// Caches the specified snapshot lens so that every time <see cref="T"/> is accessed the proper lens will be loaded.
    /// </summary>
    /// <param name="lens"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ISnapshotFactory Register<T>(Lens<T> lens)
        where T : Snapshot;
}