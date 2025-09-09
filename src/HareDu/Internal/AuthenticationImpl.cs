namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;
using Serialization;

class AuthenticationImpl :
    BaseHareDuImpl,
    Authentication
{
    public AuthenticationImpl(HttpClient client)
        : base(client, new BrokerDeserializer())
    {
    }

    public async Task<Result<AuthenticationDetails>> GetDetails(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetRequest<AuthenticationDetails>("api/auth", RequestType.Authentication, cancellationToken).ConfigureAwait(false);
    }
}