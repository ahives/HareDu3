namespace HareDu.Core.Extensions;

using System.Threading.Tasks;

public static class TaskExtensions
{
    /// <summary>
    /// Retrieves the result of the given task synchronously, if the task is not null, not canceled, and not faulted.
    /// </summary>
    /// <typeparam name="T">The type of the result the task contains.</typeparam>
    /// <param name="result">The task whose result is to be retrieved.</param>
    /// <returns>The result contained within the task, or the default value of type <typeparamref name="T"/> if the task is null, canceled, or faulted.</returns>
    public static T GetResult<T>(this Task<T> result)
        => result is not null && !result.IsCanceled && !result.IsFaulted
            ? result.GetAwaiter().GetResult()
            : default;
}