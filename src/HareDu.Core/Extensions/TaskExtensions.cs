namespace HareDu.Core.Extensions
{
    using System.Threading;
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
            => result.IsNotNull() && !result.IsCanceled && !result.IsFaulted
                ? result.GetAwaiter().GetResult()
                : default;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        public static void RequestCanceled(this CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
                return;

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}