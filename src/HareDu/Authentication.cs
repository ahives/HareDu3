namespace HareDu;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents an interface for interacting with authentication configurations on the broker.
/// </summary>
public interface Authentication :
    BrokerAPI
{
    /// <summary>
    /// Retrieves the details of authentication configuration from the broker.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of authentication details.</returns>
    [return: NotNull]
    Task<Result<AuthenticationDetails>> GetDetails([NotNull] CancellationToken cancellationToken = default);
}