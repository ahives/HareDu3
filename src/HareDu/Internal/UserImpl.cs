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
    using Core.Serialization;
    using Model;

    class UserImpl :
        BaseBrokerObject,
        User
    {
        public UserImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<UserInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/users";
            
            return await GetAllRequest<UserInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ResultList<UserInfo>> GetAllWithoutPermissions(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/users/without-permissions";
            
            return await GetAllRequest<UserInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string username, string password, string passwordHash = null,
            Action<UserConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new UserConfiguratorImpl();
            configurator?.Invoke(impl);

            string Normalize(string value) => string.IsNullOrWhiteSpace(value) ? null : value;

            UserRequest request =
                new ()
                {
                    Password = Normalize(password),
                    PasswordHash = !string.IsNullOrWhiteSpace(Normalize(password)) ? null : passwordHash,
                    Tags = impl.Tags.Value
                };

            Debug.Assert(request != null);
                    
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(username))
                errors.Add(new (){Reason = "The username is missing."});

            if (string.IsNullOrWhiteSpace(password))
            {
                if (string.IsNullOrWhiteSpace(passwordHash))
                    errors.Add(new (){Reason = "The password/hash is missing."});
            }
            
            string url = $"api/users/{username}";

            if (errors.Any())
                return new FaultedResult {DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

            return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string username, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
                
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(username))
                errors.Add(new (){Reason = "The username is missing."});

            string url = $"api/users/{username}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
        }


        class UserConfiguratorImpl :
            UserConfigurator
        {
            string _tags;

            public Lazy<string> Tags { get; }

            public UserConfiguratorImpl()
            {
                Tags = new Lazy<string>(() => _tags, LazyThreadSafetyMode.PublicationOnly);
            }

            public void WithTags(Action<UserAccessOptions> tags)
            {
                var impl = new UserAccessOptionsImpl();
                tags?.Invoke(impl);

                _tags = impl.ToString();
            }

            
            class UserAccessOptionsImpl :
                UserAccessOptions
            {
                List<string> Tags { get; }
                
                public UserAccessOptionsImpl()
                {
                    Tags = new List<string>();
                }

                public void None() => Tags.Add(UserAccessTag.None);

                public void Administrator() => Tags.Add(UserAccessTag.Administrator);

                public void Monitoring() => Tags.Add(UserAccessTag.Monitoring);

                public void Management() => Tags.Add(UserAccessTag.Management);
                
                public void PolicyMaker() => Tags.Add(UserAccessTag.PolicyMaker);

                public void Impersonator() => Tags.Add(UserAccessTag.Impersonator);

                public override string ToString()
                {
                    if (Tags.Contains(UserAccessTag.None) || !Tags.Any())
                        return UserAccessTag.None;

                    return string.Join(",", Tags);
                }
            }
        }
    }
}