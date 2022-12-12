namespace HareDu.Core.Extensions;

using System.Threading.Tasks;

public static class TaskExtensions
{
    /// <summary>
    /// Unwrap <see cref="Task"/> and return T.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetResult<T>(this Task<T> result)
        => result is not null && !result.IsCanceled && !result.IsFaulted
            ? result.GetAwaiter().GetResult()
            : default;
}