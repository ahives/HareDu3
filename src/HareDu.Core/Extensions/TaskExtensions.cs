namespace HareDu.Core.Extensions;

using System.Threading.Tasks;

public static class TaskExtensions
{
    /// <summary>
    /// Retrieves the result of a task if the task is not null, not canceled, and not faulted. Otherwise, returns the default value of type T.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the task.</typeparam>
    /// <param name="result">The task from which to retrieve the result.</param>
    /// <returns>The result of the task if it has completed successfully; otherwise, the default value of type T.
    public static T GetResult<T>(this Task<T> result)
        => result is not null && !result.IsCanceled && !result.IsFaulted
            ? result.GetAwaiter().GetResult()
            : default;
}