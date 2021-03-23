namespace HareDu.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Extensions;
    using Model;
    using Serialization;

    class UserPermissionsImpl :
        BaseBrokerObject,
        UserPermissions
    {
        public UserPermissionsImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<UserPermissionsInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/permissions";
            
            return await GetAllRequest<UserPermissionsInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string username, string vhost,
            Action<UserPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new UserPermissionsConfiguratorImpl();
            configurator?.Invoke(impl);

            UserPermissionsRequest request =
                new ()
                {
                    Configure = impl.ConfigurePattern,
                    Write = impl.WritePattern,
                    Read = impl.ReadPattern
                };

            Debug.Assert(request != null);

            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(username))
                errors.Add(new (){Reason = "The username and/or password is missing."});
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});
            
            string url = $"api/permissions/{vhost.ToSanitizedName()}/{username}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

            return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(username))
                errors.Add(new (){Reason = "The username and/or password is missing."});
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});

            string url = $"api/permissions/{vhost.ToSanitizedName()}/{username}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
        }


        class UserPermissionsConfiguratorImpl :
            UserPermissionsConfigurator
        {
            public string ConfigurePattern { get; private set; }
            public string ReadPattern { get; private set; }
            public string WritePattern { get; private set; }

            public void UsingConfigurePattern(string pattern) => ConfigurePattern = pattern;

            public void UsingWritePattern(string pattern) => WritePattern = pattern;

            public void UsingReadPattern(string pattern) => ReadPattern = pattern;
        }
    }
}