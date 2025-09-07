namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class BindingExtensions
{
    /// <summary>
    /// Retrieves all bindings from the broker asynchronously.
    /// </summary>
    /// <param name="factory">The factory interface for interacting with the broker APIs.</param>
    /// <param name="credentials">An action to configure credentials for the broker connection.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the results of the bindings information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<BindingInfo>> GetAllBindings(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Binding>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }
}

public static class ResultExtensions
{
    public static Result<TValue> Maybe<T, TValue>(this Result<T> source, Func<T, Result<TValue>> projection)
    {
        Guard.IsNotNull(source);

        return !source.HasFaulted && source.HasData
            ? projection(source.Data) ?? ResultHelper.Missing<TValue>()
            : ResultHelper.Missing<TValue>();
    }
}