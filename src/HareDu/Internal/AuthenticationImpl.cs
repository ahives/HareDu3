namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class AuthenticationImpl :
    BaseBrokerImpl,
    Authentication
{
    public AuthenticationImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<AuthenticationDetails>> GetDetails(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetRequest<AuthenticationDetails>("api/auth", cancellationToken).ConfigureAwait(false);
    }
}