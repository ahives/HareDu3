namespace HareDu.Internal
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    class ConsumerImpl :
        BaseBrokerObject,
        Consumer
    {
        public ConsumerImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<ConsumerInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/consumers";
            
            return await GetAll<ConsumerInfo>(url, cancellationToken).ConfigureAwait(false);
        }
    }
}