namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class AuthenticationImpl :
    BaseBrokerObject,
    Authentication
{
    public AuthenticationImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<AuthenticationDetails>> GetDetails(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = "api/auth";
            
        return await GetRequest<AuthenticationDetails>(url, cancellationToken).ConfigureAwait(false);
    }
}