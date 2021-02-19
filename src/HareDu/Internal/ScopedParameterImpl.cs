namespace HareDu.Internal
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    class ScopedParameterImpl :
        BaseBrokerObject,
        ScopedParameter
    {
        public ScopedParameterImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/parameters";
            
            return await GetAll<ScopedParameterInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create<T>(string parameter, T value, string component, string vhost,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            ScopedParameterDefinition<T> definition =
                new()
                {
                    VirtualHost = vhost,
                    Component = component,
                    ParameterName = parameter,
                    ParameterValue = value
                };

            Debug.Assert(definition != null);
                
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(parameter))
                errors.Add(new() {Reason = "The name of the parameter is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new() {Reason = "The name of the virtual host is missing."});

            if (string.IsNullOrWhiteSpace(component))
                errors.Add(new() {Reason = "The component name is missing."});
                    
            string url = $"api/parameters/{component}/{vhost.ToSanitizedName()}/{parameter}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new () {URL = url, Request = definition.ToJsonString(), Errors = errors}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string parameter, string component, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
                
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(parameter))
                errors.Add(new() {Reason = "The name of the parameter is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new() {Reason = "The name of the virtual host is missing."});

            if (string.IsNullOrWhiteSpace(component))
                errors.Add(new() {Reason = "The component name is missing."});

            string url = $"api/parameters/{component}/{vhost.ToSanitizedName()}/{parameter}";

            if (errors.Any())
                return new FaultedResult {DebugInfo = new() {URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }
    }
}