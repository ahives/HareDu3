namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Configuration;
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
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Results<BindingInfo>> GetAllBindings(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

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